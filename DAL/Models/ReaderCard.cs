using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ReaderCard : BaseEntity
    {
        public DateTime RegistrationDate { get; set; }
        public int BonusCount { get; set; }

        public int ReaderId { get; set; }

        public Reader Reader { get; set; }
    }
}
