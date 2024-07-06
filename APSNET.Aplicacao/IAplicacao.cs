using System;
using System.Collections.Generic;

namespace APSNET.Aplicacao
{
    interface IAplicacao<TEntity> : IDisposable where TEntity : class
    {
        TEntity Select(string id);
        List<TEntity> Select(List<string> parametros);
        List<TEntity> Select(List<string> parametros, int Limit);
        List<TEntity> Select(List<string> parametros, string OrderBy, bool Order, int Limit = 0);
        List<TEntity> Select(List<string> parametros, string GroupBy, int Limit = 0, string OrderBy = "");
        bool Save(TEntity obj);
        bool Delete(TEntity obj);
        int Count(List<string> parametros);
    }
}
