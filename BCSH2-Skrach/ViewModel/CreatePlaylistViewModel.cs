using BCSH2_Skrach.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BCSH2_Skrach.ViewModel
{
    class CreatePlaylistViewModel : ViewModelBase
    {
        private string _playlistName;
        public string PlaylistName
        {
            get { return _playlistName; }
            set
            {
                if (_playlistName != value)
                {
                    _playlistName = value;
                    OnPropertyChanged("PlaylistName");
                }
            }
        }
        private byte[] _playlistPicture;
        public byte[] PlaylistPicture
        {
            get { return _playlistPicture; }
            set
            {
                if (_playlistPicture != value)
                {
                    _playlistPicture = value;
                    OnPropertyChanged("SelectImageCommand");
                }
            }
        }
        public ICommand SelectImageCommand { get; }
        public ICommand CreatePlaylistCommand { get; }
        public ICommand CancelCommand { get; }

        public CreatePlaylistViewModel()
        {
            SelectImageCommand = new RelayCommand(SelectImage);
            CreatePlaylistCommand = new RelayCommand(CreatePlaylist);
            CancelCommand = new RelayCommand(CloseWindow);
        }
        public CreatePlaylistViewModel(Playlist playlist)
        {
            SelectImageCommand = new RelayCommand(SelectImage);
            CreatePlaylistCommand = new RelayCommand(CreatePlaylist);
            CancelCommand = new RelayCommand(CloseWindow);

            if (playlist != null)
            {
                PlaylistName = playlist.Nazev;
                PlaylistPicture = playlist.Obrazek; // This line remains unchanged
            }
        }


        private void SelectImage(object obj)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg;*.jpeg;*.gif;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                // Store the path to the image
                _playlistPicture = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void CreatePlaylist(object obj)
        {
            var window = Window.GetWindow((DependencyObject)obj);
            if (window != null)
            {
                window.DialogResult = true;
            }
        }

        private void CloseWindow(object obj)
        {
            var window = Window.GetWindow((DependencyObject)obj);
            if (window != null)
            {
                window.DialogResult = false;
            }
        }


    }
}