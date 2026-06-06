// Address class - contains street, city, state/province, and country
class Address
{
    private string street;
    private string city;
    private string stateProvince;
    private string country;

    public Address(string street, string city, string stateProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateProvince = stateProvince;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToLower() == "usa" || country.ToLower() == "united states";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {stateProvince}\n{country}";
    }
}

// Customer class - contains name and address
class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }
}

// Product class - contains name, product id, price, and quantity
class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return price * quantity;
    }

    public string GetName()
    {
        return name;
    }

    public string GetProductId()
    {
        return productId;
    }
}

// Order class - contains list of products and a customer
class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (Product product in products)
        {
            total += product.GetTotalCost();
        }
        
        // Add shipping cost
        if (customer.LivesInUSA())
        {
            total += 5; // $5 shipping for USA
        }
        else
        {
            total += 35; // $35 shipping for international
        }
        
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "PACKING LABEL\n";
        label += "=============\n";
        foreach (Product product in products)
        {
            label += $"Product: {product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        string label = "SHIPPING LABEL\n";
        label += "===============\n";
        label += $"Customer: {customer.GetName()}\n";
        label += $"Address:\n{customer.GetAddress().GetFullAddress()}\n";
        return label;
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address usaAddress1 = new Address("123 Main Street", "New York", "NY", "USA");
        Address usaAddress2 = new Address("456 Oak Avenue", "Los Angeles", "CA", "USA");
        Address internationalAddress = new Address("789 Maple Road", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Smith", usaAddress1);
        Customer customer2 = new Customer("Sarah Johnson", usaAddress2);
        Customer customer3 = new Customer("Michael Brown", internationalAddress);

        // Create Order 1 (USA customer)
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P1001", 999.99, 1));
        order1.AddProduct(new Product("Wireless Mouse", "P1002", 29.99, 2));
        order1.AddProduct(new Product("USB Cable", "P1003", 12.99, 3));

        // Create Order 2 (USA customer)
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Smartphone", "P2001", 699.99, 1));
        order2.AddProduct(new Product("Phone Case", "P2002", 24.99, 2));

        // Create Order 3 (International customer - Canada)
        Order order3 = new Order(customer3);
        order3.AddProduct(new Product("Coffee Maker", "P3001", 79.99, 1));
        order3.AddProduct(new Product("Coffee Beans", "P3002", 15.99, 3));
        order3.AddProduct(new Product("Mug Set", "P3003", 29.99, 1));

        // Display Order 1
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine($"TOTAL PRICE: ${order1.GetTotalCost():F2}");
        Console.WriteLine(new string('-', 50) + "\n");

        // Display Order 2
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine($"TOTAL PRICE: ${order2.GetTotalCost():F2}");
        Console.WriteLine(new string('-', 50) + "\n");

        // Display Order 3
        Console.WriteLine(order3.GetShippingLabel());
        Console.WriteLine(order3.GetPackingLabel());
        Console.WriteLine($"TOTAL PRICE: ${order3.GetTotalCost():F2}");
        Console.WriteLine(new string('-', 50));
    }
}