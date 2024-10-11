using MySql.Data.MySqlClient;
using SzepsegSzalon;

namespace Szepsegipar { 
    //foglalas delete add, szolgaltatas add update
public class DatabaseService
{
    private string connectionString = "Server=127.0.0.1;Database=szepsegszalon; User Id=root; Password=;";

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    // Dolgozók lekérdezése
    public List<Dolgozo> GetDolgozok()
    {
        List<Dolgozo> dolgozok = new List<Dolgozo>();

        using (var connection = GetConnection())
        {
            connection.Open();
            string query = "SELECT D_ID, D_VezetekNev, D_KeresztNev FROM dolgozók WHERE Statusz = 1";
            using (var command = new MySqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dolgozok.Add(new Dolgozo
                        {
                            Dolgozo_Id = reader.GetInt32("D_ID"),
                            Dolgozo_VezetekNev = reader.GetString("D_VezetekNev"),
                            Dolgozo_KeresztNev = reader.GetString("D_KeresztNev")
                        });
                    }
                }
            }
        }

        return dolgozok;
    }

    // Szolgáltatások lekérdezése
    public List<Szolgaltatas> GetSzolgaltatasok()
    {
        List<Szolgaltatas> szolgaltatasok = new List<Szolgaltatas>();

        using (var connection = GetConnection())
        {
            connection.Open();
                string query = "SELECT SZ_ID, SZ_Kategoria, SZ_Idotartam, SZ_Ar FROM szolgáltatás";
                using (var command = new MySqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            szolgaltatasok.Add(new Szolgaltatas
                            {
                                Szolgaltatas_Id = reader.GetInt32("SZ_ID"),
                                Szolgaltatas_Kategoria = reader.GetString("SZ_Kategoria"),
                                Szolgaltatas_Idotartam = TimeSpan.FromMinutes(reader.GetInt32("SZ_Idotartam")),
                                Szolgaltatas_Ar = reader.GetInt32("SZ_Ar")
                            });
                        }
                }
            }
        }

        return szolgaltatasok;
    }

        public List<TimeSpan> GetFoglalasokByDolgozoAndDatum(int dolgozoId, DateTime datum)
        {
            List<TimeSpan> foglaltIdopontok = new List<TimeSpan>();

            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT F_Kezdes FROM foglalás WHERE D_ID = @dolgozoId AND DATE(F_Kezdes) = @datum";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dolgozoId", dolgozoId);
                    command.Parameters.AddWithValue("@datum", datum.ToString("yyyy-MM-dd"));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime kezdete = reader.GetDateTime("F_Kezdes");
                            foglaltIdopontok.Add(kezdete.TimeOfDay);
                        }
                    }
                }
            }

            return foglaltIdopontok;
        }

        // Foglalás rögzítése
        public bool RogzitesFoglalas(int ugyfelId, int dolgozoId, int szolgaltatasId, DateTime kezdes, DateTime befejezes)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Ellenőrizzük, van-e már foglalás az adott időpontban
                string ellenorzesQuery = @"
            SELECT COUNT(*) 
            FROM foglalás 
            WHERE D_ID = @dolgozoId 
            AND ((@kezdes >= F_Kezdes AND @kezdes < F_Befejezesk) 
            OR (@befejezes > F_Kezdes AND @befejezes <= F_Befejezesk)
            OR (@kezdes <= F_Kezdes AND @befejezes >= F_Befejezesk))";

                using (var ellenorzesCommand = new MySqlCommand(ellenorzesQuery, connection))
                {
                    ellenorzesCommand.Parameters.AddWithValue("@dolgozoId", dolgozoId);
                    ellenorzesCommand.Parameters.AddWithValue("@kezdes", kezdes);
                    ellenorzesCommand.Parameters.AddWithValue("@befejezes", befejezes);

                    int foglaltIdopontok = Convert.ToInt32(ellenorzesCommand.ExecuteScalar());

                    if (foglaltIdopontok > 0)
                    {
                        // Ha már van foglalás, visszaadunk false-t, hogy jelezzük a hibát
                        return false;
                    }
                }

                // Ha nincs ütközés, akkor beszúrjuk az új foglalást
                string insertQuery = "INSERT INTO foglalás (U_ID, D_ID, SZ_ID, F_Kezdes, F_Befejezesk) VALUES (@ugyfelId, @dolgozoId, @szolgaltatasId, @kezdes, @befejezes)";
                using (var command = new MySqlCommand(insertQuery, connection))
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

