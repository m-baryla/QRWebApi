using System;
using System.Collections.Generic;

#nullable disable

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
