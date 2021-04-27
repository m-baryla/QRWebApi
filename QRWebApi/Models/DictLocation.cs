using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class DictLocation
    {
        public DictLocation()
        {
            Tickets = new HashSet<Ticket>();
            Wikis = new HashSet<Wiki>();
        }

        public int Id { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Wiki> Wikis { get; set; }
    }
}
