using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Foutopsporing_3___TryCatchDatum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Stel je hebt een .NET Core project
            // In de map \bin\Debug\netcoreapp3.1 zit de .exe file (ons programma)
            // Dus we gaan 3 mappen terug met .. en daarna zitten we in de map waarin de images en bin mappen staan
            // Kijk nu in images.

            // Stel je hebt een .NET Framework project en geen .NET Core
            // Dan is het 2x .. ipv 3x.

            bool isSucceeded = true;
            try
            {
                string fileName = @"..\..\..\images\foto.jpg";
                BitmapImage img = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + fileName, UriKind.RelativeOrAbsolute));
                Background = new ImageBrush(img);
            }
            catch (System.IO.IOException)
            {
                isSucceeded = false;
            }

            if (!isSucceeded)
            {
                MessageBox.Show("De afbeelding staat niet in het aangegeven pad.",
                  "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            MessageBox.Show($"Afbeelding op achtergrond van formulier is{(isSucceeded ? " " : " NIET ")}ingeladen.",
                   "Melding", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        bool IsDateCorrect(string dd, string mm, string yyyy)
        {
            int day, month, year;
            day = Convert.ToInt32(dd);
            month = Convert.ToInt32(mm);
            year = Convert.ToInt32(yyyy);

            if (year > DateTime.MaxValue.Year || year < DateTime.MinValue.Year)
                return false;
            if (month < 1 || month > 12)
                return false;
            if (day < 1 || day > DateTime.DaysInMonth(year, month))
                return false;

            return true;
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsDateCorrect(dayTextBox.Text, monthTextBox.Text, yearTextBox.Text))
                    MessageBox.Show("Geldige datum.", "Melding", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Ongeldige datum.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Gelieve 3 getallen in te geven.", "Foutmelding",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Gelieve 3 niet te grote getallen in te geven", "Foutmelding",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
