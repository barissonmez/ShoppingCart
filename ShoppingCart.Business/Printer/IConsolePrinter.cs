namespace ShoppingCart.Business.Printer
{
    public interface IConsolePrinter<in T> : IPrinter
    {
        void Print(T itemToPrint);
    }
}
