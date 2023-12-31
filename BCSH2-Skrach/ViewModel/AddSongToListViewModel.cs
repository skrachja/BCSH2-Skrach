using BCSH2_Skrach.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Mapping;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_Skrach
{
    public class AddSongToListViewModel : ViewModelBase
    {
        private int idPlaylistu;
        private Skladba _selectedSong;
        public Skladba SelectedSong
        {
            get
            {
                return _selectedSong;
            }
            set
            {
                _selectedSong = value;
                OnPropertyChanged("SelectedSong");
            }
        }

        public ObservableCollection<Skladba> Songs { get; set; }

        public ICommand SelectSongChangedCommand { get; }


        public AddSongToListViewModel(Playlist playlist)
        {
            idPlaylistu = playlist.Id;
            DatabaseConnector _databaseConnector = new DatabaseConnector();
            _databaseConnector.Connect();
            var songs = _databaseConnector.Query<Skladba>("SELECT * FROM Skladba INNER JOIN Interpret ON Skladba.interpret_id = Interpret.id INNER JOIN Album ON Skladba.album_id = Album.id");
            Songs = new ObservableCollection<Skladba>(songs);
            SelectSongChangedCommand = new RelayCommand(SelectSong);
            _databaseConnector.Disconnect();
        }

        private void SelectSong(object obj)
        {
            if (_selectedSong != null)
            {
                var idPlaylist = idPlaylistu;
                var idSong = _selectedSong.Id;
                var currentDate = DateTime.Now.ToString();

                var sqlInsert = "INSERT INTO Playlist_skladba (id_playlist, id_skladba, datum_nahrani) VALUES (@idPlaylist, @idSong, @currentDate)";

                using (var connector = new DatabaseConnector())
                {
                    connector.Connect();
                    connector.ExecuteNonQuery(sqlInsert, new SQLiteParameter("@idPlaylist", idPlaylist), new SQLiteParameter("@idSong", idSong), new SQLiteParameter("@currentDate", currentDate));
                    connector.Disconnect();
                }
            }
            else
            {
                MessageBox.Show("Vyber skladbu.");
            }
        }
    }


}
