using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Model
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Gallery> Galleries { get; set; }
    }
}
