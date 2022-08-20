using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.API.Models.Dtos
{
    public class GetDto
    {
        public string comType { get; set; }
        public int quantity { get; set; }
        public long packageCharges { get; set; }
        public long deliveryCharges { get; set; }
    }
}
