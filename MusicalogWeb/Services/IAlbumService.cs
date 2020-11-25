using MusicalogWeb.ViewModels;
using MusicalogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalogWeb.Services
{
    public interface IAlbumService : IDisposable
    {
        AlbumListVM GetAlbumList();
        AlbumVM GetAlbum(int albumID);
        bool SaveAlbum(AlbumVM album);
        bool DeleteAlbum(int albumID);
    }
}
