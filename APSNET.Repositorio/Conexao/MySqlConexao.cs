using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APSNET.Repositorio.Conexao
{
    public class MySqlConexao : IConexao
    {
        private string Server { get; set; }
        private string DB { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string strConexao { get; set; }
        public string Erro { get; set; }
        public int LinhasAfetadas { get; set; }
        public long LastInsertedId { get; set; }
        private MySqlConnection Conexao { get; set; }
        public bool UtilizarFakeTransaction { get; set; }
        private List<string> ListaComandosTransaction { get; set; }

        private bool utilizandoConexao;

        private int TentativasQuery = 0;

        private System.Object objLook = new System.Object();

        //private string AMBIENTE = "HOMOLOGACAO";
        private string AMBIENTE = "PRODUCAO";

        public MySqlConexao()
        {
            this.utilizandoConexao = true;
            this.DB = "api_teste";
            this.Server = "localhost";
            this.Username = "root";
            this.Password = "caara511";

            this.strConexao = "Server=" + Server + ";Database=" + DB + ";Uid=" + Username + ";Pwd=" + Password + "; Allow Zero Datetime=True; default command timeout=300;";

            this.Conexao = new MySqlConnection(this.strConexao);
            this.AbrirConexao();

            this.ListaComandosTransaction = new List<string>();

            this.utilizandoConexao = false;
        }

        public MySqlConexao(string server, string port)
        {
            this.utilizandoConexao = true;
            this.DB = "api_teste";
            this.Server = server;
            this.Username = "root";
            this.Password = "caara511";

            this.strConexao = "Server=" + Server + ";Database=" + DB + ";Uid=" + Username + ";Pwd=" + Password + "; Allow Zero Datetime=True; default command timeout=300;";

            string BancoDadosPorta = port;

            if (!string.IsNullOrEmpty(BancoDadosPorta))
            {
                this.strConexao = "Server=" + Server + ";Port=" + BancoDadosPorta + ";Database=" + DB + ";Uid=" + Username + ";Pwd=" + Password + "; Allow Zero Datetime=True; default command timeout=300;";
            }

            this.Conexao = new MySqlConnection(this.strConexao);
            this.AbrirConexao();

            this.ListaComandosTransaction = new List<string>();

            this.utilizandoConexao = false;
        }

        public bool AbrirConexao()
        {
            this.Erro = string.Empty;

            try
            {
                lock (objLook)
                {
                    if (this.Conexao.State == System.Data.ConnectionState.Open)
                    {
                        return true;
                    }

                    this.Conexao.Open();

                    if (this.Conexao.State == System.Data.ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Erro = "Conexão não foi realizada.";
                return false;
            }
        }

        public bool FecharConexao()
        {
            //this.Utilizando();
            this.Erro = string.Empty;

            try
            {
                lock (objLook)
                {
                    if (this.Conexao.State == System.Data.ConnectionState.Open)
                    {
                        this.Conexao.Close();
                        this.Conexao.Dispose();
                        MySqlConnection.ClearAllPools();
                        return true;
                    }

                    try
                    {
                        this.Conexao.Close();
                        this.Conexao.Dispose();
                        MySqlConnection.ClearAllPools();
                        return true;
                    }
                    catch (Exception)
                    {
                    }

                    if (this.Conexao.State == System.Data.ConnectionState.Closed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                this.Erro = "Conexão não foi finalizada.";
                return false;
            }
        }

        private void Utilizando()
        {
            while (this.utilizandoConexao)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        private void RepararTabelasBanco()
        {
            try
            {
                string query = "SELECT CONCAT('repair table ', table_name, ';') AS comando FROM information_schema.tables WHERE table_schema='" + this.DB + "';";
                System.Data.DataTable dt = this.Select(query);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string queryInterna = dt.Rows[i]["comando"].ToString();

                    try
                    {
                        this.ExecuteQuery(queryInterna);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public bool IniciarTransaction()
        {
            try
            {
                this.UtilizarFakeTransaction = true;
                this.ListaComandosTransaction = new List<string>();
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool ExecuteQueryTransaction()
        {
            // A tabela deve estar com a engine INNODB
            // se não estiver com esta engine a Transaction não é suportada 
            // com a alterãção da engine causam problemas visto que as informações não são mais salvas nos arquivos da pasta do banco de dados e sim em arquivos separados do banco de dados que fazem parte do MySql mesmo
            //
            /*
            this.Utilizando();

            this.utilizandoConexao = true;

            this.Erro = string.Empty;

            this.AbrirConexao();

            try
            {
                using (MySqlTransaction myTrans = this.Conexao.BeginTransaction())
                {
                    MySqlCommand myCommand = this.Conexao.CreateCommand();
                    myCommand.Connection = this.Conexao;
                    myCommand.Transaction = myTrans;

                    foreach (var item in this.ComandosTransaction)
                    {
                        myCommand.CommandText = item;
                        myCommand.ExecuteNonQuery();
                    }

                    myTrans.Commit();

                    this.utilizandoConexao = false;
                    this.ComandosTransaction.Clear();

                    return true;
                }
            }
            catch (Exception e)
            {
                this.ComandosTransaction.Clear();

                this.utilizandoConexao = false;
                this.Erro = e.ToString();

                System.Text.StringBuilder frase = new System.Text.StringBuilder();
                
                foreach (var item in this.ComandosTransaction)
                {
                    frase.AppendLine(item);
                }

                Log.SalvarLog(frase.ToString(), e.Message);

                if (this.Erro.IndexOf("time") > 0 || this.Erro.IndexOf("Connection must be valid and open") >= 0)
                {
                    throw new Exception("Servidor não encontrado.\nVerifique sua rede!\n\n" + e.Message);
                }
                else if (this.Erro.IndexOf("is marked as crashed and should be repaired") > 0)
                {
                    RepararTabelasBanco();
                    throw new Exception("Não foi possível executar o procedimento!");
                }
                else
                {
                    throw new Exception("Não foi possível executar o procedimento!");
                }
            }
            */
            return false;
        }

        public bool ExecuteQuery(string Query)
        {
            //this.Utilizando();

            this.utilizandoConexao = true;

            lock (objLook)
            {
                this.Erro = string.Empty;

                this.AbrirConexao();

                using (MySqlCommand Execute = null)
                {
                    try
                    {
                        if (this.UtilizarFakeTransaction == true)
                        {
                            this.ListaComandosTransaction.Add(Query);
                        }
                        else
                        {
                            //MySqlCommand myCommand = this.Conexao.CreateCommand();
                            MySqlCommand myCommand = new MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999", this.Conexao);
                            //myCommand.Connection = this.Conexao;
                            myCommand.CommandText = Query;

                            this.LinhasAfetadas = myCommand.ExecuteNonQuery();
                            this.LastInsertedId = myCommand.LastInsertedId;
                        }

                        this.utilizandoConexao = false;

                        return true;
                    }
                    catch (Exception e)
                    {
                        this.utilizandoConexao = false;
                        this.Erro = e.ToString();

                        if (this.Erro.IndexOf("time") > 0 || this.Erro.IndexOf("Connection must be valid and open") >= 0)
                        {
                            throw new Exception("Servidor não encontrado.\nVerifique sua rede!\n\n" + e.Message);
                        }
                        else if (this.Erro.IndexOf("is marked as crashed and should be repaired") > 0)
                        {
                            RepararTabelasBanco();
                            throw new Exception("Não foi possível executar o procedimento!");
                        }
                        else
                        {
                            throw new Exception("Não foi possível executar o procedimento!");
                        }
                    }
                }
            }
        }

        public bool ComitTransaction()
        {
            if (this.ListaComandosTransaction.Count > 0)
            {
                this.Utilizando();

                this.utilizandoConexao = true;

                this.Erro = string.Empty;

                this.UtilizarFakeTransaction = false;

                MySqlConexao conexaoParalela = new MySqlConexao();

                try
                {
                    conexaoParalela.AbrirConexao();

                    MySqlCommand myCommand;

                    foreach (var item in this.ListaComandosTransaction)
                    {
                        myCommand = conexaoParalela.Conexao.CreateCommand();
                        myCommand.Connection = conexaoParalela.Conexao;
                        myCommand.CommandText = item;
                        myCommand.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception e)
                {
                    this.Erro = e.ToString();


                    if (this.Erro.IndexOf("time") > 0 || this.Erro.IndexOf("Connection must be valid and open") >= 0)
                    {
                        throw new Exception("Servidor não encontrado.\nVerifique sua rede!\n\n" + e.Message);
                    }
                    else if (this.Erro.IndexOf("is marked as crashed and should be repaired") > 0)
                    {
                        RepararTabelasBanco();
                        throw new Exception("Não foi possível executar o procedimento!");
                    }
                    else
                    {
                        throw new Exception("Não foi possível executar o procedimento!");
                    }
                }
                finally
                {
                    this.ListaComandosTransaction.Clear();
                    this.utilizandoConexao = false;
                    this.UtilizarFakeTransaction = false;
                    conexaoParalela.FecharConexao();
                }
            }
            else
            {
                this.ListaComandosTransaction.Clear();
                this.utilizandoConexao = false;
                this.UtilizarFakeTransaction = false;
                return true;
            }
        }

        public System.Data.DataTable Select(string Query)
        {
            this.TentativasQuery++;
            //this.Utilizando();

            this.utilizandoConexao = true;

            this.Erro = string.Empty;

            this.AbrirConexao();

            System.Data.DataTable resultado = null;

            try
            {
                lock (objLook)
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(Query, this.Conexao))
                    {
                        resultado = new System.Data.DataTable();
                        adapter.Fill(resultado);

                        this.utilizandoConexao = false;
                    }
                }
            }
            catch (Exception e)
            {
                this.utilizandoConexao = false;
                this.Erro = e.Message.ToString();



                if (this.Erro.IndexOf("There is already an open DataReader associated with this Connection which must be closed first.") >= 0)
                {
                    if (this.TentativasQuery <= 3)
                    {
                        this.utilizandoConexao = false;
                        this.Conexao = new MySqlConnection(this.strConexao);
                        return this.Select(Query);
                    }
                }

                if (this.Erro.IndexOf("time") > 0 || this.Erro.IndexOf("Connection must be valid and open") >= 0 || this.Erro.IndexOf("Unable to connect to any of the specified MySQL hosts") >= 0)
                {
                    throw new Exception("Servidor não encontrado.\nVerifique sua rede!");
                }
                else if (this.Erro.IndexOf("is marked as crashed and should be repaired") > 0)
                {
                    RepararTabelasBanco();
                    throw new Exception("Não foi possível executar o procedimento!");
                }
                else
                {
                    throw new Exception("Erro de Banco."/* + e.Message*/);
                }
            }
            this.TentativasQuery = 0;
            return resultado;
        }

        public long InsertedId()
        {
            return this.LastInsertedId;
        }

        internal interface IConexao
        {
        }
    }
}