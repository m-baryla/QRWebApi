using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QRWebApi.Models
{
    public partial class DictStatu
    {
        public DictStatu()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
