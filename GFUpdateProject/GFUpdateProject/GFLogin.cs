using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using static GFUpdateProject.Main;

namespace GFUpdateProject
{
    public partial class GFLogin : Form
    {
        public GFLogin()
        {
            InitializeComponent();
            // Carregar as credenciais salvas ao abrir o formulário
            CarregarCredenciais();
        }

        // Método para carregar as últimas credenciais salvas
        private void CarregarCredenciais()
        {
            // Carrega o login e senha (em MD5) salvos
            txtLogin.Text = Properties.Settings.Default.Login;

            // Verificar se a opção de salvar a senha estava marcada
            SVPassCB.Checked = Properties.Settings.Default.SVPass;

            //SVPassCB.Checked ? txtSenha.Text = Properties.Settings.Default.Senha : txtSenha.Text = "";

            txtSenha.Text = (SVPassCB.Checked ? Properties.Settings.Default.Senha : "");

            // Se quiser carregar a senha original (desabilitado por padrão para segurança)
            // txtSenha.Text = Properties.Settings.Default.Senha;
        }

        // Método para salvar as credenciais no arquivo de configurações
        private void SalvarCredenciais(string login, string senha, bool svpassck)
        {
            Properties.Settings.Default.Login = login;
            Properties.Settings.Default.Senha = (svpassck ? senha : ""); // Ou senha em MD5, dependendo da sua necessidade
            Properties.Settings.Default.SVPass = svpassck;
            Properties.Settings.Default.Save(); // Salva os valores
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            // Obter os valores dos campos de login e senha
            string login = txtLogin.Text;
            string senha = txtSenha.Text;

            // Salvar as credenciais digitadas
            SalvarCredenciais(login, senha, SVPassCB.Checked);



            // Converter a senha para MD5
            string senhaHashMD5 = GerarHashMD5(senha);

            //Obter o GamePath
            string GamePath = SharedData.GameFolderTBValue.Substring(2);
            //GamePath = GamePath + "\\";
            //GamePath = GamePath.Replace("\\", "\\\\");
            string GamePathUnity = SharedData.GameFolderTBValue.Substring(0, 2);

            //MessageBox.Show(GamePath);
            //MessageBox.Show(GamePathUnity);

            // Conteúdo do script .bat (insira aqui o conteúdo do seu .bat)
            string batScript = "@echo off\n" +
                               GamePathUnity + "\n" +
                               "cd" + @GamePath + " \n" +
                               "start GrandFantasia.exe EasyFun -a %1 -p %2 \n";

            //"A:\n" +
            //"cd\\GAMESALPHA\\GrandFantasia Server Files\\074\\Ultimate\\ \n " +
            //"start GrandFantasia.exe EasyFun -a %1 -p %2 \n"; //+
            //"echo Parametros recebidos: %1 %2 \n" +
            //"pause";

            // Caminho temporário para criar o arquivo .bat em tempo de execução
            string tempBatPath = Path.Combine(Path.GetTempPath(), "tempScript.bat");

            // Escreve o conteúdo do .bat em um arquivo temporário
            File.WriteAllText(tempBatPath, batScript);

            // Monta os argumentos opcionais de login e senha
            string argumentos = string.Empty;

            if (!string.IsNullOrEmpty(login))
            {
                argumentos += $" {login}";
            }

            if (!string.IsNullOrEmpty(senhaHashMD5))
            {
                argumentos += $" {senhaHashMD5}";
            }

            // Cria as informações do processo para executar o .bat temporário
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = tempBatPath;
            processInfo.Arguments = argumentos;
            processInfo.UseShellExecute = false;

            try
            {
                // Inicia o processo
                Process process = Process.Start(processInfo);
                process.WaitForExit(); // Aguarda o processo terminar

                //MessageBox.Show("Comando executado com sucesso.");

                // Encerra o programa, já que tudo correu bem
                Application.Exit();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar o comando: {ex.Message}");
            }
            finally
            {
                // Remove o arquivo .bat temporário após a execução
                if (File.Exists(tempBatPath))
                {
                    File.Delete(tempBatPath);
                }
            }
        }

        // Método para converter o texto da senha em hash MD5
        private string GerarHashMD5(string input)
        {
            // Inicializa o MD5
            using (MD5 md5 = MD5.Create())
            {
                // Converte o input para bytes
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);

                // Gera o hash em bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Converte os bytes para string hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "X2" gera string em formato hexadecimal
                }

                return sb.ToString(); // Retorna o hash MD5 como string
            }

        }

        private void ManualLogin_Click(object sender, EventArgs e)
        {

            // Conteúdo do script .bat (insira aqui o conteúdo do seu .bat)
            string batScript = "@echo off\n" +
                               "start GrandFantasia.exe EasyFun";
            //"A:\n" +
            //"cd\\GAMESALPHA\\GrandFantasia Server Files\\074\\Ultimate\\ \n " +
            //"start GrandFantasia.exe EasyFun";


            // Caminho temporário para criar o arquivo .bat em tempo de execução
            string tempBatPath = Path.Combine(Path.GetTempPath(), "tempScript.bat");

            // Escreve o conteúdo do .bat em um arquivo temporário
            File.WriteAllText(tempBatPath, batScript);


            // Cria as informações do processo para executar o .bat temporário
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = tempBatPath;
            processInfo.UseShellExecute = false;

            try
            {
                // Inicia o processo
                Process process = Process.Start(processInfo);
                process.WaitForExit(); // Aguarda o processo terminar

                //MessageBox.Show("Comando executado com sucesso.");

                // Encerra o programa, já que tudo correu bem
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar o comando: {ex.Message}");
            }
            finally
            {
                // Remove o arquivo .bat temporário após a execução
                if (File.Exists(tempBatPath))
                {
                    File.Delete(tempBatPath);
                }
            }
        }
    }
}
