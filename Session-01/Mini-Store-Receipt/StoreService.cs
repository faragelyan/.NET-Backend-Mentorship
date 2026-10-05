class StoreService
{
    private readonly string[] _names = ["Keyboard", "Mouse", "Headset", "Monitor"];
    private readonly decimal[] _prices = [350.00m, 150.00m, 250.00m, 1200.00m];
    private readonly int[] _cart;

    public StoreService()
    {
        _cart = new int[_names.Length];
    }

    // 1. Welcome the customer
    public string FormatName()
    {
        Console.Write("Enter your name: ");
        string input = Console.ReadLine() ?? string.Empty;
        string cleaned = input.Trim();

        if (string.IsNullOrEmpty(cleaned))
        {
            return string.Empty;
        }

        // Strings are immutable, save returned string
        string formattedName = $"{char.ToUpper(cleaned[0])}{(cleaned.Length > 1 ? cleaned[1..].ToLower() : string.Empty)}";
        return formattedName;
    }

    // 2. Show the catalog
    public void PrintCatalog()
    {
        Console.WriteLine("\n--- Product Catalog ---");
        for (int i = 0; i < _names.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {_names[i]} - {_prices[i]:F2}");
        }
    }

    // Helper: Safe integer input without int.Parse
    public int ReadValidInt(string prompt, int min, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine("Invalid input. Please enter a valid positive number.");
        }
    }

    // 3. Let the customer shop
    public void Shop()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:\n1 = Add item\n2 = Checkout");
            int choice = ReadValidInt("Choose an option (1 or 2): ", 1, 2);

            if (choice == 2)
            {
                break;
            }

            int productIndex = ReadValidInt($"Select product number (1-{_names.Length}): ", 1, _names.Length) - 1;
            int quantity = ReadValidInt("Enter quantity: ", 1);

            _cart[productIndex] += quantity;
            Console.WriteLine($"Added {quantity} x {_names[productIndex]} to your cart.");
        }
    }

    // 4. Calculate subtotal
    public decimal CalculateSubtotal()
    {
        decimal subtotal = 0m;
        for (int i = 0; i < _cart.Length; i++)
        {
            subtotal += _prices[i] * _cart[i];
        }
        return subtotal;
    }

    // 4. Discount rate
    public decimal GetDiscountRate(decimal subtotal) => subtotal switch
    {
        >= 500m => 0.10m,
        >= 200m => 0.05m,
        _ => 0.00m
    };

    // 5. Print the receipt
    public void PrintReceipt(string customerName)
    {
        decimal subtotal = CalculateSubtotal();
        decimal discountRate = GetDiscountRate(subtotal);
        decimal discountAmount = subtotal * discountRate;
        decimal total = subtotal - discountAmount;

        Console.WriteLine("\n--- Receipt ---");
        Console.WriteLine($"Customer: {customerName}");

        int index = 0;
        foreach (string name in _names)
        {
            int quantity = _cart[index];
            if (quantity > 0)
            {
                decimal itemTotal = _prices[index] * quantity;
                Console.WriteLine($"{name,-8} x{quantity} = {itemTotal:F2}");
            }
            index++;
        }

        Console.WriteLine($"Subtotal:        {subtotal:F2}");
        Console.WriteLine($"Discount ({(int)(discountRate * 100)}%):  -{discountAmount:F2}");
        Console.WriteLine($"Total:            {total:F2}");
        Console.WriteLine(total >= 500m ? "FREE SHIPPING" : "Shipping: 50.00");
    }
}
