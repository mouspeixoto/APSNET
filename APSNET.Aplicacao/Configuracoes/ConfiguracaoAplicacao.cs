using APSNET.Dominio.Configuracoes;
using APSNET.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Aplicacao.Configuracoes
{
    public class ConfiguracaoAplicacao : AplicacaoAbs<Configuracao>
    {
        public ConfiguracaoAplicacao()
        {
            this.Repositorio = Factory.RepositorioFactory.ConfiguracaoRepositorio();
        }

        public bool ExecuteQuery(string query)
        {
            return this.Repositorio.ExecuteQuery(query);
        }

        public decimal VersaoSistemaAtual()
        {
            decimal versao;

            try
            {
                Configuracao config = Select("VERSAO");
                versao = config.Valor1.GetDecimal();
            }
            catch (Exception)
            {
                string queryCriaConfig = "CREATE TABLE IF NOT EXISTS `configuracoes` (`configuracao` varchar(150) NOT NULL,`valor1` text,`valor2` varchar(200) DEFAULT NULL, PRIMARY KEY (`configuracao`)) ENGINE=MyISAM DEFAULT CHARSET=utf8;";
                this.ExecuteQuery(queryCriaConfig);
                string queryInsereConfig = "INSERT INTO `configuracoes` (`configuracao`, `valor1`, `valor2`) VALUES('VERSAO', '1,00', '');";
                this.ExecuteQuery(queryInsereConfig);

                Configuracao config = Select("VERSAO");
                versao = config.Valor1.GetDecimal();
            }

            return versao;
        }

        public Configuracao BuscaConfiguracao(string configuracao)
        {
            List<string> parametros = new List<string>();
            parametros.Add("configuracao='" + configuracao + "'");

            return this.Repositorio.Select(parametros).FirstOrDefault();
        }
    }
}
