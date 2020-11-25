using MusicalogData.Models;
using MusicalogData.Repository;
using MusicalogModel;
using System;

namespace MusicalogData.UnitOfWork
{
    public class MusicalogUOW : IMusicalogUOW, IDisposable
    {
        private MusicalogDBContext _DbContext { get; set; }

        public MusicalogUOW()
        {
            CreateDbContext();
        }

        #region Repositories

        private IRepository<Album> _AlbumRepo;

        public IRepository<Album> AlbumRepo
        {
            get
            {
                if (_AlbumRepo == null)
                {
                    _AlbumRepo = new MusicalogRepository<Album>(_DbContext);
                }
                return _AlbumRepo;
            }
        }

        #endregion

        protected void CreateDbContext()
        {
            _DbContext = new MusicalogDBContext();

            _DbContext.Configuration.LazyLoadingEnabled = false;
            _DbContext.Configuration.ValidateOnSaveEnabled = false;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_DbContext != null)
                {
                    _DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
