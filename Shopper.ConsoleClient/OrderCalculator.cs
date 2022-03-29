using Shopper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.ConsoleClient
{
    public class OrderCalculator
    {
        public decimal Calculate(Product product)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Calculating...");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Calculated.");

            return product.Price * 100;
        }

        public Task<decimal> CalculateAsync(Product product)
        {
            return Task.Run(() => Calculate(product));
        }


    }

    public class Printer
    {
        public void Print(string content)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Printing... {content}");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Printed.");
        }

        public Task PrintAsync(string content)
        {
            return Task.Run(()=>Print(content));
        }

    }
}
