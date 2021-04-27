using System;
using System.Collections.Generic;

#nullable disable

namespace QRWebApi.Models
{
    public partial class TicketsHistory
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
    }
}
