using Shopper.ConsoleClient;
using Shopper.Domain.Models;

Product product = new Product { Price = 10 };

OrderCalculator orderCalculator = new OrderCalculator();
Printer printer = new Printer();

Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Hello, World!");

// Synchroniczny
//decimal price1 = orderCalculator.Calculate(product);
//decimal price2 = orderCalculator.Calculate(product);
//decimal price3 = orderCalculator.Calculate(product);
//printer.Print($"{Thread.CurrentThread.ManagedThreadId} Cena: {price} ");

// Asynchroniczny (Task + ContitueWith)
//Task<decimal> calculateTask1 = new Task<decimal>(() => orderCalculator.Calculate(product));
//Task<decimal> calculateTask2 = new Task<decimal>(() => orderCalculator.Calculate(product));
//Task<decimal> calculateTask3 = new Task<decimal>(() => orderCalculator.Calculate(product));

//calculateTask1.Start();
//calculateTask2.Start();
//calculateTask3.Start();

//Task task4 = Task.WhenAll(calculateTask1, calculateTask2, calculateTask3);

//var result = calculateTask1.Result + calculateTask2.Result + calculateTask3.Result;

//task4
//    .ContinueWith(t => Task.Run(() => printer.Print($"Total: {result}")));


Task<decimal> calculateTask1 =  orderCalculator.CalculateAsync(product);
Task<decimal> calculateTask2 =  orderCalculator.CalculateAsync(product);
Task<decimal> calculateTask3 =  orderCalculator.CalculateAsync(product);

Task task4 = Task.WhenAll(calculateTask1, calculateTask2, calculateTask3);

// Task.Run(()=>printer.Print($"Cena: {calculateTask1.Result + calculateTask2.Result + calculateTask3.Result}"));

printer.Print($"Cena: {calculateTask1.Result + calculateTask2.Result + calculateTask3.Result}");

Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Press any key to exit.");
Console.ReadKey();
