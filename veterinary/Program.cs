

using Veterinary.Services;

namespace Veterinary.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Health Clinic the nursery for pets!");
        List<Customer> customers = new List<Customer>();
        CustomerService service = new CustomerService();

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== Veterinary Console System (ID-based) ===");
            Console.WriteLine("1. Register customer");
            Console.WriteLine("2. List customers");
            Console.WriteLine("3. Search customer by ID");
            Console.WriteLine("4. Register pet for customer (by Customer ID)");
            Console.WriteLine("5. List pets for customer (by Customer ID)");
            Console.WriteLine("6. Search pet by PetID & CustomerID");
            Console.WriteLine(" 7  delete client by ID");
            Console.WriteLine(" 8  delete all pet by ID");
            Console.WriteLine(" 9  update client by ID");
            Console.WriteLine(" 10 filter clients by age or pet age");
            Console.WriteLine("10. Exit");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    service.RegisterCustomer(customers);
                    break;

                case "2":
                    service.ListCustomers(customers);
                    break;

                case "3":
                    service.SearchCustomerById(customers);
                    break;

                case "4":
                    service.RegisterPetForCustomerByCustomerId(customers);
                    break;

                case "5":
                    service.ListPetsForCustomerByCustomerId(customers);
                    break;

                case "6":
                    service.SearchPetByIdForCustomerId(customers);
                    break;
                case "7":
                    service.DeleteCustomerById(customers);
                    break;

                case "8":
                    service.DeletePetById(customers);
                    break;

                case "9":
                    service.UpdateCustomer(customers);
                    break;

                case "10":
                    service.filterCustomerByAge(customers);
                    break;

                case "11":
                    Console.WriteLine("\nExiting system... Goodbye!");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("\n❌ Invalid option. Please try again.");
                    break;
            }
        }
    }
}

