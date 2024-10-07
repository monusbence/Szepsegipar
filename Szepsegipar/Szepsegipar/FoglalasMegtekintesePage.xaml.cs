using MySql.Data.MySqlClient; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Szepsegipar
{
    public partial class FoglalasMegtekintesePage : Page
    {
        public List<Foglalas> Foglalasok { get; set; }

        public FoglalasMegtekintesePage()
        {
            InitializeComponent();
            LoadFoglalasok(); 
        }

        private void LoadFoglalasok()
        {
            Foglalasok = new List<Foglalas>();

            string connectionString = "Server=127.0.0.1;Database=szepsegszalon;User ID=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT f.F_Id, s.SZ_Kategoria, d.D_VezetekNev, d.D_KeresztNev, u.U_VezetekNev, u.U_KeresztNev, f.F_Kezdes, f.F_Befejezesk
                             FROM foglalás f
                             JOIN szolgáltatás s ON f.SZ_Id = s.SZ_ID
                             JOIN dolgozók d ON f.D_Id = d.D_ID
                             JOIN ügyfél u ON f.U_Id = u.U_ID";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Foglalas foglalas = new Foglalas
                            {
                                Foglalas_Id = reader.GetInt32(0),
                                Szolgaltatas_Kategoria = reader.GetString(1),
                                Dolgozo_VezetekNev = reader.GetString(2),
                                Dolgozo_KeresztNev = reader.GetString(3),
                                Ugyfel_VezetekNev = reader.GetString(4),
                                Ugyfel_KeresztNev = reader.GetString(5),
                                Foglalas_Kezdes = reader.GetDateTime(6),
                                Foglalas_Befejezes = reader.GetDateTime(7),
                                IsSelected = false
                            };
                            Foglalasok.Add(foglalas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt az adatbázis lekérdezése során: {ex.Message}");
                }
            }

            FoglalasListView.ItemsSource = Foglalasok;
        }


        private void FrissitesGomb_Click(object sender, RoutedEventArgs e)
        {
            LoadFoglalasok(); 
        }

        private void TorlesGomb_Click(object sender, RoutedEventArgs e)
        {
            var selectedFoglalasok = Foglalasok.Where(f => f.IsSelected).ToList();

            if (selectedFoglalasok.Any())
            {
                string connectionString = "Server=127.0.0.1;Database=szepsegszalon;User ID=root;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var foglalas in selectedFoglalasok)
                    {
                        string deleteQuery = "DELETE FROM foglalás WHERE F_Id = @Foglalas_Id";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@Foglalas_Id", foglalas.Foglalas_Id);
                        deleteCommand.ExecuteNonQuery();
                    }
                }

                LoadFoglalasok(); 
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva törlendő foglalás.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = new MainWindow();
            Application.Current.MainWindow.Content = mainWindow.Content;
        }
    }

    public class Foglalas
    {
        public int Foglalas_Id { get; set; }
        public string Szolgaltatas_Kategoria { get; set; }
        public string Dolgozo_VezetekNev { get; set; }
        public string Dolgozo_KeresztNev { get; set; }
        public string Ugyfel_VezetekNev { get; set; }
        public string Ugyfel_KeresztNev { get; set; }
        public DateTime Foglalas_Kezdes { get; set; }
        public DateTime Foglalas_Befejezes { get; set; }
        public bool IsSelected { get; set; }
    }

}
