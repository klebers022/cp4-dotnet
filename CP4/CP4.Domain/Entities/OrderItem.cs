using CP4.Domain.ValueObject;

namespace CP4.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string ProductId { get; private set; }
    public string NameSnapshot { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public Guid? OrderId { get; private set; }

    private OrderItem() { }

    public OrderItem(string productId, string name, Money unitPrice, int quantity)
    {
        //VALIDAR SE ID ESTA NO BANCO DE DADOS AO INVES DE NULL
        if (string.IsNullOrWhiteSpace(productId))
            throw new ArgumentException("ProductId inválido");

        if (quantity <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero");

        ProductId = productId;
        NameSnapshot = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
        
    }

    public Money Subtotal() => UnitPrice * Quantity;
}