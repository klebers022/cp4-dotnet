namespace CP4.Domain.Entities;

public class Customer
{
    private Guid Id {set; get;}
    private string Name {set; get;}
    private string? Email {set; get;}
    private string Adress {set; get;}

    public Customer(string name, string email, string adress)
    {
        Name = name;
        Email = email;
        Adress = adress;
    }
}