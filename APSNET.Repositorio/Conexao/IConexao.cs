using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Repositorio.Conexao
{
    public interface IConexao
    {
        bool AbrirConexao();
        bool FecharConexao();
        bool IniciarTransaction();
        bool ExecuteQuery(string Query);
        bool ComitTransaction();
        System.Data.DataTable Select(string Query);
        long InsertedId();
    }
}
