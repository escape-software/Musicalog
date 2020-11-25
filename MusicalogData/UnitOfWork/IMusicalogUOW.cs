using MusicalogData.Repository;
using MusicalogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalogData.UnitOfWork
{
    public interface IMusicalogUOW
    {
        IRepository<Album> AlbumRepo { get; }
    }
}
