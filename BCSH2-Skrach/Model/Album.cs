using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Skrach.Model
{

    public class Album
    {
        public int Id { get; set; }

        public string Nazev { get; set; }

        public DateTime DatumNahrani { get; set; }

        public int InterpretId { get; set; }

        public byte[] Obrazek { get; set; }

        public override string ToString()
        {
            return $"{Nazev} - {DatumNahrani.Year}";
        }
    }
}
