using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public string descripiton { get; set; }

    }
}
