using MusicalogWeb.Properties;
using MusicalogWeb.Services;
using MusicalogWeb.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace MusicalogWeb.Controllers
{
    public class AlbumController : BaseController
    {
        private IAlbumService _AlbumService;

        public AlbumController(IAlbumService albumService)
        {
            // Initialise dependency injections.
            _AlbumService = albumService;
        }

        /// <summary>
        /// Get list of Albums
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            AlbumListVM model = _AlbumService.GetAlbumList();

            // Set the page title in the master layout.
            ViewBag.Title = Settings.Default.AlbumPageTitle;

            // Create URLs to allow client to request for the album list, album edit, album delete and album list paging.
            ViewBag.AlbumListURL = Url.Action("Index");
            ViewBag.AlbumEditURL = Url.Action("AlbumEdit");
            ViewBag.AlbumDeleteURL = Url.Action("AlbumDelete");
            ViewBag.AlbumListPaginationURL = Url.Action("AlbumListPagination");

            return View(model);
        }

        [HttpGet]
        public ActionResult AlbumEdit(int albumID = 0)
        {
            AlbumVM model = new AlbumVM();

            // If editing an existing album, get existing ablum view model.
            if (albumID > 0)
            {
                model = _AlbumService.GetAlbum(albumID);
            }

            // Set the Album Details panel title depending on operation.
            model.PanelTitle = albumID > 0 ? "Album Edit" : "Album Create";

            // Create URL to allow client to request for the album list.
            ViewBag.AlbumListURL = Url.Action("Index");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumEdit(AlbumVM model)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                // Save new or existing album.
                status = _AlbumService.SaveAlbum(model);
            }
            else
            {
                // Set the panel title to add or edit mode.
                model.PanelTitle = model.AlbumID > 0 ? "Album Edit" : "Album Create";
            }

            // Remove use existing album list flag from Session which will cause a new album list to be obtained from web service.
            Session.Remove("UseExistingAlbumList");

            // Return AlbumEdit with validation errors if any.
            // If validation was successful and album was saved to database then client can redirect to Album List using provided URL.
            return JsonView(status, "AlbumEdit", model, Url.Action("AlbumList", "Album"));
        }

        /// <summary>
        /// Delete an Album for a given AlbumID.
        /// Using HttpGet instead of HttpDelete.
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AlbumDelete(int albumID = 0)
        {
            bool status = false;
            string errorMsg = "";

            if (albumID > 0)
            {
                // Delete album.
                status = _AlbumService.DeleteAlbum(albumID);
                if (!status)
                {
                    errorMsg = "Could not delete Album.";
                }
            }

            // Remove use existing album list flag from Session which will cause a new album list to be obtained from web service.
            Session.Remove("UseExistingAlbumList");

            // Return JSON result containing AlbumDelete status with any generated error message.
            return JsonActionResult(status, errorMsg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumSort(int selSortBy)
        {
            Session.Remove("UseExistingAlbumList");
            Session.Remove("SortColumnID");

            if (selSortBy > 0)
            {
                // Save the current selected Sort By column ID, page number and use existing album list flag to Session.
                // This will cause the Album List page to sort the existing list and display the first page.
                Session["SortColumnID"] = selSortBy;
                Session["PageNum"] = 1;
                Session["UseExistingAlbumList"] = true;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AlbumListPagination(int direction)
        {
            Session.Remove("UseExistingAlbumList");

            int pageNum = Session["PageNum"] != null ? (int)Session["PageNum"] : 1;
            IList<AlbumVM> albumVMlist = Session["AlbumList"] != null ? (IList<AlbumVM>)Session["AlbumList"] : null;
            int maxPage = 1;

            if (albumVMlist != null && albumVMlist.Count > 0)
            {
                // Calculate the maximum page number for album list.
                int pageSize = Settings.Default.DefAlbumPageSize;
                if (pageSize > 0)
                {
                    if (albumVMlist.Count % pageSize == 0)
                    {
                        maxPage = albumVMlist.Count / pageSize;
                    }
                    else
                    {
                        maxPage = (albumVMlist.Count / pageSize) + 1;
                    }
                }

                // Increment or decrement the page number.
                if (direction > 0)
                {
                    pageNum = pageNum + 1 > maxPage ? pageNum : pageNum+1;
                }
                else
                {
                    pageNum = pageNum > 1 ? pageNum-1 : pageNum;
                }

                // Save current page number and use existing album list flag to Session.
                // This will cause the new album list page to be displayed.
                Session["PageNum"] = pageNum;
                Session["UseExistingAlbumList"] = true;
            }

            return RedirectToAction("Index");
        }
    }
}