using BCSH2_Skrach.ViewModel;
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

namespace BCSH2_Skrach.View
{
    /// <summary>
    /// Interakční logika pro CreatePlaylistWindow.xaml
    /// </summary>
    public partial class CreatePlaylistWindow : Window
    {
        public CreatePlaylistWindow()
        {
            InitializeComponent();
            DataContext = new CreatePlaylistViewModel();
        }
    }
}
