using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowDesktop
{
    interface Servicio
    {
        Task<List<Conocimiento>> GetConocimientos(string id);
        Task<int> PostConocimiento(string obj, string des, string col, string ani, string gen, int cont);
        Task<int> GetUltimoNun();
        string GetConnSQL();
    }
}
