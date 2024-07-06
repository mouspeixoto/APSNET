using APSNET.Aplicacao.Configuracoes;
using APSNET.Dominio.Configuracoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Aplicacao.Versao
{
    public class VerificarVersao
    {
        public decimal Versao = 1.01M;
        public decimal VersaoAtual;
        private DataTable dtTab = new DataTable();

        private ConfiguracaoAplicacao configuracaoApp = new ConfiguracaoAplicacao();

        public VerificarVersao()
        {
            if (this.NecessitaAtualizar())
            {
                this.Atualizar();
            }
        }


        public decimal VerificarVersaoBancoDeDados()
        {
            this.VersaoAtual = configuracaoApp.VersaoSistemaAtual();

            return this.VersaoAtual;
        }

        private bool NecessitaAtualizar()
        {
            decimal parm = 2;

            try
            {
                parm = configuracaoApp.VersaoSistemaAtual();
            }
            catch (Exception)
            {
                //throw ex;
            }

            this.VersaoAtual = parm;

            if (this.VersaoAtual < this.Versao)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ComandosVersao(string Versao)
        {
            this.dtTab.Clear();
            switch (Versao)
            {
                case "1,01":
                    break;

                default:
                    break;
            }
        }

        public void Atualizar()
        {
            try
            {
                this.VersaoAtual += 0.01M;

                string vers = this.VersaoAtual.ToString();
                this.ComandosVersao(vers);

                for (int i = 0; i < dtTab.Rows.Count; i++)
                {
                    string comando = dtTab.Rows[i]["comando"].ToString();

                    if (!string.IsNullOrEmpty(comando))
                    {
                        try
                        {
                            this.configuracaoApp.ExecuteQuery(comando);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                Configuracao config = this.configuracaoApp.Select("VERSAO");
                config.Valor1 = this.VersaoAtual.ToString();
                this.configuracaoApp.Save(config);

                while (this.NecessitaAtualizar())
                {
                    this.Atualizar();
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
