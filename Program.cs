﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Vendor vendor = new Vendor();
        Buyer buyer = new Buyer();
        Changer changer = new Changer();
        Magazin magazin = new Magazin();

        magazin.Procces(vendor, buyer, changer);
    }
}

static class Utils
{
    static public string ReadString(string text = "")
    {
        Console.Write(text + " ");
        string tempString = Console.ReadLine().ToLower();
        Console.WriteLine();
        return tempString;
    }
}

class Product
{
    public Product(string title = "", int price = 0)
    {
        Title = title;
        Price = price;
    }

    public string Title { get; private set; }
    public int Price { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine(Title + " " + Price + "$");
    }
}

class Changer
{
    public void Trade(Vendor vendor, Buyer buyer)
    {
        if (vendor.TryGetProductForSell(out Product product) == false)
            return;

        buyer.BuyProduct(product);
        vendor.SellProduct(product);
    }
}

class Magazin
{
    public void Procces(Vendor vendor, Buyer buyer, Changer changer)
    {
        const string CommandShowVendorProducts = "1";
        const string CommandShowBuyerProducts = "2";
        const string CommandShowAllProducts = "3";
        const string CommandBuyProduct = "4";
        const string CommandExit = "9";

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
                    changer.Trade(vendor, buyer);
                    break;

                case CommandExit:
                    isRun = false;
                    break;
            }
        }
    }
}

class Person
{
    protected List<Product> _products;
    public int Money { get; protected set; }

    public virtual void ShowProducts()
    {
        foreach (var product in _products)
            product.ShowInfo();

        Console.WriteLine("Money: " + Money);
        Console.WriteLine();
    }
}

class Vendor : Person
{
    public Vendor() : base()
    {
        _products = new List<Product>() {new Product("milk", 26), new Product("shugar", 17),
                                        new Product("pork", 48), new Product("salt", 19),
                                        new Product("peper", 11), new Product("meat", 52),
                                        new Product("ches", 44), new Product("oil", 19) }; ;
        Money = 0;
    }

    public bool TryGetProductForSell(out Product product)
    {
        string neededProduct = Utils.ReadString("What do you want to buy: ");

        foreach (var prod in _products)
        {
            if (prod.Title == neededProduct)
            {
                product = prod;
                return true;
            }
        }

        product = null;
        return false;
    }

    public void SellProduct(Product product)
    {
        Money += product.Price;
        _products.Remove(product);
    }

    public override void ShowProducts()
    {
        Console.WriteLine("At the seller: ");
        base.ShowProducts();
    }
}

class Buyer : Person
{
    public Buyer() : base()
    {
        _products = new List<Product>();
        Money = 100;
    }

    public void BuyProduct(Product product)
    {
        if (product == null)
            return;

        if (product.Price <= Money)
        {
            AddProduct(product);
            SubtractMoney(product.Price);
        }
        else
        {
            Console.WriteLine("Do not enought mony for this product");
        }
    }

    public override void ShowProducts()
    {
        Console.WriteLine("At the bayer: ");
        base.ShowProducts();
    }

    private void SubtractMoney(int value)
    {
        Money -= value;
    }

    private void AddProduct(Product product)
    {
        _products.Add(product);
    }
}