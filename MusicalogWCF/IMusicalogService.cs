using MusicalogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MusicalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMusicalogService1" in both code and config file together.
    [ServiceContract]
    public interface IMusicalogService : IDisposable
    {
        [OperationContract]
        List<Album> GetAlbums();

        [OperationContract]
        Album GetAlbum(int albumID);

        [OperationContract]
        bool SaveAlbum(Album album);

        [OperationContract]
        bool DeleteAlbum(int albumID);
    }
}
