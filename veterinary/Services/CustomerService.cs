using System;
using System.Collections.Generic;
using System.Linq;
using Veterinary.Models;

namespace Veterinary.Services
{
    public class CustomerService
    {
        // ---------- Customer ops (by ID) ----------

        public void RegisterCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n=== Register Customer ===");

            int id = ReadInt("Enter Customer ID: ");
            if (customers.Any(c => c.Id == id))
            {
                Console.WriteLine("⚠ A customer with that ID already exists.");
                return;
            }

            string name = ReadNonEmpty("Enter Name: ");
            string address = ReadNonEmpty("Enter Address: ");
            string email = ReadNonEmpty("Enter Email: ");
            string phone = ReadNonEmpty("Enter Phone: ");

            var customer = new Customer
            {
                Id = id,
                Name = name,
                Address = address,
                Email = email,
                Phone = phone
            };

            customers.Add(customer);
            Console.WriteLine("\n Registered Customer:");
            Console.WriteLine(customer.ToString());
        }

        public void ListCustomers(List<Customer> customers)
        {
            Console.WriteLine("\n=== Customer List ===");

            if (customers.Count == 0)
            {
                Console.WriteLine("⚠ No customers registered yet.");
                return;
            }

            foreach (var c in customers)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public Customer? FindCustomerById(List<Customer> customers, int id)
        {
            return customers.FirstOrDefault(c => c.Id == id);
        }

        public void SearchCustomerById(List<Customer> customers)
        {
            Console.WriteLine("\n=== Search Customer by ID ===");
            int id = ReadInt("Enter Customer ID: ");

            var found = FindCustomerById(customers, id);
            if (found != null)
            {
                Console.WriteLine("\n Found Customer:");
                Console.WriteLine(found.ToString());
            }
            else
            {
                Console.WriteLine("⚠ Customer not found.");
            }
        }

        // ---------- Pet ops (by Customer ID, and Pet ID unique per customer) ----------

        public void RegisterPetForCustomerByCustomerId(List<Customer> customers)
        {
            Console.WriteLine("\n=== Register Pet for Customer (by Customer ID) ===");

            if (customers.Count == 0)
            {
                Console.WriteLine("⚠ There are no customers yet. Please register a customer first.");
                return;
            }

            int customerId = ReadInt("Enter Customer ID: ");
            var customer = FindCustomerById(customers, customerId);
            if (customer == null)
            {
                Console.WriteLine("⚠ Customer not found.");
                return;
            }

            int petId = ReadInt("Enter Pet ID: ");
            if (customer.Pets.Any(p => p.Id == petId))
            {
                Console.WriteLine("⚠ This customer already has a pet with that ID.");
                return;
            }

            string petName = ReadNonEmpty("Enter Pet Name: ");
            string species = ReadNonEmpty("Enter Species: ");
            int age = ReadInt("Enter Pet Age: ");
            string? symptom = ReadOptional("Enter Symptom (optional): ");

            var pet = new Pet
            {
                Id = petId,
                Name = petName,
                Species = species,
                Age = age,
                Symptom = string.IsNullOrWhiteSpace(symptom) ? null : symptom.Trim()
            };

            customer.Pets.Add(pet);

            Console.WriteLine("\n Registered Pet:");
            Console.WriteLine(pet.ToString());
        }

        public void ListPetsForCustomerByCustomerId(List<Customer> customers)
        {
            Console.WriteLine("\n=== List Pets for Customer (by Customer ID) ===");

            if (customers.Count == 0)
            {
                Console.WriteLine("⚠ There are no customers yet.");
                return;
            }

            int customerId = ReadInt("Enter Customer ID: ");
            var customer = FindCustomerById(customers, customerId);
            if (customer == null)
            {
                Console.WriteLine("⚠ Customer not found.");
                return;
            }

            if (customer.Pets.Count == 0)
            {
                Console.WriteLine($"⚠ Customer '{customer.Name}' has no pets registered.");
                return;
            }

            Console.WriteLine($"\n Pets of {customer.Name} (CustomerID: {customer.Id}):");
            foreach (var p in customer.Pets)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public void SearchPetByIdForCustomerId(List<Customer> customers)
        {
            Console.WriteLine("\n=== Search Pet by PetID and CustomerID ===");

            int customerId = ReadInt("Enter Customer ID: ");
            var customer = FindCustomerById(customers, customerId);
            if (customer == null)
            {
                Console.WriteLine("⚠ Customer not found.");
                return;
            }

            int petId = ReadInt("Enter Pet ID: ");
            var pet = customer.Pets.FirstOrDefault(p => p.Id == petId);

            if (pet != null)
            {
                Console.WriteLine("\n Found Pet:");
                Console.WriteLine(pet.ToString());
            }
            else
            {
                Console.WriteLine("⚠ Pet not found for this customer.");
            }
        }

        // ---------- Helpers ----------
        private int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                    return value;

                Console.WriteLine("⚠ Invalid input. Please enter a whole number.");
            }
        }

        private string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("⚠ Value cannot be empty. Please try again.");
            }
        }

        private string? ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void DeleteCustomerById(List<Customer> customers)
        {
            Console.WriteLine("\n=== Delete Customer by ID ===");
            int id = ReadInt("Enter Customer ID to delete: ");

            var customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                Console.WriteLine($" Deleted: {customer.Name} (Id = {id})");
            }
            else
            {
                Console.WriteLine($" Customer with Id = {id} not found.");
            }
        }



        public void DeletePetById(List<Customer> customers)
        {
            Console.WriteLine("\n=== Delete Pet by CustomerID & PetID ===");

            int customerId = ReadInt("Enter Customer ID: ");
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with Id = {customerId} not found.");
                return;
            }

            int petId = ReadInt("Enter Pet ID to delete: ");
            var pet = customer.Pets.FirstOrDefault(p => p.Id == petId);
            if (pet == null)
            {
                Console.WriteLine($" Pet with Id = {petId} not found for this customer.");
                return;
            }

            customer.Pets.Remove(pet);
            Console.WriteLine($" Deleted Pet: {pet.Name} (PetId = {pet.Id}) for Customer {customer.Name} (CustomerId = {customer.Id})");
        }

        public void UpdateCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n=== Update Customer by ID ===");
            int id = ReadInt("Enter Customer ID to update: ");

            var customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                Console.WriteLine($" Customer with Id = {id} not found.");
                return;
            }

            Console.WriteLine($"Editing Customer: {customer.Name} (Id: {customer.Id})");


            string? name = ReadOptional($"Enter new Name (leave empty to keep '{customer.Name}'): ");
            string? address = ReadOptional($"Enter new Address (leave empty to keep '{customer.Address}'): ");
            string? email = ReadOptional($"Enter new Email (leave empty to keep '{customer.Email}'): ");
            string? phone = ReadOptional($"Enter new Phone (leave empty to keep '{customer.Phone}'): ");

            if (!string.IsNullOrWhiteSpace(name)) customer.Name = name;
            if (!string.IsNullOrWhiteSpace(address)) customer.Address = address;
            if (!string.IsNullOrWhiteSpace(email)) customer.Email = email;
            if (!string.IsNullOrWhiteSpace(phone)) customer.Phone = phone;

            Console.WriteLine($" Updated: {customer.Name} (Id = {customer.Id})");
        }
        public void UpdatePet(List<Customer> customers)
        {
            Console.WriteLine("\n=== Update Pet by CustomerID & PetID ===");

            int customerId = ReadInt("Enter Customer ID: ");
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine($" Customer with Id = {customerId} not found.");
                return;
            }
            if (customer.Pets.Count == 0)
            {
                Console.WriteLine($"⚠ Customer '{customer.Name}' has no pets registered.");
                return;
            }
            int petId = ReadInt("Enter Pet ID to update: ");
            var pet = customer.Pets.FirstOrDefault(p => p.Id == petId);
            if (pet == null)
            {
                Console.WriteLine($" Pet with Id = {petId} not found for this customer.");
                return;
            }

            Console.WriteLine($"Editing Pet: {pet.Name} (Id: {pet.Id}) for Customer {customer.Name} (CustomerId: {customer.Id})");

            string? petName = ReadOptional($"Enter new Pet Name (leave empty to keep '{pet.Name}'): ");
            string? species = ReadOptional($"Enter new Species (leave empty to keep '{pet.Species}'): ");
            string? ageInput = ReadOptional($"Enter new Pet Age (leave empty to keep '{pet.Age}'): ");
            string? symptom = ReadOptional($"Enter new Symptom (leave empty to keep '{pet.Symptom ?? "none"}'): ");
            if (!string.IsNullOrWhiteSpace(petName)) pet.Name = petName;
            if (!string.IsNullOrWhiteSpace(species)) pet.Species = species;
            if (int.TryParse(ageInput, out int age)) pet.Age = age;
            if (!string.IsNullOrWhiteSpace(symptom)) pet.Symptom = symptom;
            Console.WriteLine($"  Updated Pet: {pet.Name} (Id = {pet.Id}) for Customer {customer.Name} (CustomerId = {customer.Id})");

        }

        public void filterCustomerByAge(List<Customer> customers)
        {
            Console.WriteLine("\n=== Filter Customers by Age ===");
            int age = ReadInt("Enter minimum age to filter customers: ");
            var filteredCustomers = customers.Where(c => c.Pets.Any(p => p.Age >= age)).ToList();
            if (filteredCustomers.Count == 0)
            {
                Console.WriteLine($"⚠ No customers found with pets aged {age} or older.");
                return;
            }

            Console.WriteLine($"\n Customers with pets aged {age} or older:");
            foreach (var c in filteredCustomers)
            {
                Console.WriteLine(c.ToString());
            }

        }

        public void CustomerData(List<Customer> customers)
        {
            Console.Write("Enter the pet ID: ");
            int idWanted = int.Parse(Console.ReadLine());

            var data = customers
                .SelectMany(c => c.Pets, (c, p) => new { Customer = c, Pet = p })
                .Where(cp => cp.Pet.Id == idWanted)
                .Select(cp => new { cp.Pet.Name, cp.Customer.Email })
                .ToList();

            foreach (var d in data)
            {
                Console.WriteLine($"{d.Name} - {d.Email}");
            }
        }
        public void ListPetsOrdered(List<Customer> customers)
        {
            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter sorting criterion (name/age/species): ");
            string criterio = Console.ReadLine()?.ToLower() ?? "name";

            Console.Write("Descending order? (y/n): ");
            string descInput = Console.ReadLine()?.ToLower() ?? "n";
            bool descendente = descInput == "y";


            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("⚠ Customer not found.");
                return;
            }

            if (customer.Pets.Count == 0)
            {
                Console.WriteLine($"⚠ Customer '{customer.Name}' has no pets registered.");
                return;
            }

            IEnumerable<Pet> orderedPets = customer.Pets;

            switch (criterio)
            {
                case "name":
                    orderedPets = descendente ? customer.Pets.OrderByDescending(p => p.Name)
                                              : customer.Pets.OrderBy(p => p.Name);
                    break;
                case "age":
                    orderedPets = descendente ? customer.Pets.OrderByDescending(p => p.Age)
                                              : customer.Pets.OrderBy(p => p.Age);
                    break;
                case "species":
                    orderedPets = descendente ? customer.Pets.OrderByDescending(p => p.Species)
                                              : customer.Pets.OrderBy(p => p.Species);
                    break;
                default:
                    Console.WriteLine("⚠ Invalid sorting criterion. Use 'name', 'age', or 'species'.");
                    return;
            }

            Console.WriteLine($"\n🐾 Pets of {customer.Name} ordered by {criterio}:");
            foreach (var pet in orderedPets)
            {
                Console.WriteLine(pet);
            }
        }
    }
}
