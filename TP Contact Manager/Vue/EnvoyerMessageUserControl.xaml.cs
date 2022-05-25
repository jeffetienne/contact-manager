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
using BLL;
using Model;

namespace Vue
{
    /// <summary>
    /// Interaction logic for EnvoyerMessageUserControl.xaml
    /// </summary>
    public partial class EnvoyerMessageUserControl : UserControl
    {
        public EnvoyerMessageUserControl()
        {
            InitializeComponent();
        }

        private void RechercherButton_Click(object sender, RoutedEventArgs e)
        {
            if (CritereTextBox.Text != string.Empty)
            {
                ContactComboBox.ItemsSource = ContactManager.RechercherContacts("Prenom", CritereTextBox.Text);
                ContactComboBox.DisplayMemberPath = "Prenom";
                ContactComboBox.SelectedValuePath = "Id";
            }
            else
            {
                MessageBox.Show("Veuillez saisir une valeur!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ContactComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactComboBox.SelectedIndex > -1)
            {
                Contact contact = ContactManager.RechercherContact((int)ContactComboBox.SelectedValue);
                if (contact != null)
                {
                    TitreLabel.Content = "Envoyer un message a " + contact.Prenom + " " + contact.Nom;
                }
            }
        }

        private void EnvoyerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContactComboBox.Text == string.Empty)
            {
                MessageBox.Show("Veuillez selectionner un contact!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (ContenuTextBox.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le contenu du message!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (ContactComboBox.Text != string.Empty && ContenuTextBox.Text != string.Empty)
            {
                string message = MessageManager.EnvoyerMessage(new Message((int)ContactComboBox.SelectedValue, ContenuTextBox.Text, DateTime.Now));

                MessageBox.Show(message);
            }
        }
    }
}
