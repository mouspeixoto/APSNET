using APSNET.Dominio.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Repositorio.Configuracoes
{
    public class ConfiguracaoRepositorio : RepositorioAbs<Configuracao>, IRepositorio<Configuracao>
    {
        private Conexao.IConexao Conexao { get; set; }

        public ConfiguracaoRepositorio()
        {
            this.Conexao = ConexoesFactory.ConexaoFactory.GetConexao();
        }

        protected override List<Configuracao> _Select(List<string> Parametros, int Limit = 0, string OrderBy = "", string GroupBy = "")
        {
            List<Configuracao> listaRegistros = new List<Configuracao>();

            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM configuracoes");

                if (Parametros.Count > 0)
                {
                    StringBuilder qParametros = new StringBuilder();
                    qParametros.AppendLine("WHERE ");

                    foreach (string parametro in Parametros)
                    {
                        string parm = parametro;
                        qParametros.AppendLine(parm + " AND ");
                    }

                    query.AppendLine(qParametros.ToString().Substring(0, qParametros.Length - 6));
                }

                if (GroupBy != string.Empty)
                {
                    query.AppendLine("GROUP BY " + GroupBy);
                }

                if (OrderBy == string.Empty)
                {
                    query.AppendLine("ORDER BY configuracao");
                }
                else
                {
                    query.AppendLine("ORDER BY " + OrderBy);
                }

                if (Limit > 0)
                {
                    query.AppendLine("LIMIT " + Limit);
                }

                System.Data.DataTable dt = this.Conexao.Select(query.ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Configuracao registro = new Configuracao();
                    registro.IConfiguracao = dt.Rows[i]["configuracao"].ToString();
                    registro.Valor1 = dt.Rows[i]["valor1"].ToString();
                    registro.Valor2 = dt.Rows[i]["valor2"].ToString();

                    listaRegistros.Add(registro);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return listaRegistros;
        }

        public Configuracao Select(string id)
        {
            List<string> parametros = new List<string>();
            parametros.Add("configuracao='" + id + "'");
            return this.Select(parametros, 1).FirstOrDefault();
        }

        public List<Configuracao> Select(List<string> parametros)
        {
            return this._Select(parametros);
        }

        public List<Configuracao> Select(List<string> parametros, int Limit)
        {
            return this._Select(parametros, Limit);
        }

        public List<Configuracao> Select(List<string> parametros, string OrderBy, bool Order, int Limit = 0)
        {
            return this._Select(parametros, Limit, OrderBy);
        }

        public List<Configuracao> Select(List<string> parametros, string GroupBy, int Limit = 0, string OrderBy = "")
        {
            return this._Select(parametros, Limit, OrderBy, GroupBy);
        }

        public List<Configuracao> SelectLoadAll(List<Type> Carregar, List<string> Parametros, int Limit = 0, string OrderBy = "", string GroupBy = "")
        {
            throw new NotImplementedException();
        }

        public bool Save(Configuracao obj)
        {

            if (this.Insert(obj))
            {
                return true;
            }
            else
            {
                return this.Update(obj);
            }

        }

        protected override bool Insert(Configuracao obj)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO configuracoes(configuracao, valor1, valor2)");
                query.AppendLine("VALUES(");
                query.AppendLine("'" + obj.IConfiguracao + "', '" + obj.Valor1 + "', '" + obj.Valor2 + "' )");

                return this.Conexao.ExecuteQuery(query.ToString());
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override bool Update(Configuracao obj)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE configuracoes SET valor1='" + obj.Valor1 + "', valor2='" + obj.Valor2 + "' WHERE configuracao='" + obj.IConfiguracao + "'");

                this.Conexao.ExecuteQuery(query.ToString());
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(Configuracao obj)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM configuracoes WHERE configuracao='" + obj.IConfiguracao + "'");

                this.Conexao.ExecuteQuery(query.ToString());
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Count(List<string> parametros)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Query(string query)
        {
            return this.Conexao.Select(query);
        }

        public bool ExecuteQuery(string query)
        {
            return this.Conexao.ExecuteQuery(query);
        }

        public bool IniciarTransaction()
        {
            throw new NotImplementedException();
        }

        public bool ComitTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
