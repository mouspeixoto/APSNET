using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APSNET.Repositorio.Conexao;

namespace APSNET.Repositorio.ConexoesFactory
{
    public static class ConexaoFactory
    {
        public static IConexao Conexao = null;
        public static IConexao ConexaoExterna = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conexao">SERVER|PORT</param>
        /// <returns></returns>
        public static IConexao GetConexao(string conexao = "")
        {
            if (string.IsNullOrEmpty(conexao))
            {

                var tipo = "MySql";

                switch (tipo)
                {
                    case "MySql":
                        if (ConexaoFactory.Conexao == null)
                        {
                            ConexaoFactory.Conexao = new MySqlConexao();
                        }
                        break;
                    default:
                        break;
                }

                return ConexaoFactory.Conexao;
            }
            else
            {
                if (ConexaoFactory.ConexaoExterna == null)
                {
                    var spl = conexao.Split('|');

                    ConexaoFactory.ConexaoExterna = new MySqlConexao(spl[0], spl[1]);
                }
                return ConexaoFactory.ConexaoExterna;
            }


        }
    }
}