using Microsoft.Extensions.Configuration;

namespace RemessaFaccao.DAL.Setting
{
    public static class ConfigHelper
    {
        public static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        public static string ReadLog(string path)
        {
            string ultimaLinha = "";
            string linha;

            StreamReader sr = new(path);
            try
            {
                if (sr.ReadLine() is not null)
                {
                    while ((linha = sr.ReadLine()) is not null)
                        ultimaLinha = linha;
                }
                else
                {
                    sr.Close();
                    StreamWriter sw = new(path);
                    string date = DateTime.Now.ToString();
                    ultimaLinha = String.Format("NOVO ARQUIVO DE LOG - {0}", date);
                    sw.Write(ultimaLinha);
                    sw.Close();
                }
                sr.Close();

                return ultimaLinha;
            }
            catch (Exception)
            {
                sr.Close();
                return ultimaLinha;
            }
        }

        public static void WriteLog(string path, string novaLinha)
        {
            string conteudo = File.ReadAllText(path);
            string novoConteudo = conteudo + Environment.NewLine + novaLinha;
            File.WriteAllText(path, novoConteudo);
        }

        public static string PathOutLog(string key)
        {
            if (!Directory.Exists(configuration["PathOutLog"]))
                Directory.CreateDirectory(configuration["PathOutLog"]);

            DateTime date = DateTime.Now;
            string formatDate = String.Format("{0:s}", date).Substring(0, 10);
            string path = configuration["PathOutLog"] + "\\" + key + "_" + formatDate + ".txt";

            if (!File.Exists(path))
            {
                StreamWriter sw = new(path);
                sw.Close();
            }

            ConfigHelper.ReadLog(path);
            return path;
        }
    }
}
