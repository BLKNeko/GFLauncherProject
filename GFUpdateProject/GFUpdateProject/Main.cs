using System.Net;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace GFUpdateProject
{
    public partial class Main : Form
    {

        //static string serverVersionFile = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\version.txt"; // Arquivo no servidor
        //static string localVersionFile = "version.txt"; // Arquivo local

        //static string serverFilesBaseUrl = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\";


        static string manifestUrl = "http://172.26.162.7/UPDFiles/manifest.json"; // URL do manifesto no servidor
        static string localGamePath = @"A:\GAMESALPHA\GrandFantasia Server Files\074\TestClientFolder"; // Caminho do jogo no PC do client
        static Manifest manifest;


        public Main()
        {

            InitializeComponent();
            ManifestDownload();
        }


        private async void updateBT_ClickAsync(object sender, EventArgs e)
        {

            //MessageBox.Show($"executar o comando");
            //await UpdateFiles();
            //MessageBox.Show($"executar o comando");

            // Ler o manifesto
            //var manifest = JsonConvert.DeserializeObject<Manifest>(File.ReadAllText(manifestUrl));

            // Definir a barra de progresso com o número total de arquivos
            FullPBCust.Maximum = manifest.Files.Length;
            FullPBCust.Value = 0; // Começar no zero


            try
            {
                MessageBox.Show("Iniciando a atualização...");

                // Chama o método de download dos arquivos
                await UpdateFiles();

                MessageBox.Show("Atualização concluída.");
            }
            catch (Exception ex)
            {
                // Captura qualquer erro ocorrido durante o processo
                MessageBox.Show($"Erro durante a atualização: {ex.Message}");
            }



        }

        public static async Task ManifestDownload() 
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    MessageBox.Show("Baixando o manifesto...");

                    // Baixar o manifesto JSON
                    string manifestJson = await client.GetStringAsync(manifestUrl);
                    manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestJson);

                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao baixar os arquivos: {ex.Message}");
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
                }
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

    }
}
