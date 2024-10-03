using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using SzepsegSzalon;
using static Szepsegipar.DatabaseService;


namespace Szepsegipar
{
    public partial class MainWindow : Window
    {
        public DatabaseService databaseService;
         List<Dolgozo> dolgozok;
         List<Szolgaltatas> szolgaltatasok;

        public MainWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
            BetoltDolgozok();
            BetoltSzolgaltatasok();
        }

        private void BetoltDolgozok()
        {
            dolgozok = databaseService.GetDolgozok();
            DolgozoComboBox.ItemsSource = dolgozok;
            DolgozoComboBox.DisplayMemberPath = "Dolgozok_KeresztNev"; // Megjeleníti a dolgozók nevét
            DolgozoComboBox.SelectedValuePath = "Dolgozok_Id"; // Érték az ID lesz
        }

        private void BetoltSzolgaltatasok()
        {
            szolgaltatasok = databaseService.GetSzolgaltatasok();
            SzolgaltatasComboBox.ItemsSource = szolgaltatasok;
            SzolgaltatasComboBox.DisplayMemberPath = "Szolgaltatas_Kategoria"; // Megjeleníti a szolgáltatások nevét
            SzolgaltatasComboBox.SelectedValuePath = "Szolgaltatas_Id"; // Érték az ID lesz
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

            DateTime befejezes = kezdes.Add(selectedSzolgaltatas.Szolgaltatas_Idotartam.TimeOfDay);

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
    }
}