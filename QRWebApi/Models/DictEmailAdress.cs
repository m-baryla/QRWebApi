using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class DictEmailAdress
    {
        public DictEmailAdress()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string EmailAdressNotify { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
