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

            Configuracao config = Select("VERSAO");
            versao = config.Valor1.GetDecimal();

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
