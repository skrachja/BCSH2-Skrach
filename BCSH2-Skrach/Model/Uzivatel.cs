using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Skrach.Model
{
    public class Uzivatel
    {
        public Uzivatel(int id, int opravneni, string jmeno, string prijmeni, string username, string heslo)
        {
            Id = id;
            Opravneni = opravneni;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Username = username;
            Heslo = heslo;
        }

        public int Id { get; set; }

        public int Opravneni { get; set; }

        public string Jmeno { get; set; }

        public string Prijmeni { get; set; }

        public string Username { get; set; }

        public string Heslo { get; set; }

        public override string ToString()
        {
            return $"{Jmeno} {Prijmeni} ({Username})";
        }


    }

}
