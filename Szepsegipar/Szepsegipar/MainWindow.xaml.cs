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

namespace Szepsegipar
{
    public partial class MainWindow : Window
    {
        private FoglalasService foglalasService = new FoglalasService();

        public MainWindow()
        {
            InitializeComponent();
            Feltoltes();
        }

        private void Feltoltes()
        {
            // Szolgáltatások ComboBox feltöltése
            SzolgaltatasComboBox.ItemsSource = foglalasService.GetSzolgaltatasok();
            SzolgaltatasComboBox.DisplayMemberPath = "Szolgaltatas_Kategoria";
            SzolgaltatasComboBox.SelectedValuePath = "Szolgaltatas_Id";

            // Dolgozók ComboBox feltöltése
            DolgozoComboBox.ItemsSource = foglalasService.GetDolgozok();
            DolgozoComboBox.DisplayMemberPath = "Dolgozo_VezetekNev";
            DolgozoComboBox.SelectedValuePath = "Dolgozo_Id";

            // Időpontok feltöltése
            FeltoltesIdoComboBox();
        }

        private void FeltoltesIdoComboBox()
        {
            TimeSpan startTime = new TimeSpan(0, 0, 0);  // 00:00
            TimeSpan endTime = new TimeSpan(23, 30, 0);  // 23:30
            TimeSpan interval = new TimeSpan(0, 30, 0);  // 30 perc

            for (TimeSpan time = startTime; time <= endTime; time += interval)
            {
                IdoComboBox.Items.Add(time.ToString(@"hh\:mm"));
            }

            IdoComboBox.SelectedIndex = 0; // Alapértelmezett kiválasztás
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
            int ugyfelId = 1; // Feltételezzük, hogy van egy bejelentkezett ügyfél, pl. Ugyfel_Id = 1
            int szolgaltatasId = (int)SzolgaltatasComboBox.SelectedValue;
            int dolgozoId = (int)DolgozoComboBox.SelectedValue;
            DateTime kezdesDatum = (DateTime)DatumPicker.SelectedDate;
            TimeSpan kezdesIdo = TimeSpan.Parse(IdoComboBox.SelectedItem.ToString());
            DateTime kezdes = kezdesDatum.Add(kezdesIdo);

            // Szolgáltatás időtartam meghatározása
            var selectedSzolgaltatas = foglalasService.GetSzolgaltatasok().FirstOrDefault(s => s.Szolgaltatas_Id == szolgaltatasId);
            if (selectedSzolgaltatas == null)
            {
                MessageBox.Show("Hiba történt a szolgáltatás kiválasztásakor!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 1 óra hosszúságú szolgáltatás például
            DateTime befejezes = kezdes.AddHours(1);

            // Foglalás rögzítése
            bool sikeres = foglalasService.RogzitesFoglalas(ugyfelId, dolgozoId, szolgaltatasId, kezdes, befejezes);

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