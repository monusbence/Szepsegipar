using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzepsegSzalon;

namespace Szepsegipar
{
    internal class FoglalasService
    {

        private List<Foglalas> foglalasok = new List<Foglalas>();
        private List<Szolgaltatas> szolgaltatasok = new List<Szolgaltatas>();
        private List<Dolgozo> dolgozok = new List<Dolgozo>();
        private List<Ugyfel> ugyfelek = new List<Ugyfel>();

        public FoglalasService()
        {
            // Tesztadatok feltöltése
            szolgaltatasok.Add(new Szolgaltatas { Szolgaltatas_Id = 1, Szolgaltatas_Kategoria = "Hajvágás", Szolgaltatas_Ar = 5000 });
            szolgaltatasok.Add(new Szolgaltatas { Szolgaltatas_Id = 2, Szolgaltatas_Kategoria = "Masszázs", Szolgaltatas_Ar = 10000 });
            szolgaltatasok.Add(new Szolgaltatas { Szolgaltatas_Id = 3, Szolgaltatas_Kategoria = "Manikűr", Szolgaltatas_Ar = 10000 });
            szolgaltatasok.Add(new Szolgaltatas { Szolgaltatas_Id = 4, Szolgaltatas_Kategoria = "Pedikűr", Szolgaltatas_Ar = 10000 });

            dolgozok.Add(new Dolgozo { Dolgozo_Id = 1, Dolgozo_VezetekNev = "Dodi", Dolgozo_KeresztNev = "Király", Statusz = true });
            dolgozok.Add(new Dolgozo { Dolgozo_Id = 2, Dolgozo_VezetekNev = "Mónus", Dolgozo_KeresztNev = "Bence", Statusz = true });
            dolgozok.Add(new Dolgozo { Dolgozo_Id = 3, Dolgozo_VezetekNev = "Taki", Dolgozo_KeresztNev = "Bazsi", Statusz = true });

            
        }

        public List<Szolgaltatas> GetSzolgaltatasok()
        {
            return szolgaltatasok;
        }

        public List<Dolgozo> GetDolgozok()
        {
            return dolgozok;
        }

        public List<Ugyfel> GetUgyfelek()
        {
            return ugyfelek;
        }

        public bool RogzitesFoglalas(int ugyfelId, int dolgozoId, int szolgaltatasId, DateTime kezdes, DateTime befejezes)
        {
            // Ellenőrzés: Van-e már foglalás ebben az időpontban a dolgozóval?
            var existingBooking = foglalasok.FirstOrDefault(f => f.Dolgozo_Id == dolgozoId &&
                                                                ((kezdes >= f.Foglalas_Kezdes && kezdes < f.Foglalas_Befejezes) ||
                                                                 (befejezes > f.Foglalas_Kezdes && befejezes <= f.Foglalas_Befejezes)));

            if (existingBooking != null)
            {
                return false; // Már van foglalás erre az időpontra
            }

            // Új foglalás létrehozása
            var ujFoglalas = new Foglalas
            {
                Foglalas_Id = foglalasok.Count + 1,
                Ugyfel_Id = ugyfelId,
                Dolgozo_Id = dolgozoId,
                Szolgaltatas_Id = szolgaltatasId,
                Foglalas_Kezdes = kezdes,
                Foglalas_Befejezes = befejezes
            };

            foglalasok.Add(ujFoglalas);
            return true;
        }

    }
}
