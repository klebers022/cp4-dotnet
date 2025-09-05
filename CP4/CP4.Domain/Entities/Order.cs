using CP4.Aplication.Enums;
using CP4.Domain.ValueObject;

namespace CP4.Domain.Entities;

public class Order {
    private readonly List<OrderItem> _items = new();
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Created;
    public Money Total { get; private set; } = Money.Zero;
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public Guid DeliveryId { get; private set; }

    public Order(Guid customerId) { CustomerId = customerId; }

    public void AddItem(string productId, string name, Money unitPrice, int qty) {
        if (qty <= 0) throw new ArgumentException("Quantidade inválida");
        _items.Add(new OrderItem(productId, name, unitPrice, qty));
        Total = Total + new Money(unitPrice.Amount * qty);
    }
    public void Confirm() {
        if (Status != OrderStatus.Created) throw new InvalidOperationException();
        if (_items.Count == 0) throw new InvalidOperationException("Sem itens");
        Status = OrderStatus.Confirmed;
    }
    public void Dispatch(Guid deliveryId, DateTime now) {
        if (Status != OrderStatus.Confirmed) throw new InvalidOperationException();
        DeliveryId = deliveryId;
        Status = OrderStatus.Dispatched;
    }
    public void MarkDelivered(DateTime now) {
        if (Status != OrderStatus.Dispatched) throw new InvalidOperationException();
        Status = OrderStatus.Delivered; }
    
    public void Cancel(string reason) {
        if (Status == OrderStatus.Delivered) throw new InvalidOperationException();
        Status = OrderStatus.Canceled;
    }
}