using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Reader : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Birthdate { get; set; }
        public string HomeAdress { get; set; }
        public bool Problematic { get; set; }

    }
}
