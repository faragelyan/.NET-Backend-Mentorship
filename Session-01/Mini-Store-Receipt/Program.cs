class Program
{
    static void Main(string[] args)
    {
        StoreService store = new StoreService();

        string customerName = store.FormatName();
        store.PrintCatalog();
        store.Shop();
        store.PrintReceipt(customerName);
    }
}
