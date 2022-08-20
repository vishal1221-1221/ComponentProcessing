using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ComponentProcessing.API.Models

{
    public class ComponentProcessingModel
    {
        [Key] 
        public long requestId { get;  set; }
        [Required]
        public string name { get; set; }
        [Required]
        public long contactNumber { get; set; }
        [Required]
        public string componentType { get; set; }
        [Required]
        public string componentName { get; set; }
        [Required]
        public int quantity { get; set; }

        public string description { get; set; }

        public long totalCharges { get; set; }

        public long packageCharges { get; set; }
        public long deliveryCharges { get; set; }

        public DateTime dateOfDelivery { get; set; }

        public string status { get; set; }

    }
}
