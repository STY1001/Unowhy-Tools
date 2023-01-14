




namespace Unowhy_Tools_WPF.Models.Data;

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string MailTo { get; set; }
    public bool IsMember { get; set; }
    public OrderStatus Status { get; set; }
}
