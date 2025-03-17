using System.ComponentModel.DataAnnotations;

namespace invoicesManagement.Models
{
    public class InvoiceModel
    {
        public int id { get; set; }

        [Required]
        public string invoiceName { get; set; }

        [Required]
        public string invoiceNumber { get; set; }

        [Required]
        public DateTime invoiceExpiration { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public int status { get; set; }

        [Required]
        public string invoiceTotal { get; set; }

        [Required]
        public string invoiceDescription { get; set; }
    }
}
