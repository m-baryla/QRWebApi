using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QRWebApi.Models
{
    public partial class Wiki
    {
        public int Id { get; set; }
        public int IdLocation { get; set; }
        public int IdEquipment { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }

        public virtual DictEquipment IdEquipmentNavigation { get; set; }
        public virtual DictLocation IdLocationNavigation { get; set; }
    }
}
