﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using SzepsegSzalon;
using static Szepsegipar.DatabaseService;

namespace Szepsegipar
{
    public partial class MainWindow : Window
    {
        public DatabaseService databaseService;
        public List<Dolgozo> dolgozok;
        public List<Szolgaltatas> szolgaltatasok;

        public MainWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
            BetoltDolgozok();
            BetoltSzolgaltatasok();
            BetoltIdopontok();

            DolgozoComboBox.SelectionChanged += (s, e) => BetoltIdopontok();
            DatumPicker.SelectedDateChanged += (s, e) => BetoltIdopontok();
        }

        private void BetoltDolgozok()
        {
            dolgozok = databaseService.GetDolgozok();
            DolgozoComboBox.ItemsSource = dolgozok;
            DolgozoComboBox.DisplayMemberPath = "TeljesNev"; // Teljes név jelenik meg
            DolgozoComboBox.SelectedValuePath = "Dolgozo_Id"; // Érték az ID lesz
        }

        private void BetoltSzolgaltatasok()
        {
            szolgaltatasok = databaseService.GetSzolgaltatasok();
            SzolgaltatasComboBox.ItemsSource = szolgaltatasok;
            SzolgaltatasComboBox.DisplayMemberPath = "Szolgaltatas_Kategoria"; // Megjeleníti a szolgáltatások nevét
            SzolgaltatasComboBox.SelectedValuePath = "Szolgaltatas_Id"; // Érték az ID lesz
        }

        private void BetoltIdopontok()
        {
            if (DolgozoComboBox.SelectedValue == null || DatumPicker.SelectedDate == null)
            {
                IdoComboBox.ItemsSource = null;
                return;
            }

            int selectedDolgozoId = (int)DolgozoComboBox.SelectedValue;
            DateTime selectedDatum = (DateTime)DatumPicker.SelectedDate;

            List<string> szabadIdopontok = GetSzabadIdopontok(selectedDolgozoId, selectedDatum);
            IdoComboBox.ItemsSource = szabadIdopontok;
        }

        private List<string> GetSzabadIdopontok(int dolgozoId, DateTime datum)
        {
            List<TimeSpan> foglaltIdopontok = databaseService.GetFoglalasokByDolgozoAndDatum(dolgozoId, datum);
            List<string> szabadIdopontok = new List<string>();

            TimeSpan kezdes = new TimeSpan(8, 0, 0); // 8:00
            TimeSpan veg = new TimeSpan(15, 30, 0);  // 15:30
            TimeSpan lepeskoz = new TimeSpan(0, 30, 0); // 30 perc

            for (TimeSpan ido = kezdes; ido <= veg; ido = ido.Add(lepeskoz))
            {
                if (!foglaltIdopontok.Contains(ido))
                {
                    szabadIdopontok.Add(ido.ToString(@"hh\:mm"));
                }
            }

            return szabadIdopontok;
        }



        private void RogzitesGomb_Click(object sender, RoutedEventArgs e)
        {
            // Ellenőrizzük a kiválasztott értékeket
            if (SzolgaltatasComboBox.SelectedValue == null || DolgozoComboBox.SelectedValue == null || DatumPicker.SelectedDate == null || IdoComboBox.SelectedItem == null)
            {
                MessageBox.Show("Kérjük, töltse ki az összes mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Adatok lekérése a felületről
            int ugyfelId = 1; // Feltételezzük, hogy van egy bejelentkezett ügyfél
            int szolgaltatasId = (int)SzolgaltatasComboBox.SelectedValue;
            int dolgozoId = (int)DolgozoComboBox.SelectedValue;
            DateTime kezdesDatum = (DateTime)DatumPicker.SelectedDate;
            TimeSpan kezdesIdo = TimeSpan.Parse(IdoComboBox.SelectedItem.ToString());
            DateTime kezdes = kezdesDatum.Add(kezdesIdo);

            // Ellenőrzés: ne legyen a foglalás korábban, mint a jelenlegi időpont
            if (kezdes < DateTime.Now)
            {
                MessageBox.Show("A foglalás időpontja nem lehet korábban, mint a jelenlegi idő!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Szolgáltatás időtartam meghatározása
            var selectedSzolgaltatas = szolgaltatasok.FirstOrDefault(s => s.Szolgaltatas_Id == szolgaltatasId);
            if (selectedSzolgaltatas == null)
            {
                MessageBox.Show("Hiba történt a szolgáltatás kiválasztásakor!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime befejezes = kezdes.AddMinutes(selectedSzolgaltatas.Szolgaltatas_Idotartam.TotalMinutes);


            // Foglalás rögzítése
            bool sikeres = databaseService.RogzitesFoglalas(ugyfelId, dolgozoId, szolgaltatasId, kezdes, befejezes);

            if (sikeres)
            {
                MessageBox.Show("Foglalás sikeresen rögzítve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("A megadott időpontra már van foglalás!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FoglalasMegtekinteseGomb_Click(object sender, RoutedEventArgs e)
        {
            FoglalasMegtekintesePage foglalasMegtekintesePage = new FoglalasMegtekintesePage();
            this.Content = foglalasMegtekintesePage;
        }
        private void MegtekintesGomb_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the FoglalasMegtekintesePage
            FoglalasMegtekintesePage foglalasMegtekintesePage = new FoglalasMegtekintesePage();
            MainFrame.Content = foglalasMegtekintesePage; // Set the new page to the Frame
        }
    }
}
