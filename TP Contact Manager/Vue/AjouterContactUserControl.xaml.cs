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
using Model;
using BLL;
using System.Text.RegularExpressions;

namespace Vue
{
    /// <summary>
    /// Interaction logic for AjouterContactUserControl.xaml
    /// </summary>
    public partial class AjouterContactUserControl : UserControl
    {
        public AjouterContactUserControl()
        {
            InitializeComponent();
            GroupeComboBox.ItemsSource = GroupeManager.GetGroupes();
            GroupeComboBox.DisplayMemberPath = "Nom";
            GroupeComboBox.SelectedValuePath = "Id";
            
        }

        private void SauvegarderContactButton_Click(object sender, RoutedEventArgs e)
        {
            string erreurs = string.Empty;
            erreurs = this.ValiderChamps();
            if (erreurs == string.Empty)
            {
                erreurs = this.ValiderData();
                if (erreurs == string.Empty)
                {
                    string message = null;
                    int? groupe = null;
                    if (GroupeComboBox.SelectedValue != null)
                    {
                        groupe = (int)GroupeComboBox.SelectedValue;
                    }
                    Contact contact = new Contact(NomTextBox.Text, PrenomTextBox.Text, groupe, EmailTextBox.Text, TelephoneTextBox.Text, AdresseTextBox.Text);

                    message = ContactManager.AjouterContact(contact);

                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show(erreurs, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(erreurs, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ValiderData()
        {
            string[] messages = new string[3];
            string message = string.Empty;
            if (ContactManager.ContactExiste(NomTextBox.Text, PrenomTextBox.Text))
            {
                messages[0] = "Il existe un contact ayant ce nom et ce prenom.\n";
            }
            else
            {
                messages[0] = string.Empty;
            }

            if (ContactManager.ContactExiste(TelephoneTextBox.Text))
            {
                messages[1] = "Il existe un contact ayant telephone.\n";
            }
            else
            {
                messages[1] = string.Empty;
            }

            if (ContactManager.ContactExists(EmailTextBox.Text))
            {
                messages[2] = "Il existe un contact ayant cet email.\n";
            }
            else
            {
                messages[2] = string.Empty;
            }
            foreach (string s in messages)
            {
                message += s;
            }

            return message;
        }
        private string ValiderChamps()
        {
            string[] messages = new string[4];
            string message = string.Empty;
            if (NomTextBox.Text == string.Empty)
            {
                messages[0] = "Le nom est obligatoire.\n";
            }
            else
            {
                messages[0] = string.Empty;
            }

            if (PrenomTextBox.Text == string.Empty)
            {
                messages[1] = "Le prenom est obligatoire.\n";
            }
            else
            {
                messages[1] = string.Empty;
            }

            if (TelephoneTextBox.Text == string.Empty)
            {
                messages[2] = "Le telephone est obligatoire.\n";
            }
            else
            {
                Regex regex = new Regex(@"\(?\d{3}\)*\d{3}-\d{4}");
                if (!regex.IsMatch(TelephoneTextBox.Text))
                {
                    messages[2] = "Le telephone n'est pas valide.\n";
                }
                else
                {
                    messages[2] = string.Empty;
                }
            }

            if (EmailTextBox.Text != string.Empty)
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!regex.IsMatch(EmailTextBox.Text))
                {
                    messages[3] = "L'email n'est pas valide.";
                }
                else
                {
                    messages[3] = string.Empty;
                }
            }
            foreach (string s in messages)
            {
                message += s;
            }
            return message;
        }

        private void NomTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NomTextBox.Text == string.Empty)
            {
                NomValidationLabel.Content = "Le nom est obligatoire.";
            }
            else
            {
                if (PrenomTextBox.Text != string.Empty)
                {
                    if (ContactManager.ContactExiste(NomTextBox.Text, PrenomTextBox.Text))
                    {
                        NomValidationLabel.Content = "Nom et prenom existants";
                        PrenomValidationLabel.Content = "Nom et prenom existants";
                    }
                    else
                    {
                        NomValidationLabel.Content = string.Empty;
                    }
                }
                else
                {
                    NomValidationLabel.Content = string.Empty;
                }
            }
        }

        private void PrenomTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PrenomTextBox.Text == string.Empty)
            {
                PrenomValidationLabel.Content = "Le prenom est obligatoire.";
            }
            else
            {
                if (NomTextBox.Text != string.Empty)
                {
                    if (ContactManager.ContactExiste(NomTextBox.Text, PrenomTextBox.Text))
                    {
                        NomValidationLabel.Content = "Nom et prenom existants";
                        PrenomValidationLabel.Content = "Nom et prenom existants";
                    }
                    else
                    {
                        PrenomValidationLabel.Content = string.Empty;
                    }
                }
                else
                {
                    PrenomValidationLabel.Content = string.Empty;
                }
            }
        }

        private void TelephoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TelephoneTextBox.Text == string.Empty)
            {
                TelephoneValidationLabel.Content = "Le telephone est obligatoire.";
            }
            else
            {
                Regex regex = new Regex(@"\(?\d{3}\)*\d{3}-\d{4}");
                if (!regex.IsMatch(TelephoneTextBox.Text))
                {
                    TelephoneValidationLabel.Content = "Le telephone n'est pas valide.\nformat (000)000-0000";
                }
                else if (ContactManager.ContactExiste(TelephoneTextBox.Text))
                {
                    TelephoneValidationLabel.Content = "Telephone existant";
                }
                else
                {
                    TelephoneValidationLabel.Content = string.Empty;
                }
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text != string.Empty)
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!regex.IsMatch(EmailTextBox.Text))
                {
                    EmailValidationLabel.Content = "L'email n'est pas valide.";
                }
                else if (ContactManager.ContactExists(EmailTextBox.Text))
                {
                    EmailValidationLabel.Content = "Email existant";
                }
                else
                {
                    EmailValidationLabel.Content = string.Empty;
                }
            }
            else
            {
                EmailValidationLabel.Content = string.Empty;
            }
        }
    }
}
