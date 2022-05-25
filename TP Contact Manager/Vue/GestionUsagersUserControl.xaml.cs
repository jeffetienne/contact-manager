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

namespace Vue
{
    enum Action
    {
        AJOUTER,
        MODIFIER
    }
    /// <summary>
    /// Interaction logic for GestionUsagersUserControl.xaml
    /// </summary>
    public partial class GestionUsagersUserControl : UserControl
    {
        private Action action;
        private int id;
        private Usager usager;
        public GestionUsagersUserControl()
        {
            InitializeComponent();
            refresh("");
        }

        public GestionUsagersUserControl(Usager usager)
        {
            InitializeComponent();
            refresh("");
            this.usager = usager;
        }

        private void refresh(string critere)
        {
            UsagersDataGrid.ItemsSource = UsagerManager.GetUsagers(critere);
        }

        private void SupprimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsagersDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cet usager?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    UsagerViewModel usager = (UsagerViewModel)UsagersDataGrid.SelectedItem;
                    string message = UsagerManager.SupprimerUsager(usager.Id);
                    refresh("");
                }
                
            }
        }

        private void RechercherButton_Click(object sender, RoutedEventArgs e)
        {
                refresh(CritereTextBox.Text);
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayComponent(true);
            RoleComboBox.ItemsSource = RoleManager.GetRoles();
            RoleComboBox.DisplayMemberPath = "Nom";
            RoleComboBox.SelectedValuePath = "Id";
            this.action = Action.AJOUTER;
            GestionUsagerLabel.Content = "Ajouter un usager";
        }

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayComponent(true);
            IdentifiantTextBox.IsEnabled = false;
            RoleComboBox.ItemsSource = RoleManager.GetRoles();
            RoleComboBox.DisplayMemberPath = "Nom";
            RoleComboBox.SelectedValuePath = "Id";
            this.action = Action.MODIFIER;

            if (UsagersDataGrid.SelectedItem != null)
            {
                UsagerViewModel usagerViewModel = (UsagerViewModel)UsagersDataGrid.SelectedItem;
                Usager usager = UsagerManager.GetUsager(usagerViewModel.Id);
                IdentifiantTextBox.Text = usager.Identifiant;
                RoleComboBox.SelectedValue = usager.Role;
                this.id = usager.Id;
                GestionUsagerLabel.Content = "Modifier cet usager";
            }
        }

        private void DisplayComponent(bool visible)
        {
            Visibility montrer = (visible ? Visibility.Visible : Visibility.Hidden);
            GestionUsagerLabel.Visibility = montrer;
            IdentifiantLabel.Visibility = montrer;
            IdentifiantTextBox.Visibility = montrer;
            RoleLabel.Visibility = montrer;
            RoleComboBox.Visibility = montrer;
            PasswordLabel.Visibility = montrer;
            PasswordPasswordBox.Visibility = montrer;
            SauvegarderButton.Visibility = montrer;
        }

        private void SauvegarderButton_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            message = ValiderChamps();
            if (message == string.Empty)
            {
                Usager usager = new Usager(this.id, IdentifiantTextBox.Text, (int)RoleComboBox.SelectedValue);
                
                if (this.action == Action.AJOUTER)
                {
                    message = ValiderData();
                    if (message == string.Empty)
                    {
                        message = UsagerManager.AjouterUsager(usager, PasswordPasswordBox.Password);

                        
                    }
                    else
                    {
                        MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (this.action == Action.MODIFIER)
                {
                    message = UsagerManager.ModifierUsager(usager, PasswordPasswordBox.Password);
                }
                MessageBox.Show(message);
                DisplayComponent(false);
                refresh("");
            }
            else
            {
                MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ValiderData()
        {
            string message = string.Empty;
            Usager usager = UsagerManager.GetUsager(IdentifiantTextBox.Text);
            if (usager != null)
            {
                message = "Il existe un contact ayant ce nom et ce prenom.\n";
            }
            else
            {
                message = string.Empty;
            }
            return message;
        }

        private string ValiderChamps()
        {
            string[] messages = new string[3];
            string message = string.Empty;
            if (IdentifiantTextBox.Text == string.Empty)
            {
                messages[0] = "L'identifiant est obligatoire.\n";
            }
            else
            {
                messages[0] = string.Empty;
            }

            if (RoleComboBox.Text == string.Empty)
            {
                messages[1] = "Le role est obligatoire.\n";
            }
            else
            {
                messages[1] = string.Empty;
            }

            if (PasswordPasswordBox.Password == string.Empty)
            {
                messages[2] = "Le mot de passe est obligatoire.\n";
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

        private void CritereTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            refresh(CritereTextBox.Text);
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleComboBox.SelectedIndex > -1)
            {
                if ((int)RoleComboBox.SelectedValue == 2 || (int)RoleComboBox.SelectedValue == 3)
                {
                    if (this.usager.Role != 2)
                    {
                        MessageBox.Show("Seul un Gestionnaire a le droit d'ajouter un Administrateur ou un Gestionnaire.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Hand);
                        RoleComboBox.SelectedIndex = -1;
                    }
                }
                
            }
        }
    }
}
