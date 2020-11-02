using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationYear { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
