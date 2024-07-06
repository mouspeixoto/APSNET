using APSNET.Dominio.Configuracoes;
using APSNET.Repositorio.Configuracoes;
using APSNET.Repositorio;
using System;

namespace APSNET.Aplicacao.Factory
{
    public class RepositorioFactory
    {
        private static string GetTypeRepository()
        {
            string retorno = "MySQL";

            if (string.IsNullOrEmpty(retorno))
            {
                retorno = "MySQL";
            }

            return retorno;
        }

        #region Configuracoes


        public static IRepositorio<Configuracao> ConfiguracaoRepositorio()
        {
            switch (RepositorioFactory.GetTypeRepository())
            {
                case "MySQL":
                    return new ConfiguracaoRepositorio();

                default:
                    return new ConfiguracaoRepositorio();
            }
        }

        #endregion

    }
}