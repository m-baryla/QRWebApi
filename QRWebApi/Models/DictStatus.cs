using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class DictStatus
    {
        public DictStatus()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
