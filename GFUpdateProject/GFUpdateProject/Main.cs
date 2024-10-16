using System.Net;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;
using GFClientLoginProject;
using System.Linq;

namespace GFUpdateProject
{
    public partial class Main : Form
    {

        //static string serverVersionFile = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\version.txt"; // Arquivo no servidor
        //static string localVersionFile = "version.txt"; // Arquivo local

        //static string serverFilesBaseUrl = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\";

        private static string ServerIP = Properties.Settings.Default.ServerIpConfig;
        //static string manifestUrl = "http://172.26.162.7/UPDFiles/manifest.json"; // URL do manifesto no servidor
        static string manifestUrl = "http://" + ServerIP + "/GFPLFiles/manifest.json"; // URL do manifesto no servidor
        //static string localGamePath = @"A:\GAMESALPHA\GrandFantasia Server Files\074\TestClientFolder"; // Caminho do jogo no PC do client
        static string localGamePath = Environment.CurrentDirectory; // Caminho do jogo no PC do client
        static Manifest manifest;


        public Main()
        {

            InitializeComponent();
            LabelFix();
            //CheckManifestVersion();


            GameFolderTB.Text = localGamePath;
            SharedData.GameFolderTBValue = GameFolderTB.Text;
            ServerAddressTB.Text = ServerIP;
            //LoginBT.Enabled = false;
            //LoginBT.Enabled = true;





            // Mover a verifica��o do manifesto para o evento Load
            this.Load += Main_Load;
        }

        // Evento Load ass�ncrono
        private async void Main_Load(object sender, EventArgs e)
        {

            

            // Chama o m�todo ass�ncrono e aguarda a execu��o
            await CheckManifestVersion();
            await UpdateProgressLB(false, "0000 / 0000");
            await UpdateProgressLB(true, "0000 / 0000");



            

        }

        // Propriedade p�blica para acessar o valor da TextBox

        private void LabelFix()
        {
            //IpLabel
            IpLabel.Parent = BGFrameUPPB;
            IpLabel.BackColor = Color.Transparent;
            IpLabel.ForeColor = Color.White;
            //MessageLB
            MessageLB.Parent = BGFrameUPPB;
            MessageLB.BackColor = Color.Transparent;
            MessageLB.ForeColor = Color.White;
            //ManifestVersionLB
            ManifestVersionLB.Parent = BGFrameUPPB;
            ManifestVersionLB.BackColor = Color.Transparent;
            ManifestVersionLB.ForeColor = Color.White;
            //FileProgressLB
            FileProgressLB.Parent = BGFrameDWPB;
            FileProgressLB.BackColor = Color.Transparent;
            FileProgressLB.ForeColor = Color.White;
            FileProgressLB.Location = Point.Subtract(FileProgressLB.Location, new Size(BGFrameDWPB.Left, BGFrameDWPB.Top));
            //FullProgressLB
            FullProgressLB.Parent = BGFrameDWPB;
            FullProgressLB.BackColor = Color.Transparent;
            FullProgressLB.ForeColor = Color.White;
            FullProgressLB.Location = Point.Subtract(FullProgressLB.Location, new Size(BGFrameDWPB.Left, BGFrameDWPB.Top));
        }


        public static class SharedData
        {
            public static string GameFolderTBValue { get; set; }
        }

        public async Task UpdateMessage(string message)
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageTB.Text = message;
                });
            });

        }

        private async Task CheckManifestVersion()
        {
            bool GetManifest = await ManifestDownload();

            if(!GetManifest)
            {
                MessageBox.Show($"Falha ao verificar versao!");
                return;
            }

            string ManiVersion = manifest.Version.Replace(".", "");


            if (Int32.Parse(ManiVersion) > Properties.Settings.Default.ManiVers)
            {
                MessageBox.Show("Nova versao identificada! faca o update para entrar no jogo!");
                await UpdateMessage("Nova versao identificada! faca o update para entrar no jogo!");
                LoginBT.Enabled = false;
                updateBT.Enabled = true;
                Properties.Settings.Default.ManiVers = Int32.Parse(ManiVersion);
                Properties.Settings.Default.Save();
            }
            else
            {
                LoginBT.Enabled = true;
                await UpdateMessage("Pronto para jogar!!!");
            }
                

        }

        public async Task UpdateProgressLB(bool Full, string message)
        {
            if (Full)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        FullProgressLB.Text = message;
                    });
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        FileProgressLB.Text = message;
                    });
                });
            }

            

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
                //MessageBox.Show("Iniciando a atualiza��o...");
                //MessageTB.Text = "Iniciando a atualiza��o...";
                await UpdateMessage("Iniciando a atualiza��o...");


                bool manifestLoaded = await ManifestDownload();

                if (!manifestLoaded)
                {
                    MessageBox.Show("Erro ao carregar o manifesto. Opera��o cancelada.");
                    updateBT.Enabled = true;
                    return; // Cancela a execu��o se houver falha ao obter os dados

                }

                // Definir a barra de progresso com o n�mero total de arquivos
                FullPBCust.Maximum = manifest.Files.Length;
                FullPBCust.Value = 0; // Come�ar no zero

                await UpdateProgressLB(true, $"{FullPBCust.Value} / {FullPBCust.Maximum}");
                await UpdateProgressLB(false, "0000 / 0000");


                //MessageBox.Show(manifest.Files.Length.ToString());



                // Chama o m�todo de download dos arquivos
                await UpdateFiles();

                //MessageBox.Show("Atualiza��o conclu�da.");
                //MessageTB.Text = "Atualiza��o conclu�da!";
                await UpdateMessage("Atualiza��o conclu�da!");
                LoginBT.Enabled = true;
            }
            catch (Exception ex)
            {
                // Captura qualquer erro ocorrido durante o processo
                MessageBox.Show($"Erro durante a atualiza��o: {ex.Message}");
            }

            updateBT.Enabled = true;

        }

        public async Task<bool> ManifestDownload()
        {
            ManifestDownloadBT.Enabled = false;
            manifestUrl = "http://" + ServerIP + "/GFPLFiles/manifest.json";
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    //MessageBox.Show("Buscando o manifesto...");
                    MessageTB.Text = "Buscando o manifesto...";

                    // Define um tempo limite para a requisi��o (por exemplo, 10 segundos)
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // Tenta baixar o arquivo de manifesto
                    HttpResponseMessage response = await client.GetAsync(manifestUrl);

                    // Verifica se o arquivo foi encontrado no servidor
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"O arquivo de manifesto n�o foi encontrado: {manifestUrl}");
                        ManifestDownloadBT.Enabled = true;
                        return false;
                    }


                    //MessageBox.Show("Baixando o manifesto...");
                    MessageTB.Text = "Baixando o manifesto...";

                    // Baixar o manifesto JSON
                    string manifestJson = await client.GetStringAsync(manifestUrl);
                    manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestJson);
                    ManifestVersionTB.Text = manifest.Version;

                    //MessageBox.Show(manifest.Version);
                    MessageTB.Text = "Donwload do manifesto concluido!";
                    ManifestDownloadBT.Enabled = true;
                    FullProgressLB.Text = manifest.Files.Length.ToString() + " / " + manifest.Files.Length.ToString();
                    return true;

                }
            }
            catch (HttpRequestException ex)
            {
                // Captura erros de conex�o
                throw new Exception($"Erro ao acessar o manifesto: {ex.Message}");
                ManifestDownloadBT.Enabled = true;
                return false;
            }
            catch (TaskCanceledException ex)
            {
                // Captura erro de timeout (quando o servidor n�o responde no tempo esperado)
                MessageBox.Show("O servidor n�o respondeu no tempo esperado. Verifique o IP ou endere�o.");
                MessageTB.Text = "Falha na busca pelo manifesto, verifique o endereco do servidor.";
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

                    this.Invoke((MethodInvoker)delegate
                    {
                        FullPBCust.Maximum = manifest.Files.Length;  // Definir o valor m�ximo
                        FullPBCust.Value = 0;  // Reiniciar o progresso
                    });


                    // Processar cada arquivo no manifesto
                    foreach (var file in manifest.Files)
                    {
                        // Caminho local para salvar o arquivo (incluindo subpastas)
                        string localFilePath = Path.Combine(localGamePath, file.Name);

                        // Criar o diret�rio se n�o existir
                        string directoryPath = Path.GetDirectoryName(localFilePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Verificar se o arquivo precisa ser atualizado
                        if (!System.IO.File.Exists(localFilePath) || !VerifyFileChecksum(localFilePath, file.Checksum))
                        {
                            // Atualiza a TextBox na UI thread
                           // this.Invoke((MethodInvoker)delegate
                           // {
                            //    MessageTB.Text = $"Baixando {file.Name}...";
                           // });

                            await UpdateMessage($"Baixando {file.Name}...");

                            // Baixar o arquivo
                            //byte[] fileData = await client.GetByteArrayAsync(file.Url);
                            //File.WriteAllBytes(localFilePath, fileData);

                            // Baixar o arquivo com progresso
                            await DownloadFileWithProgressAsync(client, file.Url, localFilePath);


                            await UpdateMessage($"{file.Name} atualizado com sucesso.");
                            // MessageBox.Show($"{file.Name} atualizado com sucesso.");
                            //this.Invoke((MethodInvoker)delegate
                            //{
                            //    MessageTB.Text = $"{file.Name} atualizado com sucesso.";
                            //});
                        }
                        else
                        {
                            //MessageBox.Show($"{file.Name} j� est� atualizado.");

                            await UpdateMessage($"{file.Name} j� est� atualizado.");

                            //MessageTB.Text = $"j� est� atualizado.";


                            //this.Invoke((MethodInvoker)delegate
                            //{
                            //    MessageTB.Text = $"{file.Name} j� est� atualizado.";
                            //});
                        }

                        // Atualizar a barra de progresso
                        //FullPB.Value += 1;
                        //FilePBCust.Value += 1;
                        
                        this.Invoke((MethodInvoker)delegate
                        {
                            FullPBCust.Value += 1;
                        });
                        await UpdateProgressLB(true, $"{FullPBCust.Value} / {FullPBCust.Maximum}");


                    }

                    LoginBT.Enabled = true;

                }
                //HttpClientHandler handler = new HttpClientHandler
                //{
                //    MaxConnectionsPerServer = 10 // Aumenta o n�mero m�ximo de conex�es simult�neas por servidor
                //};
            }
            catch (HttpRequestException ex)
            {
                // Captura erros de conex�o
                throw new Exception($"Erro ao acessar o arquivo: {ex.Message}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao baixar os arquivos: {ex.Message}");
            }


            // Fun��o para verificar o checksum de um arquivo
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
                

                this.Invoke((MethodInvoker)delegate
                {
                    FilePBCust.Value = 0;
                    FilePBCust.Maximum = (int)totalBytes;
                });

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
                                
                                this.Invoke((MethodInvoker)delegate
                                {
                                    FilePBCust.Value = (int)totalRead;
                                });
                                await UpdateProgressLB(false, $"{FilePBCust.Value} / {FilePBCust.Maximum}");
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
                    SharedData.GameFolderTBValue = GameFolderTB.Text;
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
