// CreditCard.cs
using InterfacesLab.Payments;
public class CreditCard : IPayable
{
    public string Number { get; }

    public CreditCard(string number) => Number = number;

    public void Pay(decimal amount)
        => Console.WriteLine($"Оплата картой {Number}: {amount:C}");
}