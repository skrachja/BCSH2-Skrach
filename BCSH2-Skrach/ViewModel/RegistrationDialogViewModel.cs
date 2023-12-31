using BCSH2_Skrach.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BCSH2_Skrach
{
    public class RegistrationDialogViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        private RegistrationDialogView registrationDialogView;

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public RegistrationDialogViewModel()
        {
            OkCommand = new RelayCommand(OkAction);
            CancelCommand = new RelayCommand(CancelAction);
        }

        private void OkAction(object obj)
        {
            var button = obj as Button;
            if (button == null)
            {
                Debug.WriteLine("obj není Button");
                return;
            }

            var window = Window.GetWindow(button);
            if (window == null)
            {
                Debug.WriteLine("Není Window s Button");
                return;
            }

            window.DialogResult = true;
        }

        private void CancelAction(object obj)
        {
            var button = obj as Button;
            if (button == null)
            {
                Debug.WriteLine("obj není Button");
                return;
            }

            var window = button.TemplatedParent as Window;
            if (window == null)
            {
                Debug.WriteLine("button.TemplatedParent není Window");
                return;
            }

            window.DialogResult = false;
        }
    }

}
