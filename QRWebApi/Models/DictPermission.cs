using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QRWebApi.Models
{
    public partial class DictPermission
    {
        public DictPermission()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Permissions { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
