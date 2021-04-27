using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public int IdLocation { get; set; }
        public int IdEquipment { get; set; }
        public int IdStatus { get; set; }
        public int? IdEmailAdress { get; set; }
        public bool? IsAnonymous { get; set; }

        public virtual DictEmailAdress IdEmailAdressNavigation { get; set; }
        public virtual DictEquipment IdEquipmentNavigation { get; set; }
        public virtual DictLocation IdLocationNavigation { get; set; }
        public virtual DictStatus IdStatusNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
