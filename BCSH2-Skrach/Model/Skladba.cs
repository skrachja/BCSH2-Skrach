using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Skrach.Model
{
    using System;

    public class Skladba
    {
        public int Id { get; set; }

        public string Nazev { get; set; }

        public string InterpretId { get; set; }

        public int? AlbumID { get; set; }

        public Interpret Interpreter { get; set; }
        public Album Album { get; set; }

        public Skladba()
        {
            Interpreter = new Interpret();
            Album = new Album();
        }

        public override string ToString()
        {
            return $"{Nazev} - {InterpretId}";
        }
    }

}
