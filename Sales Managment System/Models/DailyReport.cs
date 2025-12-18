using System.ComponentModel.DataAnnotations;

namespace Sales_Managment_System.Models
{
    public class DailyReport
    {
        [Key]
        public Guid Id { get; set; }   // ✅ Primary Key
        public DateOnly Date { get; set; }
        public double Sum { get; set; }
        public int CarNumbers { get; set; }
    }
}