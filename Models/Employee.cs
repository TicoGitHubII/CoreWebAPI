using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebAPI.Models
{
    [Table("Employee")]

    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } = "";
        public string Department { get; set; } = "";
        public string EmailId { get; set; } = "";
        public DateTime DOJ { get; set; }
    }
}
