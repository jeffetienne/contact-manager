using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BLL;
using Model;

namespace Vue
{
    /// <summary>
    /// Interaction logic for AfficherContactUserControl.xaml
    /// </summary>
    public partial class AfficherContactUserControl : UserControl
    {
        public AfficherContactUserControl()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            
                List<ContactViewModel> contacts = ContactManager.AfficherContact();

                ListeContacts.ItemsSource = contacts;
            
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListeContacts.SelectedItem != null)
            {
                ContactViewModel contactViewModel = (ContactViewModel)ListeContacts.SelectedItem;
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
            if (ListeContacts.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce contact?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ContactViewModel contactViewModel = (ContactViewModel)ListeContacts.SelectedItem;
                    Contact contact = ContactManager.RechercherContact(contactViewModel.Id);
                    string message = ContactManager.SupprimerContact(contact.Id);
                    MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                    Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une ligne!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void FavoriButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListeContacts.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment ajouter ce contact aux favoris?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ContactViewModel contactViewModel = (ContactViewModel)ListeContacts.SelectedItem;
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

        private void ListeContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListeContacts.SelectedItem != null)
            {
                ContactViewModel contactViewModel = (ContactViewModel)ListeContacts.SelectedItem;
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
    }
}
