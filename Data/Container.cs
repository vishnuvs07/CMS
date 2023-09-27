using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Data
{
    [Table("Container")]
    public class Container
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Container Number")]
        public string ContainerNumber { get; set; }
        [Required]
        [Display(Name = "Shipment Date")]
        [DataType(DataType.Date)]
        public DateTime ShipmentDate { get; set; }
        [Required]
        [Display(Name = "Origin Port")]
        public string OriginPort { get; set; }
        [Required]
        [Display(Name = "Destination Port")]
        public string DestinatiionPort { get; set; }
        [Required]
        public string Status { get; set; }
        [Display(Name = "Container Documentation")]
        public string DocumentFilePath { get; set; } = "";
    }
}
