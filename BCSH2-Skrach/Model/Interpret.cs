using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Skrach.Model
{
    public class Interpret
    {
        public int Id { get; set; }

        public string Jmeno { get; set; }

        public override string ToString()
        {
            return Jmeno;
        }
    }

}
