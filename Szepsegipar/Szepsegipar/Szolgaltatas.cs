using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzepsegSzalon
{
    public class Szolgaltatas
    {
        public int Szolgaltatas_Id { get; set; }
        public string Szolgaltatas_Kategoria { get; set; }
        public TimeSpan Szolgaltatas_Idotartam { get; set; }
        public int Szolgaltatas_Ar { get; set; }

    }
}
