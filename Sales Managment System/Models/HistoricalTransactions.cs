using System;
using System.ComponentModel.DataAnnotations;

namespace Sales_Managment_System.Models
{
    public class HistoricalTransactions
    {
        [Key]
        public Guid Id { get; set; }   // ✅ Primary Key

        public string? TransactionId { get; set; }
        public TimeOnly? Time { get; set; }
        public Service? Service { get; set; }
        public string? CarNumber { get; set; }
        public DateOnly Date { get; set; }
    }
}