using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        const string CommandShowVendorProducts = "1";
        const string CommandShowBuyerProducts = "2";
        const string CommandShowAllProducts = "3";
        const string CommandBuyProduct = "4";
        const string CommandExit = "9";

        Vendor vendor = new Vendor();
        Buyer buyer = new Buyer();
        bool isRun = true;

        Console.WriteLine($"Menu: {CommandShowVendorProducts}-Show vendor products;");
        Console.WriteLine($"      {CommandShowBuyerProducts}-Show buyer products;");
        Console.WriteLine($"      {CommandShowAllProducts}-Show all product;");
        Console.WriteLine($"      {CommandBuyProduct}-Buy product;");
        Console.WriteLine($"      {CommandExit}-Exit;");

        while (isRun)
        {
            switch (Utils.ReadString("Your shois: "))
            {
                case CommandShowVendorProducts:
                    vendor.ShowProducts();
                    break;

                case CommandShowBuyerProducts:
                    buyer.ShowProducts();
                    break;

                case CommandShowAllProducts:
                    vendor.ShowProducts();
                    buyer.ShowProducts();
                    break;

                case CommandBuyProduct:
                    var product = buyer.BuyProduct(vendor);
                    vendor.SellProduct(product);
                    break;

                case CommandExit:
                    isRun = false;
                    break;
            }
        }
    }
}

static class Utils
{
    static public string ReadString(string text = "")
    {
        Console.Write(text + " ");
        var tempString = Console.ReadLine().ToLower();
        Console.WriteLine();
        return tempString;
    }
}

class Product
{
    public string Title { get; private set; }
    public int Price { get; private set; }

    public Product(string title = "", int price = 0)
    {
        Title = title;
        Price = price;
    }

    public void ShowInfo()
    {
        Console.WriteLine(Title + " " + Price + "$");
    }
}

class Personag
{
    protected List<Product> Products;
    public int Money { get; protected set; }

    protected void AddMoney(int value)
    {
        Money += value;
    }

    protected void SubtractMoney(int value)
    {
        Money -= value;
    }

    public void ShowAll()
    {
        foreach (var product in Products)
            product.ShowInfo();

        Console.WriteLine("Money: " + Money);
    }

    public Product GetProduct()
    {
        while (true)
        {
            string neededProduct = Utils.ReadString("What do you want to buy: ");

            foreach (var product in Products)
            {
                if (product.Title == neededProduct)
                    return product;
            }
        }
    }

    protected void AddProduct(Product product)
    {
        Products.Add(product);
    }

    protected void DeleteProduct(Product product)
    {
        Products.Remove(product);
    }
}

class Vendor : Personag
{
    public Vendor() : base()
    {
        Products = new List<Product>() {new Product("milk", 26), new Product("shugar", 17),
                                        new Product("pork", 48), new Product("salt", 19),
                                        new Product("peper", 11), new Product("meat", 52),
                                        new Product("ches", 44), new Product("oil", 19) }; ;
        Money = 0;
    }

    public void SellProduct(Product product)
    {
        AddMoney(product.Price);
        DeleteProduct(product);
    }

    public void ShowProducts()
    {
        Console.WriteLine("At the seller: ");
        ShowAll();
        Console.WriteLine();
    }
}

class Buyer : Personag
{
    public Buyer() : base()
    {
        Products = new List<Product>();
        Money = 100;
    }

    public Product BuyProduct(Vendor vendor)
    {
        var product = vendor.GetProduct();

        if (product.Price <= Money)
        {
            AddProduct(product);
            SubtractMoney(product.Price);
        }
        else
        {
            Console.WriteLine("Do not enought mony for this product");
        }

        return product;
    }

    public void ShowProducts()
    {
        Console.WriteLine("At the bayer: ");
        ShowAll();
        Console.WriteLine();
    }
}