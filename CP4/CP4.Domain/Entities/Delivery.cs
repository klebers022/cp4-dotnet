namespace CP4.Domain.Entities;


public class Delivery
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid OrderId { get; private set; }
    public Guid? CourierId { get; private set; } 
    public DateTime? PickupAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }

    private Delivery() { }

    public Delivery(Guid orderId)
    {
        OrderId = orderId;
    }

    public void AssignCourier(Guid courierId)
    {
        if (CourierId is not null)
            throw new InvalidOperationException("Já existe um entregador atribuído.");

        CourierId = courierId;
    }

    public void MarkPickedUp(DateTime now)
    {
        if (PickupAt is not null)
            throw new InvalidOperationException("Pedido já foi retirado.");

        PickupAt = now;
    }

    public void MarkDelivered(DateTime now)
    {
        if (PickupAt is null)
            throw new InvalidOperationException("Não é possível entregar antes de retirar.");
        if (DeliveredAt is not null)
            throw new InvalidOperationException("Pedido já entregue.");

        DeliveredAt = now;
    }
}
