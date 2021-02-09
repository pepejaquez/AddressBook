using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBookElements addressBook = new AddressBookElements();
            bool whileSwitcher = true;

            addressBook.promptForAddressBooks();

            while (whileSwitcher)
            {
                Console.Clear();
                Console.Write("\nCurrent Address Book: {0}\n\n1) View all names in address book\n2) Make new entries.\n3) Update address information\n4) Exit program\n\nEnter the number of what you want to do: ", addressBook.path.Replace("AddressBooks\\", "").Replace(".txt", ""));

                byte userNumber;
                while (byte.TryParse(Console.ReadLine(), out userNumber) == false || userNumber < 1 || userNumber > addressBook.addressBookEntries.Count)
                {
                    Console.Clear();
                    Console.Write("\nInvalid input!\n\n1) View all names in address book\n2) Make new entries.\n3) Update address information\n4) Exit program\n\nEnter the number of what you want to do: ");
                }

                switch (userNumber)
                {
                    case 1:
                        Console.Clear();
                        if (addressBook.addressBookEntries.Count == 0)
                        {
                            Console.Write("There are no entries to view.\nPress any key to return to main menu.");
                            Console.ReadKey();
                            break;
                        }
                        addressBook.ViewNamesOnly(addressBook.addressBookEntries);
                        Console.Write("\nTo view complete address information enter the line number\nor press enter to return to main menu: ");
                        string viewNameInput = Console.ReadLine();
                        if (viewNameInput == "")
                        {

                        }

                        else
                        {
                            int userEntryNumber;
                            while (int.TryParse(viewNameInput, out userEntryNumber) == false || userEntryNumber < 1 || userEntryNumber > addressBook.addressBookEntries.Count)
                            {
                                Console.Write("\nYou did not enter a valid entry.\nTo view complete address information enter the line number: ");
                                viewNameInput = Console.ReadLine();
                                //while (int.TryParse(Console.ReadLine(), out userEntryNumber) == false)
                                //{
                                //    break;
                                //}

                            }
                            addressBook.ViewAllAddressInformation(userEntryNumber - 1, addressBook.addressBookEntries);
                            Console.ReadKey();
                        }

                        break;

                    case 2:
                        addressBook.AddressAdder();

                        break;

                    case 3:
                        Console.Clear();
                        if (addressBook.addressBookEntries.Count == 0)
                        {
                            Console.Write("There are no entries to view.\nPress any key to return to main menu.");
                            Console.ReadKey();
                            break;
                        }
                        addressBook.ViewNamesOnly(addressBook.addressBookEntries);
                        Console.Write("\nEnter line number to change persons information\nor press enter to return to main menu: ");

                        string userInput = Console.ReadLine();
                        if (userInput == "")
                        {
                        }
                        else
                        {
                            int entryToChangeNumber = Int32.Parse(userInput);
                            while (entryToChangeNumber < 1 || entryToChangeNumber > addressBook.addressBookEntries.Count)
                            {
                                Console.Write("\nYou did not enter a valid entry.\nEnter line number to change persons information: ");

                                while (int.TryParse(Console.ReadLine(), out entryToChangeNumber) == false)
                                {
                                    break;
                                }
                            }
                            addressBook.AddressAdder();
                            addressBook.addressBookEntries.RemoveAt(entryToChangeNumber - 1);
                        }

                        break;

                    case 4:
                        whileSwitcher = false;
                        break;
                }

                addressBook.WriteToFile(addressBook.path, addressBook.addressBookEntries);

            }
        }
    }
}
