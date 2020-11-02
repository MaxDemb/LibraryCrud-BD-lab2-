using DAL.Models.Base;
using System;

namespace DAL.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool Is_Woman { get; set; }
    }
}
