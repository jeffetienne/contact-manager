using System.Windows;
using System.Windows.Controls;
using BLL;
using Model;

namespace Vue
{
    /// <summary>
    /// Interaction logic for FavorisUserControl.xaml
    /// </summary>
    public partial class FavorisUserControl : UserControl
    {
        public FavorisUserControl()
        {
            InitializeComponent();
            FavorisDataGrid.ItemsSource = FavoriManager.GetFavoris();
        }
        
        private void EnleverButton_Click(object sender, RoutedEventArgs e)
        {
            if (FavorisDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment enlever ce contact des favoris?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ContactViewModel contactViewModel = (ContactViewModel)FavorisDataGrid.SelectedItem;
                    string message = FavoriManager.Supprimerfavori(contactViewModel.Id);
                    MessageBox.Show(message, "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une ligne!!!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
