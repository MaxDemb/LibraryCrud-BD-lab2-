using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Abonnement : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public int PenaltySum { get; set; }
        public DateTime Deadline { get; set; }

        public int ReaderId { get; set; }
        public int BookId { get; set; }

        public Reader Reader { get; set; }
        public Book Book { get; set; }
    }
}
