using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzepsegSzalon
{
    public class Dolgozo
    {

        public int Dolgozo_Id { get; set; }
        public string Dolgozo_VezetekNev { get; set; }
        public string Dolgozo_KeresztNev { get; set; }
        public string Dolgozo_TelefonSzam { get; set; }
        public string Dolgozo_Email { get; set; }
        public bool Statusz { get; set; }
        public int Szolgaltatas { get; set; }
        public int Jogkor { get; set; }


        public string TeljesNev
        {
            get { return $"{Dolgozo_VezetekNev} {Dolgozo_KeresztNev}"; }
        }

    }
}
