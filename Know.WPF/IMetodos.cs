using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Know.WPF.Models;

namespace Know.WPF
{
    interface IMetodos
    {
        Task<List<Knows>> GetConocimientos(string nombre);
        Task<int> PostConocimiento(string obj, string des, string col, string ani, string gen, int cont);
        Task<int> GetUltimoNun();
        string GetConnSQL();
    }
}
