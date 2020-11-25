using MusicalogWeb.ViewModels;
using MusicalogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicalogWeb.Properties;
using MusicalogWeb.MusicalogService;

namespace MusicalogWeb.Services
{
    public class AlbumService : IAlbumService
    {
        private MusicalogServiceClient _MusicalogServiceClient;

        public AlbumService(MusicalogServiceClient musicalogServiceClient)
        {
            // Initialise dependency injections.
            _MusicalogServiceClient = musicalogServiceClient;
        }

        /// <summary>
        /// Get a list of albums using WCF service.
        /// </summary>
        /// <returns></returns>
        public AlbumListVM GetAlbumList()
        {
            AlbumListVM model = new AlbumListVM();

            try
            {
                int pageSize = Settings.Default.DefAlbumPageSize;

                // Get existing full album list from Session.
                IList<AlbumVM> albumVMList = HttpContext.Current.Session["AlbumList"] != null ? (IList<AlbumVM>)HttpContext.Current.Session["AlbumList"] : null;

                // Get album list changed flag from Session to determine if Sort By or Pagination buttons have been selected which will use existing album list.
                bool UseExistingAlbumList = HttpContext.Current.Session["UseExistingAlbumList"] != null ? (bool)HttpContext.Current.Session["UseExistingAlbumList"] : false;

                if (UseExistingAlbumList && albumVMList != null && albumVMList.Count > 0)
                {
                    // Get current sort by column ID and list page number from Session.
                    int sortColumnID = HttpContext.Current.Session["SortColumnID"] != null ? (int)HttpContext.Current.Session["SortColumnID"] : 1;
                    int pageNum = HttpContext.Current.Session["PageNum"] != null ? (int)HttpContext.Current.Session["PageNum"] : 1;

                    // Number of albums to skip passed in sorted album list to get the current page of albums.
                    int skipAlbums = (pageNum - 1) * pageSize;

                    // Sort album list by the current selected Sort By column selection or by Album Name by default.
                    switch (sortColumnID)
                    {
                        case 2:
                            model.AlbumList = (from p in albumVMList orderby p.Artist ascending select p).Skip(skipAlbums).Take(pageSize).ToList();
                            break;
                        case 3:
                            model.AlbumList = (from p in albumVMList orderby p.AlbumType ascending select p).Skip(skipAlbums).Take(pageSize).ToList();
                            break;
                        case 4:
                            model.AlbumList = (from p in albumVMList orderby p.Stock ascending select p).Skip(skipAlbums).Take(pageSize).ToList();
                            break;
                        default:
                            model.AlbumList = (from p in albumVMList orderby p.AlbumName ascending select p).Skip(skipAlbums).Take(pageSize).ToList();
                            break;
                    }

                    model.PageNum = pageNum;
                }
                else
                {
                    // Remove current album list from Session.
                    HttpContext.Current.Session.Remove("AlbumList");

                    var list = _MusicalogServiceClient.GetAlbums();

                    if (list != null)
                    {
                        albumVMList = new List<AlbumVM>();
                        AlbumVM albumVM = null;

                        string albumType1 = Settings.Default.AlbumType1Desc;
                        string albumType2 = Settings.Default.AlbumType2Desc;

                        // Copy all Album models to Album view model and add to album VM list.
                        foreach (var item in list)
                        {
                            albumVM = new AlbumVM()
                            {
                                AlbumID = item.AlbumID,
                                AlbumName = item.AlbumName,
                                AlbumType = item.AlbumType,
                                AlbumTypeDesc = item.AlbumType == 1 ? albumType1 : albumType2,
                                AlbumLabel = item.AlbumLabel,
                                Stock = item.Stock,
                                Artist = item.Artist
                            };

                            albumVMList.Add(albumVM);
                        }

                        // Sort album list by album name by default and get first page of albums.
                        var sortedAlbumList = (from p in albumVMList orderby p.AlbumName ascending select p).Take(pageSize).ToList();

                        // Save Album List view model details.
                        model.AlbumList = sortedAlbumList;
                        model.PageNum = 1;

                        // Save album list and default page number to Session.
                        HttpContext.Current.Session.Add("AlbumList", sortedAlbumList);
                        HttpContext.Current.Session["PageNum"] = 1;
                    }
                }

                // Save current sorted album list to Session.
                HttpContext.Current.Session.Add("AlbumList", albumVMList);

                // Remove album list changed flag from Session so that Sort By or Pagination buttons must be selected again 
                // to use album list in Session, otherwise get a fresh album list from web services.
                HttpContext.Current.Session.Remove("UseExistingAlbumList");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get list of albums due to the following error: {ex.Message}");
            }

            return model;
        }

        /// <summary>
        /// Get an album for given album ID using WCF service.
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        public AlbumVM GetAlbum(int albumID)
        {
            AlbumVM albumVM = null;

            if (albumID > 0)
            {
                try
                {
                    var album = _MusicalogServiceClient.GetAlbum(albumID);

                    if (album != null)
                    {
                        albumVM = new AlbumVM()
                        {
                            AlbumID = album.AlbumID,
                            AlbumName = album.AlbumName,
                            AlbumType = album.AlbumType,
                            AlbumLabel = album.AlbumLabel,
                            Stock = album.Stock,
                            Artist = album.Artist
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get album details due to the following error: {ex.Message}");
                }
            }

            return albumVM;
        }

        /// <summary>
        /// Save a new or existing album for given album view model using WCF service.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveAlbum(AlbumVM model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Album", "Album view model was not provided to save.");
            }

            bool result = false;

            try
            {
                var album = new Album
                {
                    AlbumID = model.AlbumID,
                    AlbumName = model.AlbumName,
                    Artist = model.Artist,
                    AlbumLabel = model.AlbumLabel,
                    AlbumType = model.AlbumType,
                    Stock = model.Stock
                };

                result = _MusicalogServiceClient.SaveAlbum(album);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save album details because the following error occurred: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Delete an album for given album ID using WCF service.
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
                    result = _MusicalogServiceClient.DeleteAlbum(albumID);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to delete album because the following error occurred: {ex.Message}");
                }
            }

            return result;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_MusicalogServiceClient != null)
                    {
                        _MusicalogServiceClient = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AlbumService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}