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
    }
}