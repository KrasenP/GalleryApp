using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Model
{
    public class GalleryImages
    {
        [Key]
        public int Id { get; set; }

        [Required]        
        public string FileName { get; set; }

        public string Ext { get; set; }

        [ForeignKey(nameof(GalleryId))]
        public Guid GalleryId { get; set; }
        public Gallery Gallery { get; set; }
    }
}
