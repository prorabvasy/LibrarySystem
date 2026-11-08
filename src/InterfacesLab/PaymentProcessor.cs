using InterfacesLab.Payments;
public static class PaymentProcessor
{
    public static void ProcessPayment(IPayable method, decimal amount)
    {
        Console.WriteLine($"[PaymentProcessor] Сумма: {amount:C}");
        method.Pay(amount);
        Console.WriteLine("[PaymentProcessor] Платёж обработан.");
    }
}