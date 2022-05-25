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
    /// Interaction logic for RechercherContactUserControl.xaml
    /// </summary>
    public partial class RechercherContactUserControl : UserControl
    {
        public RechercherContactUserControl()
        {
            InitializeComponent();
        }

        private void RechercherButton_Click(object sender, RoutedEventArgs e)
        {

            if (CritereComboBox.Text != string.Empty && CritereTextBox.Text != string.Empty)
            {
                ContactsDataGrid.ItemsSource = ContactManager.RechercherContacts(CritereComboBox.Text, CritereTextBox.Text);
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsDataGrid.SelectedItem != null)
            {
                ContactViewModel contactViewModel = (ContactViewModel)ContactsDataGrid.SelectedItem;
                Contact contact = ContactManager.RechercherContact(contactViewModel.Id);
                AjouterContactWindow ajouterContactWindow = new AjouterContactWindow(contact);
                ajouterContactWindow.Show();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une ligne!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce contact?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ContactViewModel contactViewModel = (ContactViewModel)ContactsDataGrid.SelectedItem;
                    Contact contact = ContactManager.RechercherContact(contactViewModel.Id);
                    string message = ContactManager.SupprimerContact(contact.Id);
                    MessageBox.Show(message);
                    CritereComboBox.Text = string.Empty;
                    CritereTextBox.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une ligne!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void FavoriButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment ajouter ce contact aux favoris?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ContactViewModel contactViewModel = (ContactViewModel)ContactsDataGrid.SelectedItem;
                    Contact contact = ContactManager.RechercherContact(contactViewModel.Id);
                    Favori favori = FavoriManager.GetFavori(contact.Id);
                    if (favori != null)
                    {
                        MessageBox.Show("Ce contact est deja un favori!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        favori = new Favori(0, contact.Id);
                        string message = FavoriManager.AjouterFavori(favori);
                        MessageBox.Show(message, "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une ligne!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ContactsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactsDataGrid.SelectedItem != null)
            {
                ContactViewModel contactViewModel = (ContactViewModel)ContactsDataGrid.SelectedItem;
                Contact contact = ContactManager.RechercherContact(contactViewModel.Id);
                Favori favori = FavoriManager.GetFavori(contact.Id);
                if (favori != null)
                {
                    FavoriButton.Background = Brushes.Yellow;
                    FavoriButton.Foreground = Brushes.Black;
                }
                else
                {
                    FavoriButton.Background = (Brush)(new BrushConverter()).ConvertFrom("#0275D8");
                    FavoriButton.Foreground = Brushes.White;
                }
            }
        }

        private void CritereTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CritereComboBox.Text != string.Empty && CritereTextBox.Text != string.Empty)
            {
                ContactsDataGrid.ItemsSource = ContactManager.RechercherContacts(CritereComboBox.Text, CritereTextBox.Text);
            }
        }
    }
}
