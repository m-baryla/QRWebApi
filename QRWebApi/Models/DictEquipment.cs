using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class DictEquipment
    {
        public DictEquipment()
        {
            Tickets = new HashSet<Ticket>();
            Wikis = new HashSet<Wiki>();
        }

        public int Id { get; set; }
        public string EquipmentName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Wiki> Wikis { get; set; }
    }
}
