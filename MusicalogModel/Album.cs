using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalogModel
{
    public class Album : IAlbum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumID { get; set; }
        [Required]
        [MaxLength(50)]
        public string AlbumName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Artist { get; set; }
        [Required]
        [MaxLength(50)]
        public string AlbumLabel { get; set; }
        [Required]
        public int AlbumType { get; set; }
        public int Stock { get; set; }
    }
}
