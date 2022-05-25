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
using System.Windows.Shapes;
using Model;

namespace Vue
{
    /// <summary>
    /// Interaction logic for PrincipalWindow.xaml
    /// </summary>
    public partial class PrincipalWindow : Window
    {
        private Usager usager;
        public PrincipalWindow()
        {
            InitializeComponent();
        }

        public PrincipalWindow(Usager usager)
        {
            InitializeComponent();
            this.usager = usager;
            UserLabel.Content = "Bienvenue " + usager.Identifiant;
            if (usager.Role == 2 || usager.Role == 3)
            {
                AjouterUsagerButton.Visibility = Visibility.Visible;
            }
            else
            {
                AjouterUsagerButton.Visibility = Visibility.Collapsed;
                Separateur.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AjouterContactUserControl();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = new AfficherContactUserControl();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataContext = new RechercherContactUserControl();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataContext = new EnvoyerMessageUserControl();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataContext = new AfficherMessageUserControl();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DataContext = new FavorisUserControl();
        }

        private void AjouterUsagerButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new GestionUsagersUserControl(this.usager);
        }

        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
