using MusicalogModel;
using MusicalogData.UnitOfWork;
using System;
using System.Collections.Generic;

namespace MusicalogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MusicalogService : IMusicalogService
    {
        /// <summary>
        /// Get a list of all Albums.
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAlbums()
        {
            List<Album> list = null;

            try
            {
                using (MusicalogUOW uow = new MusicalogUOW())
                {
                    if (uow != null)
                    {
                        list = uow.AlbumRepo.GetAll();
                    }
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return list;
        }

        /// <summary>
        /// Get an Album for the given AlbumID.
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        public Album GetAlbum(int albumID)
        {
            Album album = null;

            if (albumID > 0)
            {
                using (MusicalogUOW uow = new MusicalogUOW())
                {
                    if (uow != null)
                    {
                        album = uow.AlbumRepo.GetSingleByID(albumID);
                    }
                }
            }

            return album;
        }

        /// <summary>
        /// Save a new or existing Album.
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public bool SaveAlbum(Album album)
        {
            bool result = false;

            if (album == null)
            {
                // Should return a Fault Contract to client containing error. For now return false flag.
                return result;
            }

            try
            {
                using (MusicalogUOW uow = new MusicalogUOW())
                {
                    if (uow != null)
                    {
                        if (album.AlbumID > 0)
                        {
                            var existingAlbum = uow.AlbumRepo.GetSingleByID(album.AlbumID);
                            if (existingAlbum != null)
                            {
                                existingAlbum.AlbumLabel = album.AlbumLabel;
                                existingAlbum.AlbumName = album.AlbumName;
                                existingAlbum.Artist = album.Artist;
                                existingAlbum.Stock = album.Stock;
                                existingAlbum.AlbumType = album.AlbumType;
                            }
                            result = uow.AlbumRepo.Edit(existingAlbum);
                        }
                        else
                        {
                            result = uow.AlbumRepo.Add(album);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Delete an Album for the given AlbumID.
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        public bool DeleteAlbum(int albumID)
        {
            bool result = false;

            if (albumID > 0)
            {
                try
                {
                    using (MusicalogUOW uow = new MusicalogUOW())
                    {
                        if (uow != null)
                        {
                            uow.AlbumRepo.DeleteByID(albumID);
                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                }
            }

            return result;
        }

        #region "Dispose Methods"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MusicalogService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
