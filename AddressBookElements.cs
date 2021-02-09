using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AddressBook
{
    class AddressBookElements
    {
        //holds address book entries
        public List<string> addressBookEntries = new List<string>();

        private string lastName;
        private string firstName;
        private string address;
        private string cityStateZip;
        private string email;
        private string comment;
        public string path;

        //*********************************************************************************************************************************
        //constructor
        public AddressBookElements()
        {
            Console.Title = "Address Book Entries";
            //Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
        }
        //*********************************************************************************************************************************
        //address methods
        public void SetAddress(string address)
        {
            this.address = address;
        }

        public string GetAddress()
        {
            return this.address;
        }

        //cityStateZip methods
        public void SetCityStateZip(string cityStateZip)
        {
            this.cityStateZip = cityStateZip;
        }

        public string GetCityStateZip()
        {
            return this.cityStateZip;
        }

        //comment methods
        public void SetComment(string comment)
        {
            this.comment = comment;
        }

        public string GetComment()
        {
            return this.comment;
        }

        //email methods
        public void SetEmail(string email)
        {
            this.email = email;
        }

        public string GetEmail()
        {
            return this.email;
        }

        //first name methods
        public void SetFirstName(string firstName)
        {
            while (firstName.Length == 0)
            {
                Console.Write("You did not enter a first name!\n\nEnter the first name: ");
                firstName = Console.ReadLine();
            }
            string fixName = CaseToProper(firstName);
            this.firstName = fixName;
        }

        public string GetFirstName()
        {

            return this.firstName;
        }

        //last name methods
        public void SetLastName(string lastName)
        {
            while (lastName.Length == 0)
            {
                Console.Write("You did not enter a last name!\n\nEnter the last name: ");
                lastName = Console.ReadLine();
            }
            string fixName = CaseToProper(lastName);
            this.lastName = fixName;
        }

        public string GetLastName()
        {

            return this.lastName;
        }
        //*********************************************************************************************************************************
        //general methods
        public string ConcatForFile()
        {
            string fileEntry = lastName + "|" + firstName + "|" + address + "|" + cityStateZip + "|" + email + "|" + comment;
            return fileEntry;
        }

        public void WriteToFile(string path, List<string> input)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (string s in input)
                {
                    sw.WriteLine(s);
                }
            }
        }

        public void ReadFromFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                //list = new string[fs.Length];
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();
                    addressBookEntries.Add(line);
                }
            }
        }

        public string CaseToProper(string s)
        {
            //changes input string to proper case
            StringBuilder sb = new StringBuilder(s);
            sb[0] = char.ToUpper(sb[0]);
            string word = sb.ToString();
            return word;
        }

        public void ViewNamesOnly(List<string> entries)
        {
            Console.WriteLine("CURRENT CONTACT LIST\n********************");
            int counter = 1;
            foreach (string s in entries)
            {
                string[] splitter = s.Split(new Char[] { '|' });
                Console.WriteLine("{0}) {1}, {2}", counter++, CaseToProper(splitter[0]), CaseToProper(splitter[1]));
            }
        }

        public void ViewAllAddressInformation(int lineNumber, List<string> addressEntries)
        {
            string[] fields = { "    First Name: ", "     Last Name: ", "       Address: ", "City\\State\\ZIP: ", "         email: ", "       Comment: " };
            string[] splitter = addressEntries[lineNumber].Split(new Char[] { '|' });
            byte counter = 0;
            Console.Clear();
            Console.WriteLine("COMPLETE ADDRESS\n****************");
            foreach (string s in splitter)
            {
                Console.WriteLine("{0}" + s, fields[counter++]);
            }
            Console.Write("\n(Press any key to return main menu)");
        }

        public void AddressAdder()
        {
            Console.Clear();
            Console.Write("Enter the last name: ");
            SetLastName(Console.ReadLine());

            Console.Write("Enter the first name: ");
            SetFirstName(Console.ReadLine());

            Console.Write("Enter the address: ");
            SetAddress(Console.ReadLine());

            Console.Write("Enter the (city state zip): ");
            SetCityStateZip(Console.ReadLine());

            Console.Write("Enter the email: ");
            SetEmail(Console.ReadLine());

            Console.Write("Enter the comment: ");
            SetComment(Console.ReadLine());

            addressBookEntries.Add(ConcatForFile());

            Console.Write("\n(Press any key to return main menu)");
            Console.Clear();
        }

        public void promptForAddressBooks()
        {
            if (Directory.Exists("AddressBooks") == false)
            {
                Directory.CreateDirectory("AddressBooks");
            }

            string[] files = Directory.GetFiles("AddressBooks");

            Console.WriteLine("You have " + files.Length + " address book(s).\n");

            byte counter = 1;
            foreach (string file in files)
            {
                string stripFileName = file.Replace("AddressBooks\\", "").Replace(".txt", "");
                Console.WriteLine("{0}) " + stripFileName, counter++);
            }

            Console.Write("{0}) New", files.Length + 1);

            Console.Write("\n\nPlease enter an address book number or enter {0} to create an address book: ", files.Length + 1);

            int selection;
            while (int.TryParse(Console.ReadLine(), out selection) == false || selection < 1 || selection > files.Length + 1)
            {
                Console.Write("You did not enter a valid entry.\nPlease enter an address book number or enter {0} to create an address book: ", files.Length + 1);

            }

            if (selection == files.Length + 1)
            {
                Console.Write("What is the name of the new address book?: ");
                string bookName = Console.ReadLine();
                while (bookName == "" || bookName.Length > 25)
                {
                    Console.Write("\nYou must enter a name for your address book that is 25 characters or less.\nWhat is the name of the new address book?: ");
                    bookName = Console.ReadLine();
                }

                this.path = "AddressBooks\\" + bookName + ".txt";
                StreamWriter newFile = new StreamWriter(this.path);
                newFile.Close();
            }

            else if (File.Exists(files[Convert.ToInt32(selection) - 1]))
            {
                ReadFromFile(files[Convert.ToInt32(selection) - 1]);
                this.path = files[Convert.ToInt32(selection) - 1];
            }
            else
            {
                Console.WriteLine("Sorry, book does not exist...");
            }

        }
    }
}
