using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Data.Models
{
    public class FinanceData
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type  { get; set;}
        public double value { get; set;}
        public User User { get; set; }

    }
}
