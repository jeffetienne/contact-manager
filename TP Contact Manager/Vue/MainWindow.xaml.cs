using BLL;
using Model;
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

namespace Vue
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

        private void ConnecterButton_Click(object sender, RoutedEventArgs e)
        {
            Usager usager = UsagerManager.Login(IdentifiantTextBox.Text, MotDePasseTextBox.Password);
            if (usager != null)
            {
                PrincipalWindow menuPrincipalWindow = new PrincipalWindow(usager);
                menuPrincipalWindow.Show();
                this.Close();
            }
            else
            {
                ErrorLogin.Text = "Identifiant ou mot de passe incorrect";
            }
        }
    }
}
