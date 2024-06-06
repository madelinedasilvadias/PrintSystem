using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new DigitecContext();
            var created = context.Database.EnsureCreated();
            if (created)
            {
                // Add data into Database
                seed();
            }

            // Test basic queries
            testBasicQueries();

            Console.WriteLine("Done!");
        }

        private static void seed()
        {
            using var context = new DigitecContext();

            var emp1 = new Employee() { Firstname = "Tom", Lastname = "Simon", Age = 40, YearsOfService = 10, StartDate = new DateTime(2013, 1, 1), Salary = 4500 };
            var emp2 = new Employee() { Firstname = "Anny", Lastname = "Bing", Age = 20, YearsOfService = 3, StartDate = new DateTime(2020, 1, 1), Salary = 2500 };
            var emp3 = new Employee() { Firstname = "Tim", Lastname = "Burton", Age = 70, YearsOfService = 20, StartDate = new DateTime(2003, 1, 1), Salary = 10500 };
            context.Employees.AddRange(emp1, emp2, emp3);

            var cust1 = new Customer() { Firstname = "Alain", Lastname = "Duc", ShoppingAddress = "Route Principale 10, 3960 Sierre" };
            var cust2 = new Customer() { Firstname = "Antoine", Lastname = "Widmer", ShoppingAddress = "Ruelle du Chateau 2, 1920 Martigny" };
            var cust3 = new Customer() { Firstname = "John", Lastname = "Doe", ShoppingAddress = "Bahnhofstrasse 30, 3900 Brig" };
            context.Customers.AddRange(cust1, cust2, cust3);
            
            var item1 = new Item() { Name = "HIFI", Description = "blabla", Price = 89.9f, Employee = emp1 };
            var item2 = new Item() { Name = "HIFI1", Description = "blabla", Price = 189.9f, Employee = emp1 };
            var item3 = new Item() { Name = "HIFI2", Description = "blabla", Price = 79.9f, Employee = emp1 };
            var item4 = new Item() { Name = "HIFI3", Description = "blabla", Price = 19.9f, Employee = emp3 };
            context.Items.AddRange(item1, item2, item3, item4);

            var cart11 = new Cart() { ShoppingDate = new DateTime(2023, 1, 15), Customer = cust1 };
            var cart12 = new Cart() { ShoppingDate = new DateTime(2023, 2, 5), Customer = cust1 };
            var cart21 = new Cart() { ShoppingDate = new DateTime(2022, 1, 15), Customer = cust2 };
            var cart22 = new Cart() { ShoppingDate = new DateTime(2023, 2, 25), Customer = cust2 };
            var cart23 = new Cart() { ShoppingDate = new DateTime(2023, 3, 1), Customer = cust2 };
            var cart31 = new Cart() { ShoppingDate = new DateTime(2023, 1, 1), Customer = cust3 };
            context.Carts.AddRange(cart11, cart12, cart21, cart22, cart23, cart31);

            var cartItem1 = new CartItem() { Cart = cart11, Item = item1, Quantity = 1 };
            var cartItem2 = new CartItem() { Cart = cart11, Item = item2, Quantity = 1 };
            var cartItem3 = new CartItem() { Cart = cart11, Item = item3, Quantity = 1 };
            var cartItem4 = new CartItem() { Cart = cart11, Item = item4, Quantity = 1 };
            var cartItem5 = new CartItem() { Cart = cart12, Item = item2, Quantity = 2 };
            var cartItem6 = new CartItem() { Cart = cart21, Item = item1, Quantity = 2 };
            var cartItem7 = new CartItem() { Cart = cart21, Item = item4, Quantity = 3 };
            var cartItem8 = new CartItem() { Cart = cart22, Item = item1, Quantity = 1 };
            var cartItem9 = new CartItem() { Cart = cart23, Item = item3, Quantity = 4 };
            var cartItem10 = new CartItem() { Cart = cart31, Item = item3, Quantity = 4 };
            context.CartItems.AddRange(cartItem1, cartItem2, cartItem3, cartItem4, cartItem5, cartItem6, cartItem7, cartItem8, cartItem9, cartItem10);
                        
            context.SaveChanges();
        }

        private static void testBasicQueries()
        {
            using var context = new DigitecContext();

            // Get Tim Burton Employee
            var employee = context.Employees.FirstOrDefault(e => e.Firstname == "Tim" && e.Lastname == "Burton");
            Console.WriteLine($"Employee found : EmployeeId = {employee.EmployeeId}, Firstname = {employee.Firstname}, Lastname = {employee.Lastname}");

            // Update Tim Burton Employee
            employee.Firstname = "Tom";
            context.SaveChanges();            
            // Get Employee by his EmployeeId (new query)
            employee = context.Employees.Find(employee.EmployeeId);
            Console.WriteLine($"Employee found : EmployeeId = {employee.EmployeeId}, Firstname = {employee.Firstname}, Lastname = {employee.Lastname}");
            
            // Delete Employee
            context.Employees.Remove(employee);
            context.SaveChanges();
            // Try to get Employee by his EmployeeId
            employee = context.Employees.Find(employee.EmployeeId);
            if (employee == null)
            {
                Console.WriteLine($"Employee not found");
            }
        }
    }
}