using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalogModel
{
    public interface IAlbum
    {
        int AlbumID { get; set; }
        string AlbumName { get; set; }
        string Artist { get; set; }
        string AlbumLabel { get; set; }
        int AlbumType { get; set; }
        int Stock { get; set; }
    }
}
