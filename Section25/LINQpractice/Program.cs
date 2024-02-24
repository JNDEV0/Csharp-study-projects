using System.Diagnostics.Contracts;
using System.Linq;

namespace LinqPractice
{
    class Program
    {
        static void Main()
        {
            List<Customer> customers = new() 
            { 
            new Customer(1, "Juan", "Norpolis", 1000.00m),
            new Customer(2, "Jorge", "Norpolis", 2090.87m),
            new Customer(3, "Jimmy", "Norpolis", 30000.00m),
            new Customer(4, "Jack", "Julietown", 0m),
            };

            IEnumerable<Customer> norpolisCustomers = customers.Where(cust => cust.City == "Norpolis").OrderBy(cust => cust.Name);

            foreach (Customer customer in norpolisCustomers)
            {
                Console.WriteLine($"{customer.Name}, { customer.City}");
            }

            //first or last
            Customer firstCustomer = customers.FirstOrDefault(cust => cust.Name == "Juanito");
            if (firstCustomer is not null)
            {
                Console.WriteLine($"{firstCustomer.Name}, {firstCustomer.City}");
            }

            Customer lastCustomer = customers.LastOrDefault(cust => cust.City == "Norpolis");
            Console.WriteLine($"{lastCustomer.Name}, {lastCustomer.City}");

            //elementAt
            int index = Convert.ToInt32(Math.Floor(Convert.ToDecimal(norpolisCustomers.Count()) / 2));
            Customer middleCustomer = norpolisCustomers.ElementAtOrDefault(index);
            Console.WriteLine($"{middleCustomer.Name}, {middleCustomer.City}");

            //select
            Console.WriteLine("List of Persons from Customers using Select:");
            List<Person> persons = customers.Select(cust => new Person(cust.Name)).ToList();
            foreach (Person person in persons)
            {
                Console.WriteLine(person.Name);
            }

            //min, max, sum, average, count
            decimal min = customers.Min(cust => cust.Balance);
            decimal max = customers.Max(cust => cust.Balance);
            decimal sum = customers.Sum(cust => cust.Balance);
            decimal average = customers.Average(cust => cust.Balance);
            int count = customers.Count((cust) => { if (cust.Balance > 0) { return true; } else { return false; } });
            Console.WriteLine($"Min balance:{min}, Max balance:{max}, Sum total:{sum}, Average balance:{average}, Count with a balance:{count}");
        }
    }

    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Balance { get; set; }
    
        public Customer(int id, string name, string city, decimal balance)
        {
            Id = id;
            Name = name;
            City = city;
            Balance = balance;
        }
    }

    class Person
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }
    }
}