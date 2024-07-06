using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSNET.Repositorio
{
    public abstract class RepositorioAbs<TEntity> : BaseClass where TEntity : class
    {
        protected abstract List<TEntity> _Select(List<string> Parametros, int Limit = 0, string OrderBy = "", string GroupBy = "");
        protected abstract bool Insert(TEntity obj);
        protected abstract bool Update(TEntity obj);
    }
}