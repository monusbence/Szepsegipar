using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzepsegSzalon
{
    internal class Dolgozo
    {

        public int Dolgozok_Id { get; set; }
        public string Dolgozok_VezetekNev { get; set; }
        public string Dolgozok_KeresztNev { get; set; }
        public string Dolgozok_TelefonSzam { get; set; }
        public string Dolgozok_Email { get; set; }
        public bool Statusz { get; set; }
        public int Szolgaltatas { get; set; }
        public int Jogkor { get; set; }

    }
}
