using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KoDTicketingLibrary.DTO
{
    public class Promotion
    {
        public String PromotionCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal DiscountPercentage { get; set; }
        
        public String RegexValidator { get; set; }
        public String WebPromotionId { get; set; }

        public Int16 CO { get; set; }
        public Int16 DM { get; set; }
        public Int16 BZ { get; set; }
        public Int16 PL { get; set; }
        public Int16 SL { get; set; }
        public Int16 GL { get; set; }
    }
}
