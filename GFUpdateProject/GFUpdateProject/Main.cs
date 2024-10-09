using System.Net;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;
using GFClientLoginProject;

namespace GFUpdateProject
{
    public partial class Main : Form
    {

        //static string serverVersionFile = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\version.txt"; // Arquivo no servidor
        //static string localVersionFile = "version.txt"; // Arquivo local

        //static string serverFilesBaseUrl = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\";

        private static string ServerIP = Properties.Settings.Default.ServerIpConfig;
        //static string manifestUrl = "http://172.26.162.7/UPDFiles/manifest.json"; // URL do manifesto no servidor
        static string manifestUrl = "http://" + ServerIP + "/UPDFiles/manifest.json"; // URL do manifesto no servidor
        //static string localGamePath = @"A:\GAMESALPHA\GrandFantasia Server Files\074\TestClientFolder"; // Caminho do jogo no PC do client
        static string localGamePath = Environment.CurrentDirectory; // Caminho do jogo no PC do client
        static Manifest manifest;


        public Main()
        {

            InitializeComponent();
            ManifestDownload();


            GameFolderTB.Text = localGamePath;
            ServerAddressTB.Text = ServerIP;
            LoginBT.Enabled = false;


        }


        private async void updateBT_ClickAsync(object sender, EventArgs e)
        {

            //MessageBox.Show($"executar o comando");
            //await UpdateFiles();
            //MessageBox.Show($"executar o comando");

            // Ler o manifesto
            //var manifest = JsonConvert.DeserializeObject<Manifest>(File.ReadAllText(manifestUrl));

            
            updateBT.Enabled = false;



            try
            {
                MessageBox.Show("Iniciando a atualização...");


                bool manifestLoaded = await ManifestDownload();

                if (!manifestLoaded)
                {
                    MessageBox.Show("Erro ao carregar o manifesto. Operação cancelada.");
                    updateBT.Enabled = true;
                    return; // Cancela a execução se houver falha ao obter os dados

                }

                // Definir a barra de progresso com o número total de arquivos
                FullPBCust.Maximum = manifest.Files.Length;
                FullPBCust.Value = 0; // Começar no zero

                // Chama o método de download dos arquivos
                await UpdateFiles();

                MessageBox.Show("Atualização concluída.");
            }
            catch (Exception ex)
            {
                // Captura qualquer erro ocorrido durante o processo
                MessageBox.Show($"Erro durante a atualização: {ex.Message}");
            }

            updateBT.Enabled = true;

        }

        public async Task<bool> ManifestDownload()
        {
            ManifestDownloadBT.Enabled = false;
            manifestUrl = "http://" + ServerIP + "/UPDFiles/manifest.json";
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    MessageBox.Show("Buscando o manifesto...");

                    // Define um tempo limite para a requisição (por exemplo, 10 segundos)
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // Tenta baixar o arquivo de manifesto
                    HttpResponseMessage response = await client.GetAsync(manifestUrl);

                    // Verifica se o arquivo foi encontrado no servidor
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"O arquivo de manifesto não foi encontrado: {manifestUrl}");
                        ManifestDownloadBT.Enabled = true;
                        return false;
                    }


                    MessageBox.Show("Baixando o manifesto...");

                    // Baixar o manifesto JSON
                    string manifestJson = await client.GetStringAsync(manifestUrl);
                    manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestJson);
                    ManifestVersionTB.Text = manifest.Version;

                    MessageBox.Show(manifest.Version);
                    ManifestDownloadBT.Enabled = true;
                    return true;

                }
            }
            catch (HttpRequestException ex)
            {
                // Captura erros de conexão
                throw new Exception($"Erro ao acessar o manifesto: {ex.Message}");
                ManifestDownloadBT.Enabled = true;
                return false;
            }
            catch (TaskCanceledException ex)
            {
                // Captura erro de timeout (quando o servidor não responde no tempo esperado)
                MessageBox.Show("O servidor não respondeu no tempo esperado. Verifique o IP ou endereço.");
                ManifestDownloadBT.Enabled = true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao baixar os arquivos: {ex.Message}");
                ManifestDownloadBT.Enabled = true;
                return false;
            }
            

        }


        public async Task UpdateFiles()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //MessageBox.Show("Baixando o manifesto...");

                    // Baixar o manifesto JSON
                    //string manifestJson = await client.GetStringAsync(manifestUrl);
                    //var manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestJson);

                    // Processar cada arquivo no manifesto
                    foreach (var file in manifest.Files)
                    {
                        // Caminho local para salvar o arquivo (incluindo subpastas)
                        string localFilePath = Path.Combine(localGamePath, file.Name);

                        // Criar o diretório se não existir
                        string directoryPath = Path.GetDirectoryName(localFilePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Verificar se o arquivo precisa ser atualizado
                        if (!System.IO.File.Exists(localFilePath) || !VerifyFileChecksum(localFilePath, file.Checksum))
                        {
                            MessageBox.Show($"Baixando {file.Name}...");

                            // Baixar o arquivo
                            //byte[] fileData = await client.GetByteArrayAsync(file.Url);
                            //File.WriteAllBytes(localFilePath, fileData);

                            // Baixar o arquivo com progresso
                            await DownloadFileWithProgressAsync(client, file.Url, localFilePath);



                            // MessageBox.Show($"{file.Name} atualizado com sucesso.");
                        }
                        else
                        {
                            MessageBox.Show($"{file.Name} já está atualizado.");
                        }

                        // Atualizar a barra de progresso
                        //FullPB.Value += 1;
                        //FilePBCust.Value += 1;
                        FullPBCust.Value += 1;


                    }

                    LoginBT.Enabled = true;

                }
            }
            catch (HttpRequestException ex)
            {
                // Captura erros de conexão
                throw new Exception($"Erro ao acessar o arquivo: {ex.Message}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao baixar os arquivos: {ex.Message}");
            }


            // Função para verificar o checksum de um arquivo
            static bool VerifyFileChecksum(string filePath, string expectedChecksum)
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = System.IO.File.OpenRead(filePath))
                    {
                        var fileChecksum = md5.ComputeHash(stream);
                        string fileChecksumHex = BitConverter.ToString(fileChecksum).Replace("-", "").ToLowerInvariant();
                        return fileChecksumHex == expectedChecksum;
                    }
                }
            }


            //END
        }


        private async Task DownloadFileWithProgressAsync(HttpClient client, string fileUrl, string localFilePath)
        {
            using (var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                // Tamanho total do arquivo
                var totalBytes = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;

                // Inicializar a barra de progresso do arquivo
                FilePBCust.Value = 0;
                FilePBCust.Maximum = (int)totalBytes;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        var buffer = new byte[8192];
                        long totalRead = 0;
                        int bytesRead;

                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            if (totalBytes != -1)
                            {
                                // Atualizar a barra de progresso para o arquivo atual
                                FilePBCust.Value = (int)totalRead;
                            }
                        }
                    }
                }
            }
        }




        // Estrutura que representa o manifesto JSON
        public class Manifest
        {
            public string Version { get; set; }
            public FileInfo[] Files { get; set; }
        }

        public class FileInfo
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Checksum { get; set; }
            public long Size { get; set; }
        }

        private void GameFolderBT_Click(object sender, EventArgs e)
        {

            // Instancia a classe.
            using (FolderBrowserDialog dirDialog = new FolderBrowserDialog())
            {
                // Mostra a janela de escolha do directorio
                DialogResult res = dirDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    // Como o utilizador carregou no OK, o directorio escolhido pode ser acedido da seguinte forma:
                    localGamePath = dirDialog.SelectedPath;
                    GameFolderTB.Text = localGamePath;
                }
                else
                {
                    // Caso o utilizador tenha cancelado
                    // ...
                }
            }


        }

        private void ManifestDownloadBT_Click(object sender, EventArgs e)
        {
            ManifestDownload();
        }

        private void ServerAddressTB_TextChanged(object sender, EventArgs e)
        {
            ServerIP = ServerAddressTB.Text;
            //MessageBox.Show(ServerIP);
            Properties.Settings.Default.ServerIpConfig = ServerIP;
            Properties.Settings.Default.Save();
        }

        private void LoginBT_Click(object sender, EventArgs e)
        {
            GFLogin gfloginf = new GFLogin();

            gfloginf.ShowDialog();
        }
    }
}
