using System.Net;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace GFUpdateProject
{
    public partial class Main : Form
    {

        //static string serverVersionFile = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\version.txt"; // Arquivo no servidor
        //static string localVersionFile = "version.txt"; // Arquivo local

        //static string serverFilesBaseUrl = "A:\\GAMESALPHA\\GrandFantasia Server Files\\074\\TestServerFolder\\";
        

        static string manifestUrl = "http://172.26.162.7/UPDFiles/manifest.json"; // URL do manifesto no servidor
        static string localGamePath = @"A:\GAMESALPHA\GrandFantasia Server Files\074\TestClientFolder"; // Caminho do jogo no PC do client


        public Main()
        {
            InitializeComponent();
        }


        private async void updateBT_ClickAsync(object sender, EventArgs e)
        {

            //MessageBox.Show($"executar o comando");
            //await UpdateFiles();
            //MessageBox.Show($"executar o comando");


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


        public static async Task UpdateFiles() 
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    MessageBox.Show("Baixando o manifesto...");

                    // Baixar o manifesto JSON
                    string manifestJson = await client.GetStringAsync(manifestUrl);
                    var manifest = Newtonsoft.Json.JsonConvert.DeserializeObject<Manifest>(manifestJson);

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
                        if (!File.Exists(localFilePath) || !VerifyFileChecksum(localFilePath, file.Checksum))
                        {
                            //MessageBox.Show($"Baixando {file.Name}...");

                            // Baixar o arquivo
                            byte[] fileData = await client.GetByteArrayAsync(file.Url);
                            File.WriteAllBytes(localFilePath, fileData);

                           // MessageBox.Show($"{file.Name} atualizado com sucesso.");
                        }
                        else
                        {
                            //MessageBox.Show($"{file.Name} já está atualizado.");
                        }
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
                    using (var stream = File.OpenRead(filePath))
                    {
                        var fileChecksum = md5.ComputeHash(stream);
                        string fileChecksumHex = BitConverter.ToString(fileChecksum).Replace("-", "").ToLowerInvariant();
                        return fileChecksumHex == expectedChecksum;
                    }
                }
            }


         //END
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
