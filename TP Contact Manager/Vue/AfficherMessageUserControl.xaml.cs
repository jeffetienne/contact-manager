using System.Collections.Generic;
using System.Windows.Controls;
using BLL;
using Model;

namespace Vue
{
    /// <summary>
    /// Interaction logic for AfficherMessageUserControl.xaml
    /// </summary>
    public partial class AfficherMessageUserControl : UserControl
    {
        public AfficherMessageUserControl()
        {
            InitializeComponent();
           
            List<MessageViewModel> messages = MessageManager.VoirMessages();
            lvUsers.ItemsSource = messages;
        }
    }
    
}
