using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webmvc.Models
{
[Table("Tbl_Customer")]
public class CustomerModel
{
    [Key]
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNo { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
}
}