﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
