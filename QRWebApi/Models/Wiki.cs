using System;
using System.Collections.Generic;

#nullable disable

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
