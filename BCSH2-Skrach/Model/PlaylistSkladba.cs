using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Skrach.Model
{
    public class PlaylistSkladba
    {
        public int Id { get; set; }

        public int PlaylistId { get; set; }

        public int SkladbaId { get; set; }

        public string DatumNahrani { get; set; }

        public override string ToString()
        {
            return $"PlaylistId: {PlaylistId}, SkladbaId: {SkladbaId}";
        }
    }

}
