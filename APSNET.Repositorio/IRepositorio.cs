using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Repositorio
{
    public interface IRepositorio<TEntity> : IDisposable where TEntity : class
    {
        TEntity Select(string id);
        List<TEntity> Select(List<string> parametros);
        List<TEntity> Select(List<string> parametros, int Limit);
        List<TEntity> Select(List<string> parametros, string OrderBy, bool Order, int Limit = 0);
        List<TEntity> Select(List<string> parametros, string GroupBy, int Limit = 0, string OrderBy = "");
        List<TEntity> SelectLoadAll(List<Type> Carregar, List<string> Parametros, int Limit = 0, string OrderBy = "", string GroupBy = "");
        bool Save(TEntity obj);
        bool Delete(TEntity obj);
        int Count(List<string> parametros);
        System.Data.DataTable Query(string query);
        bool ExecuteQuery(string query);
        bool IniciarTransaction();
        bool ComitTransaction();
    }
}
