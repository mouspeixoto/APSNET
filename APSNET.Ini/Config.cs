using Ini.Net;

namespace APSNET.Ini
{
    public static class Config
    {
        private static IniFile Configurador = null;

        /// <summary>
        /// Carrega o configurador a partir do arquivo de configuração.
        /// </summary>
        /// <exception cref="Exception">Lança uma exceção se o arquivo de configuração não for encontrado.</exception>
        private static void CarregarConfigurador()
        {
            try
            {
                if (Config.Configurador == null)
                {
                    string CaminhoArquivo = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\config.ini";

                    if (System.IO.File.Exists(CaminhoArquivo))
                    {
                        Config.Configurador = new IniFile(CaminhoArquivo);
                    }
                    else if (System.IO.File.Exists(@"C:\ConfigSistemas\APSNET\config.ini"))
                    {
                        Config.Configurador = new IniFile(@"C:\ConfigSistemas\APSNET\config.ini");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Arquivo de configuração não encontrado!");
            }
        }

        /// <summary>
        /// Seleciona configurações do sistema.
        /// </summary>
        /// <param name="Section">Seção da configuração, por exemplo, "CONFIG".</param>
        /// <param name="Key">Chave da configuração, por exemplo, "CONFIGTESTE".</param>
        /// <returns>O valor da configuração especificada.</returns>
        public static string GetConfiguracao(string Section, string Key)
        {
            Config.CarregarConfigurador();

            string retorno = "";

            try
            {
                retorno = Config.Configurador.ReadString(Section, Key);
            }
            catch (Exception)
            {
            }

            return retorno;
        }

        /// <summary>
        /// Insere ou modifica configurações do sistema.
        /// </summary>
        /// <param name="Section">Seção da configuração, por exemplo, "CONFIG".</param>
        /// <param name="Key">Chave da configuração, por exemplo, "CONFIGTESTE".</param>
        /// <param name="Value">Valor da configuração, por exemplo, "teste".</param>
        /// <returns>True se a configuração foi inserida/modificada com sucesso, caso contrário, false.</returns>
        public static bool SetConfiguracao(string Section, string Key, string Value)
        {
            Config.CarregarConfigurador();

            bool retorno = false;

            try
            {
                retorno = Config.Configurador.WriteString(Section, Key, Value);
            }
            catch (Exception)
            {
            }

            return retorno;
        }
    }

}