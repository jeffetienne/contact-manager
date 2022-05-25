using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vue
{
    /// <summary>
    /// Interaction logic for AjouterContactWindow.xaml
    /// </summary>
    public partial class AjouterContactWindow : Window
    {
        private int id;
        public AjouterContactWindow()
        {
            InitializeComponent();
        }

        public AjouterContactWindow(Contact contact)
        {
            InitializeComponent();
            GroupeComboBox.ItemsSource = GroupeManager.GetGroupes();
            GroupeComboBox.DisplayMemberPath = "Nom";
            GroupeComboBox.SelectedValuePath = "Id";
            this.id = contact.Id;
            NomTextBox.Text = contact.Nom;
            PrenomTextBox.Text = contact.Prenom;
            if (contact.NumeroGroupe != null)
            {
                GroupeComboBox.SelectedValue = contact.NumeroGroupe;
            }
            
            EmailTextBox.Text = contact.AdresseCourriel;
            TelephoneTextBox.Text = contact.Tel;
            AdresseTextBox.Text = contact.Adresse;
        }

        private void SauvegarderContactButton_Click(object sender, RoutedEventArgs e)
        {
            string erreurs = string.Empty;
            erreurs = this.ValiderChamps();
            if (erreurs == string.Empty)
            {
                string message = null;
                int? groupe = null;
                if (GroupeComboBox.SelectedValue != null)
                {
                    groupe = (int)GroupeComboBox.SelectedValue;
                }
                Contact contact = new Contact(id, NomTextBox.Text, PrenomTextBox.Text, groupe, EmailTextBox.Text, TelephoneTextBox.Text, AdresseTextBox.Text);

                message = ContactManager.ModifierContact(contact);

                MessageBox.Show(message);
                this.Close();
            }
            else
            {
                MessageBox.Show(erreurs, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

    }
}
