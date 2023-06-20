using Microsoft.Extensions.Configuration;

namespace RemessaFaccao.DAL.Setting
{
    public static class ConfigHelper
    {
        public static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        public static string LerArquivo(string path)
        {
            string ultimaLinha = "";
            string linha;

            StreamReader sr = new(path);

            if (sr.ReadLine().Any())
                while ((linha = sr.ReadLine()) is not null)
                    ultimaLinha = linha;
            else
            {
                sr.Close();
                StreamWriter sw = new(path);
                ultimaLinha = "NOVO ARQUIVO DE LOG";
                sw.Write(ultimaLinha);
                sw.Close();
            }
            return ultimaLinha;
        }

        public static void EscreverProxLinha(string path, string linhaAtual, string novaLinha)
        {
            string conteudo = File.ReadAllText(path);
            string novoConteudo = conteudo.Replace(linhaAtual, linhaAtual + Environment.NewLine + novaLinha);
            File.WriteAllText(path, novoConteudo);
        }

        public static string PathOutLogLogin()
        {
            if (!Directory.Exists(configuration["PathOutLog"]))
                Directory.CreateDirectory(configuration["PathOutLog"]);

            string path = configuration["PathOutLog"] + "\\Login.txt";

            if (!File.Exists(path))
            {
                StreamWriter sw = new(path);
                sw.Close();
            }
            return path;
        }
    }
}
