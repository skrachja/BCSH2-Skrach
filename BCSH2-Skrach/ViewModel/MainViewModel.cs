using BCSH2_Skrach.Model;
using BCSH2_Skrach.View;
using BCSH2_Skrach.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Dapper;
using System.Reflection;
using System.Windows.Threading;

namespace BCSH2_Skrach

{
    public class MainViewModel : ViewModelBase
    {

        private Playlist _selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                _selectedPlaylist = value;
                OnPropertyChanged(nameof(SelectedPlaylist));
                IsEditButtonVisible = value != null;
                IsDeleteButtonVisible = value != null;
                FetchSongsFromPlaylist(value);
            }
        }

        private bool _isEditButtonVisible;
        public bool IsEditButtonVisible
        {
            get { return _isEditButtonVisible; }
            set
            {
                _isEditButtonVisible = value;
                OnPropertyChanged(nameof(IsEditButtonVisible));
            }
        }

        private bool _isDeleteButtonVisible;
        public bool IsDeleteButtonVisible
        {
            get { return _isDeleteButtonVisible; }
            set
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged(nameof(IsDeleteButtonVisible));
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        private Uzivatel Uzivatel { get; }
        public ICommand PlaylistButtonClick { get; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand InitializeDataCommand { get; }
        public ICommand AddSongToDbCommand { get; private set; }
        public ICommand AddSongCommand { get; }

        public ICommand DeleteSongCommand { get; }
        public ObservableCollection<Playlist> Playlists { get; }


        public MainViewModel(Window mainWindow, Uzivatel uzivatel)
        {
            Uzivatel = uzivatel;
            Playlists = new ObservableCollection<Playlist>();
            PlaylistButtonClick = new RelayCommand(ExecutePlaylistButtonClick);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            AddSongToDbCommand = new RelayCommand(AddSongToDb);
            AddSongCommand = new RelayCommand(AddSong);
            DeleteCommand = new RelayCommand(DeleteSong);
            if (Uzivatel.Opravneni == 10)
            {
                IsAdmin = true;
            }
            else IsAdmin = false;
            IsEditButtonVisible = false;
            IsDeleteButtonVisible = false;
            InitializeDataCommand = new RelayCommand(InitializeData);

            // Pokud není nastaven delay, dochází po inicializaci k database close
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };

            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                InitializeData(null);
            };

            timer.Start();
        }

        private void InitializeData(object obj)
        {
            FetchPlaylists();
        }
        private void ExecutePlaylistButtonClick(object parameter)
        {
            var createPlaylistWindow = new CreatePlaylistWindow
            {
                DataContext = new CreatePlaylistViewModel()
            };

            bool? result = createPlaylistWindow.ShowDialog();

            if (result == true)
            {
                var viewModel = createPlaylistWindow.DataContext as CreatePlaylistViewModel;

                var playlist = new Playlist
                {
                    Nazev = viewModel.PlaylistName,
                    AutorId = Uzivatel.Id,
                    DatumNahrani = DateTime.Now.ToString(),
                    Obrazek = viewModel.PlaylistPicture
                };

                // Add the new playlist to the ObservableCollection
                Playlists.Add(playlist);

                // Save the new playlist to the database
                DatabaseConnector connector = new DatabaseConnector();
                connector.Connect();
                string sqlInsert = "INSERT INTO Playlist (nazev, autor_id, datum_nahrani, obrazek) VALUES (@nazev, @autorId, @datumNahrani, @obrazek)";
                connector.ExecuteNonQuery(sqlInsert, new SQLiteParameter("@nazev", playlist.Nazev), new SQLiteParameter("@autorId", playlist.AutorId), new SQLiteParameter("@datumNahrani", playlist.DatumNahrani), new SQLiteParameter("@obrazek", playlist.Obrazek));
                connector.Disconnect();
                FetchPlaylists();
            }
        }
        private void FetchSongsFromPlaylist(Playlist playlist)
        {
            if (playlist != null)
            {
                int idPlaylist = playlist.Id;
                string sqlQuery = @"
SELECT S.*, I.*, A.*
FROM Playlist_skladba AS PS
LEFT JOIN Skladba AS S ON PS.id_skladba = S.id
LEFT JOIN Interpret AS I ON S.interpret_id = I.id
LEFT JOIN Album AS A ON S.album_id = A.id
WHERE PS.id_playlist = @idPlaylist";

                using (var connector = new DatabaseConnector())
                {
                    connector.Connect();
                    var songs = connector.Connection.Query<Skladba, Interpret, Album, Skladba>(
                        sqlQuery,
                        (song, interpreter, album) =>
                        {
                            if (song.Interpreter == null)
                            {
                                song.Interpreter = interpreter;
                            }

                            if (song.Album == null)
                            {
                                song.Album = album;
                            }

                            return song;
                        },
                        splitOn: "id, id, id",
                        param: new { idPlaylist = idPlaylist }
                    );
                    playlist.Songs = new ObservableCollection<Skladba>(songs);
                    connector.Disconnect();
                }
            }
        }
        private void FetchPlaylists()
        {
            Playlists.Clear();
            int idUzivatel = Uzivatel.Id;
            string sqlQuery = "SELECT * FROM Playlist WHERE autor_id = @userId";

            using (DatabaseConnector connector = new DatabaseConnector())
            {
                connector.Connect();
                using (SQLiteDataReader reader = connector.ExecuteQuery(sqlQuery, new SQLiteParameter("@userId", idUzivatel)))
                {
                    while (reader.Read())
                    {
                        var playlist = new Playlist
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Nazev = reader.GetString(reader.GetOrdinal("nazev")),
                            AutorId = reader.GetInt32(reader.GetOrdinal("autor_id")),
                            DatumNahrani = reader.GetString(reader.GetOrdinal("datum_nahrani")),
                            Obrazek = (byte[])reader["obrazek"]
                        };

                        Playlists.Add(playlist);
                    }
                }
            }
        }

        private void Edit(object param)
        {
            MessageBox.Show("Edit");
            var playlist = _selectedPlaylist;
            if (playlist != null)
            {
                var createPlaylistWindow = new CreatePlaylistWindow
                {
                    DataContext = new CreatePlaylistViewModel(playlist)
                };

                bool? result = createPlaylistWindow.ShowDialog();

                if (result == true)
                {
                    var viewModel = createPlaylistWindow.DataContext as CreatePlaylistViewModel;

                    int idPlaylist = _selectedPlaylist.Id;
                    string nazev = viewModel.PlaylistName;
                    string datumNahrani = DateTime.Now.ToString();
                    byte[] obrazek = viewModel.PlaylistPicture;

                    string sqlUpdate = "UPDATE Playlist SET nazev = @nazev, datum_nahrani = @datumNahrani, obrazek = @obrazek WHERE id = @id";

                    using (DatabaseConnector connector = new DatabaseConnector())
                    {
                        connector.Connect();
                        connector.ExecuteNonQuery(sqlUpdate, new SQLiteParameter("@nazev", nazev), new SQLiteParameter("@datumNahrani", datumNahrani), new SQLiteParameter("@obrazek", obrazek), new SQLiteParameter("@id", idPlaylist));
                    }

                    Playlists.Remove(playlist);
                    Playlist newPlaylist = new Playlist
                    {
                        Id = idPlaylist,
                        Nazev = nazev,
                        AutorId = Uzivatel.Id,
                        DatumNahrani = datumNahrani,
                        Obrazek = obrazek
                    };
                    Playlists.Add(newPlaylist);
                    FetchPlaylists();

                }
            }
        }
        private void Delete(object param)
        {
            MessageBox.Show("Delete");

            var playlist = _selectedPlaylist;
            if (playlist != null)
            {
                int idPlaylist = _selectedPlaylist.Id;

                string sqlDelete = "DELETE FROM Playlist WHERE id = @id";

                using (DatabaseConnector connector = new DatabaseConnector())
                {
                    connector.Connect();
                    connector.ExecuteNonQuery(sqlDelete, new SQLiteParameter("@id", idPlaylist));
                }

                Playlists.Remove(_selectedPlaylist);
                FetchPlaylists();

            }
        }

        private void AddSongToDb(object param)
        {
            var createPlaylistWindow = new AddSongWindow()
            {
                DataContext = new AddSongViewModel()
            };

            bool? result = createPlaylistWindow.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Vloženo");
            }
        }
        private void AddSong(object param)
        {
            var addSongToListWindow = new AddSongToListWindow(_selectedPlaylist);


            bool? result = addSongToListWindow.ShowDialog();

            if (result == true)
            {

            }
        }

        private void DeleteSong(object param)
        {

        }

    }
}
