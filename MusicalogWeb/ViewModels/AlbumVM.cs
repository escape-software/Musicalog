using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicalogWeb.ViewModels
{
    public class AlbumVM
    {
        public int AlbumID { get; set; }

        [Required(ErrorMessage = "Album Name is required.")]
        [Display(Name = "Album Name: ")]
        [StringLength(50)]
        public string AlbumName { get; set; }

        [Required(ErrorMessage = "Artist is required.")]
        [Display(Name = "Artist: ")]
        [StringLength(50)]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Album Label is required.")]
        [Display(Name = "Album Label: ")]
        [StringLength(50)]
        public string AlbumLabel { get; set; }

        [Required(ErrorMessage = "Album Type is required.")]
        [Display(Name = "Album Type: ")]
        [Range(minimum: 1, maximum: 2, ErrorMessage = "Please Album Type (1=Vinyl, 2=CD).")]
        public int AlbumType { get; set; }

        public string AlbumTypeDesc { get; set; }

        [Display(Name = "Stock: ")]
        [Range(minimum: 0, maximum: 9999999, ErrorMessage = "Please enter a Stock between 0-9999999.")]
        public int Stock { get; set; }

        public string PanelTitle { get; set; }
    }
}