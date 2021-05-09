using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRWebApi.Models
{
    public class DictEmailAdressDetails
    {
        public string EmailAdressNotify { get; set; }
        public string Subject { get; set; }
        public string Content_part1 { get; set; }
        public string Content_part2 { get; set; }
        public string Content_part3 { get; set; }
    }
}
