using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Skrach.Model
{
    public class Playlist
    {

        public int Id { get; set; }

        public string Nazev { get; set; }

        public int AutorId { get; set; }

        public string DatumNahrani { get; set; }

        public byte[] Obrazek { get; set; }

        public ObservableCollection<Skladba> Songs { get; set; }


        public override string ToString()
        {
            return Nazev;
        }
    }

}
