using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzepsegSzalon
{
    internal class Foglalas
    {
        public int Foglalas_Id { get; set; }
        public int Szolgaltatas_Id { get; set; }
        public int Dolgozok_Id { get; set; }
        public int Ugyfel_Id { get; set; }
        public DateTime Foglalas_Kezdes { get; set; }
        public DateTime Foglalas_Befejezes { get; set; }


    }
}
