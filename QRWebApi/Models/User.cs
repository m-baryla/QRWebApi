using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdPermission { get; set; }

        public virtual DictPermission IdPermissionNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
