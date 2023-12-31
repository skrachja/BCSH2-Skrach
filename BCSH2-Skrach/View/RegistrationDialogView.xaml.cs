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
    /// Interakční logika pro RegistrationDialogView.xaml
    /// </summary>
    public partial class RegistrationDialogView : Window
    {
        public RegistrationDialogView()
        {
            InitializeComponent();
            DataContext = new RegistrationDialogViewModel();
        }
    }
}
