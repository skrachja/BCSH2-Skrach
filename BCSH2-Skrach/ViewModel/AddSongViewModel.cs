using BCSH2_Skrach.Model;
using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_Skrach.ViewModel
{
    class AddSongViewModel : ViewModelBase
    {
        private string _songName;
        public string SongName
        {
            get { return _songName; }
            set
            {
                if (_songName != value)
                {
                    _songName = value;
                    OnPropertyChanged("SongName");
                }
            }
        }

        private string _interpreter;
        public string Interpreter
        {
            get { return _interpreter; }
            set
            {
                if (_interpreter != value)
                {
                    _interpreter = value;
                    OnPropertyChanged("Interpreter");
                }
            }
        }

        private string _album;
        public string Album
        {
            get { return _album; }
            set
            {
                if (_album != value)
                {
                    _album = value;
                    OnPropertyChanged("Album");
                }
            }
        }

        private byte[] _songImage;
        public byte[] SongImage
        {
            get { return _songImage; }
            set
            {
                if (_songImage != value)
                {
                    _songImage = value;
                    OnPropertyChanged("SongImage");
                }
            }
        }

        public ICommand AddSongCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectImageCommand { get; }

        public AddSongViewModel()
        {
            AddSongCommand = new RelayCommand(AddSong);
            CancelCommand = new RelayCommand(CloseWindow);
            SelectImageCommand = new RelayCommand(SelectImage);
        }

        private async void AddSong(object obj)
        {
            using (var db = new DatabaseConnector())
            {
                db.Connect();

                // Check if the interpreter exists
                var interpreterId = CheckOrCreateInterpreter(db, Interpreter);

                await Task.Delay(500); // Delay for 0.5 seconds

                // Check if the album exists
                var albumId = CheckOrCreateAlbum(db, Album, interpreterId);

                await Task.Delay(500); // Delay for 0.5 seconds

                // Now we can add the song
                AddSongToDatabase(db, SongName, interpreterId, albumId);

                db.Disconnect();
            }

            var window = Window.GetWindow((DependencyObject)obj);
            if (window != null)
            {
                window.DialogResult = true;
            }
        }

        private int CheckOrCreateInterpreter(DatabaseConnector db, string interpreter)
        {
            // Check if the interpreter exists
            var interpreterId = db.ExecuteScalar<int?>("SELECT id FROM Interpret WHERE jmeno = @jmeno", new SQLiteParameter("@jmeno", interpreter));

            if (interpreterId == null)
            {
                // The interpreter doesn't exist, so create a new one
                db.ExecuteNonQuery("INSERT INTO Interpret (jmeno) VALUES (@jmeno)", new SQLiteParameter("@jmeno", interpreter));

                // Get the ID of the newly inserted interpreter
                interpreterId = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }

            return interpreterId.Value;
        }

        private int CheckOrCreateAlbum(DatabaseConnector db, string album, int interpreterId)
        {
            // Check if the album exists
            var albumId = db.ExecuteScalar<int?>("SELECT id FROM Album WHERE nazev = @nazev AND interpret_id = @interpret_id", new SQLiteParameter("@nazev", album), new SQLiteParameter("@interpret_id", interpreterId));

            if (albumId == null)
            {
                // The album doesn't exist, so create a new one
                db.ExecuteNonQuery("INSERT INTO Album (nazev, interpret_id, datum_nahrani, obrazek) VALUES (@nazev, @interpret_id, @datum_nahrani, @obrazek)", new SQLiteParameter("@nazev", album), new SQLiteParameter("@interpret_id", interpreterId), new SQLiteParameter("@datum_nahrani", DateTime.Now.ToString()), new SQLiteParameter("@obrazek", SongImage));

                // Get the ID of the newly inserted album
                albumId = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }

            return albumId.Value;
        }

        private void AddSongToDatabase(DatabaseConnector db, string songName, int interpreterId, int albumId)
        {
            // Now we can add the song
            db.ExecuteNonQuery("INSERT INTO Skladba (nazev, interpret_id, album_id) VALUES (@nazev, @interpret_id, @album_id)", new SQLiteParameter("@nazev", songName), new SQLiteParameter("@interpret_id", interpreterId), new SQLiteParameter("@album_id", albumId));
        }

        private void CloseWindow(object obj)
        {
            var window = Window.GetWindow((DependencyObject)obj);
            if (window != null)
            {
                window.DialogResult = false;
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
                _songImage = File.ReadAllBytes(dialog.FileName);
            }
        }
    }
}