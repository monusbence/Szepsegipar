using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace Szepsegipar
{
    public partial class FoglalasMegtekintesePage : Page
    {
        private string connectionString = "Server=127.0.0.1;Database=szepsegszalon;User ID=root;";

        public FoglalasMegtekintesePage()
        {
            InitializeComponent();
            BetoltFoglalasok();
        }

        private void BetoltFoglalasok()
        {
            try
            {
                using (MySqlConnection kapcsolat = new MySqlConnection(connectionString))
                {
                    kapcsolat.Open();
                    string query = "SELECT f.F_ID AS Foglalas_Id, f.SZ_ID AS Szolgaltatas_Id, f.D_ID AS Dolgozo_Id, " +
                                   "f.U_ID AS Ugyfel_Id, f.F_Kezdes AS Foglalas_Kezdes, f.F_Befejezesk AS Foglalas_Befejezes " +
                                   "FROM foglalás f";

                    MySqlCommand parancs = new MySqlCommand(query, kapcsolat);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(parancs);
                    DataTable foglalasok = new DataTable();
                    adapter.Fill(foglalasok);

                    FoglalasListView.ItemsSource = foglalasok.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a foglalások betöltésekor: " + ex.Message);
            }
        }
    }
}
