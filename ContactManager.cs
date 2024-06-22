using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiThucHanhC_
{
    public class ContactManager
    {
        private Hashtable addressBook = new Hashtable();

        public void Run()
        {
            int choice;
            do
            {
                Console.WriteLine("Contact Manager Menu:");
                Console.WriteLine("1. Add new contact");
                Console.WriteLine("2. Find a contact by name");
                Console.WriteLine("3. Display all contacts");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddContact();
                            break;
                        case 2:
                            FindContactByName();
                            break;
                        case 3:
                            DisplayContacts();
                            break;
                        case 4:
                            Console.WriteLine("Exiting Contact Manager. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine();
            } while (choice != 4);
        }

        private void AddContact()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine().Trim();
            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine().Trim();

            if (addressBook.ContainsKey(name))
            {
                Console.WriteLine($"Contact with name '{name}' already exists.");
            }
            else
            {
                Contact contact = new Contact(name, phone);
                addressBook.Add(name, contact);
                Console.WriteLine("Contact added successfully.");
            }
        }

        private void FindContactByName()
        {
            Console.Write("Enter name to find: ");
            string name = Console.ReadLine().Trim();

            if (addressBook.ContainsKey(name))
            {
                Contact contact = (Contact)addressBook[name];
                Console.WriteLine($"Phone number for {name}: {contact.Phone}");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        private void DisplayContacts()
        {
            Console.WriteLine("Contacts in the Address Book:");
            foreach (var key in addressBook.Keys)
            {
                Contact contact = (Contact)addressBook[key];
                Console.WriteLine($"Name: {contact.Name}, Phone: {contact.Phone}");
            }
        }
    }
}