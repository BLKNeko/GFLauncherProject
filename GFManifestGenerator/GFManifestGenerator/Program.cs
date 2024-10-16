using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using Newtonsoft.Json;

namespace GFManifestGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Caminho da pasta que você quer escanear
            //string folderPath = @"\GAMESALPHA\GrandFantasia Server Files\074\TestServerFolder"; // Altere para o caminho desejado
            //string manifestPath = @"A:\GAMESALPHA\GrandFantasia Server Files\GF Projects\GFManifestGenerator"; // Altere para onde você quer salvar o manifesto

            int major = 1;
            int minor = 0;
            int patch = 0;
            string control = "1";
            string folderPath = "";
            string versionInput = "1.0.0";




            while (control == "1")
            {


                // Solicitar ao usuário o caminho da pasta a ser escaneada
                Console.WriteLine("=====================================================================");
                Console.WriteLine("Digite o caminho da pasta que deseja escanear:");
                Console.WriteLine("=====================================================================");
                folderPath = Console.ReadLine();

                // Verificar se o caminho da pasta é válido
                while (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("O caminho da pasta não existe. Certifique-se de que está correto.");
                    folderPath = Console.ReadLine();
                }

                // Solicitar ao usuário a versao do manifesto
                Console.WriteLine("=====================================================================");
                Console.WriteLine("Digite a versao do manifesto ex: 1.0.0 // Major.Minor.Patch");
                Console.WriteLine("=====================================================================");
                Console.WriteLine("Digite o valor de MAJOR // Major.Minor.Patch");
                while (!int.TryParse(Console.ReadLine(), out major))
                {
                    Console.WriteLine("Insira apenas números inteiros");
                    Console.WriteLine("INSERINDO VALOR PARA MAJOR");
                }

                Console.WriteLine("=====================================================================");
                Console.WriteLine("Digite o valor de MINOR // Major.Minor.Patch");
                while (!int.TryParse(Console.ReadLine(), out minor))
                {
                    Console.WriteLine("Insira apenas números inteiros");
                    Console.WriteLine("INSERINDO VALOR PARA MINOR");
                }

                Console.WriteLine("=====================================================================");
                Console.WriteLine("Digite o valor de PATCH // Major.Minor.Patch");
                while (!int.TryParse(Console.ReadLine(), out patch))
                {
                    Console.WriteLine("Insira apenas números inteiros");
                    Console.WriteLine("INSERINDO VALOR PARA PATCH");
                }

                Console.WriteLine("=====================================================================");
                Console.WriteLine($"Versao digitada: {major}.{minor}.{patch} // Major.Minor.Patch");
                Console.WriteLine("=====================================================================");
                Console.WriteLine("Deseja Alterar algum valor?");
                Console.WriteLine("Se Sim, digite 1 e pressione enter");
                Console.WriteLine("=====================================================================");
                control = Console.ReadLine();




            }

            versionInput = $"{major}.{minor}.{patch}";

            // Caminho local para salvar o arquivo (incluindo subpastas)
            string manifestResultFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneratedManifest");

            // Criar o diretório se não existir
            if (!Directory.Exists(manifestResultFolderPath))
            {
                Directory.CreateDirectory(manifestResultFolderPath);
            }

            // Definir o caminho do arquivo 'manifest.json' dentro da pasta 'generatedManifest'
            string manifestResultPath = Path.Combine(manifestResultFolderPath, "manifest.json");


            // Gerar o manifesto
            var manifest = GenerateManifest(folderPath, versionInput);

            // Serializar o manifesto para JSON e salvar em um arquivo
            string json = JsonConvert.SerializeObject(manifest, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(manifestResultPath, json);
            Console.WriteLine(manifestResultPath, json);

            Console.WriteLine("Manifesto gerado com sucesso!");
        }

        static Manifest GenerateManifest(string folderPath, string versionInput)
        {
            var manifest = new Manifest { Version = versionInput, Files = new List<FileEntry>() };

            // Percorrer todos os arquivos e subpastas
            foreach (var file in Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(folderPath, file);
                manifest.Files.Add(new FileEntry
                {
                    Name = relativePath.Replace(Path.DirectorySeparatorChar, '/'), // Trocar separador de diretório por '/'
                    Url = $"http://172.26.162.7/GFPLFiles/{relativePath.Replace(Path.DirectorySeparatorChar, '/')}", // Atualize o URL conforme necessário
                    Checksum = CalculateMD5(file),
                    Size = new FileInfo(file).Length
                });
                Console.WriteLine(relativePath);
            }

            return manifest;
        }

        static string CalculateMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }

    public class Manifest
    {
        public string Version { get; set; }
        public List<FileEntry> Files { get; set; }
    }

    public class FileEntry
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Checksum { get; set; }
        public long Size { get; set; }
    }
}
