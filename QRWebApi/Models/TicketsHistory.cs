using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
