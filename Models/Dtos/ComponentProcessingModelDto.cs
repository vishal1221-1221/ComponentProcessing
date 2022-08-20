using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ComponentProcessing.API.Models.Dtos
{
    public class ComponentProcessingModelDto
    {

        public long requestId { get; set; }

        public long totalCharges { get; set; }

        public long packageCharges { get; set; }
        public long deliveryCharges { get; set; }

        public DateTime dateOfDelivery { get; set; }

        public string status { get; set; }

    }
}
