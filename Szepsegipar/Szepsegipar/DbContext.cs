using MySql.Data.MySqlClient;
using SzepsegSzalon;

namespace Szepsegipar
{


    public class DatabaseService
    {
        private string connectionString = "Server=localhost;Database=szepsegszalon;Uid=root;Pwd=password;"; // Állítsd be a saját adatbázis kapcsolatodat

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Dolgozók lekérdezése
         List<Dolgozo>  GetDolgozok()
        {
            List<Dolgozo> dolgozok = new List<Dolgozo>();

            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT Dolgozok_Id, Dolgozok_VezetekNev, Dolgozok_KeresztNev FROM dolgozok";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dolgozok.Add(new Dolgozo
                            {
                                Dolgozo_Id = reader.GetInt32("Dolgozok_Id"),
                                Dolgozo_VezetekNev = reader.GetString("Dolgozok_VezetekNev"),
                                Dolgozo_KeresztNev = reader.GetString("Dolgozok_KeresztNev")
                            });
                        }
                    }
                }
            }

            return dolgozok;
        }

        // Szolgáltatások lekérdezése
         List<Szolgaltatas> GetSzolgaltatasok()
        {
            List<Szolgaltatas> szolgaltatasok = new List<Szolgaltatas>();

            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT Szolgaltatas_Id, Szolgaltatas_Kategoria, Szolgaltatas_Idotartam, Szolgaltatas_Ar FROM szolgaltatas";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            szolgaltatasok.Add(new Szolgaltatas
                            {
                                Szolgaltatas_Id = reader.GetInt32("Szolgaltatas_Id"),
                                Szolgaltatas_Kategoria = reader.GetString("Szolgaltatas_Kategoria"),
                                Szolgaltatas_Idotartam = reader.GetDateTime("Szolgaltatas_Idotartam"),
                                Szolgaltatas_Ar = reader.GetInt32("Szolgaltatas_Ar")
                            });
                        }
                    }
                }
            }

            return szolgaltatasok;
        }

        // Foglalás rögzítése
        public bool RogzitesFoglalas(int ugyfelId, int dolgozoId, int szolgaltatasId, DateTime kezdes, DateTime befejezes)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO foglalas (Ugyfel_Id, Dolgozok_Id, Szolgaltatas_Id, Foglalas_Kezdes, Foglalas_Befejezes) VALUES (@ugyfelId, @dolgozoId, @szolgaltatasId, @kezdes, @befejezes)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ugyfelId", ugyfelId);
                    command.Parameters.AddWithValue("@dolgozoId", dolgozoId);
                    command.Parameters.AddWithValue("@szolgaltatasId", szolgaltatasId);
                    command.Parameters.AddWithValue("@kezdes", kezdes);
                    command.Parameters.AddWithValue("@befejezes", befejezes);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


    }
}
