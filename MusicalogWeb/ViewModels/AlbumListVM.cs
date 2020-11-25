using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicalogWeb.ViewModels
{
    public class AlbumListVM
    {
        public IList<AlbumVM> AlbumList { get; set; }
        public int PageNum { get; set; }
        public string ErrorMsg { get; set; }
    }
}