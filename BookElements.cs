using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace AddressBook
{
	/// <summary>
	/// All the basic tools to build an address book.
	/// </summary>
	public class BookElements
	{
		
		string Path = @"AddressBooks\";		
		
		//Holds the address entries for the currently selected address book.
		public List<AddressEntry> addressBookEntries = new List<AddressEntry>();
			
		
		//Holds the different address books.
		public string[] addressBooks;
		
		
		public BookElements()
		{			
			//Place all current address books in the addressBooks array.
			getAddressBooks();
		}		
			
		
		private void getAddressBooks()
		{
			if(!Directory.Exists("AddressBooks"))
			{
				Directory.CreateDirectory("AddressBooks");
			}
			
			//Get all of the current address books.
			addressBooks = Directory.GetFiles("AddressBooks");
			StripFileName();
		}
		
		
		public void CreateNewAddressBook(string addressBookName)
		{			
			using(StreamWriter newFile = new StreamWriter(this.Path + addressBookName + ".dat")){}
			addressBooks = Directory.GetFiles("AddressBooks");
			StripFileName();
		}
		
		private void StripFileName()
		{
			// Strip the path and file extention off the file name.
			for(int i = 0; i < addressBooks.Length; i++)
			{
				addressBooks[i] = addressBooks[i].Replace("AddressBooks\\", "").Replace(".dat", "");
			}
		}
		
		
		//Add new entries to the address book.
		public void EnterNewAddress()
		{
			AddressEntry addressEntry = new AddressEntry();
			
			Console.Clear();
			
			Console.Write("First name: ");
			addressEntry.FirstName = ProperCase(Console.ReadLine());
			
			Console.Write("Last name: ");
			addressEntry.LastName = ProperCase(Console.ReadLine());
			
			Console.Write("Address: ");
			addressEntry.Address = Console.ReadLine();
			
			Console.Write("City: ");
			addressEntry.City = ProperCase(Console.ReadLine());
			
			Console.Write("State: ");
			addressEntry.State = Console.ReadLine().ToUpper();
			
			Console.Write("Zipcode: ");
			addressEntry.Zipcode = Console.ReadLine();
			
			Console.Write("Email: ");
			addressEntry.Email = Console.ReadLine();
			
			Console.Write("Home Phone: ");
			addressEntry.HomePhone = Console.ReadLine();
			
			Console.Write("Cell Phone: ");
			addressEntry.CellPhone = Console.ReadLine();
			
			Console.Write("Comment: ");
			addressEntry.Comment = Console.ReadLine();
			
			//Add the new entry to the book.
			addressBookEntries.Add(addressEntry);
		}
		
		
		public void EditAddress(int editEntry)
		{
			AddressEntry addressEntry = new AddressEntry();
			
			Console.Write("First Name: ".PadLeft(13) + addressBookEntries[editEntry].FirstName + " to: ");			
			addressEntry.FirstName = ProperCase(Console.ReadLine());
			
			Console.Write("Last Name: ".PadLeft(13) + addressBookEntries[editEntry].LastName + " to: ");
			addressEntry.LastName = ProperCase(Console.ReadLine());
			
			Console.Write("Address: ".PadLeft(13) + addressBookEntries[editEntry].Address + " to: ");
			addressEntry.Address = Console.ReadLine();
			
			Console.Write("City: ".PadLeft(13) + addressBookEntries[editEntry].City + " to: ");
			addressEntry.City = ProperCase(Console.ReadLine());
			
			Console.Write("State: ".PadLeft(13) + addressBookEntries[editEntry].State + " to: ");
			addressEntry.State = Console.ReadLine().ToUpper();
			
			Console.Write("Zip Code: ".PadLeft(13) + addressBookEntries[editEntry].Zipcode + " to: ");
			addressEntry.Zipcode = Console.ReadLine();
			
			Console.Write("Email: ".PadLeft(13) + addressBookEntries[editEntry].Email + " to: ");
			addressEntry.Email = Console.ReadLine();
			
			Console.Write("Home Phone: ".PadLeft(13) + addressBookEntries[editEntry].HomePhone + " to: ");
			addressEntry.HomePhone = Console.ReadLine();
			
			Console.Write("Cell Phone: ".PadLeft(13) + addressBookEntries[editEntry].CellPhone + " to: ");
			addressEntry.CellPhone = Console.ReadLine();
			
			Console.Write("Comment: ".PadLeft(13) + addressBookEntries[editEntry].Comment + " to: ");
			addressEntry.Comment = Console.ReadLine();
			
			//Add the new entry to the book.
			addressBookEntries.Add(addressEntry);
			
			//Remove the old entry.
			addressBookEntries.RemoveAt(editEntry);
		}
		
		public void RemoveEntry(int removeEntry)
		{
			addressBookEntries.RemoveAt(removeEntry - 1);
		}
		
		
		public void ViewNamesOnly()
		{
			Console.Clear();
			Console.WriteLine("CURRENT CONTACT LIST\n********************");
			int counter = 1;
			foreach(AddressEntry addressEntry in addressBookEntries)
			{
				Console.WriteLine("{0}) {1}, {2}", counter++, addressEntry.LastName ,addressEntry.FirstName);
			}
		}
		
		
		public void ViewAllAddressInformation(int lineNumber)
		{
			Console.WriteLine("COMPLETE ADDRESS\n****************");
			Console.WriteLine("Name: ".PadLeft(16) + addressBookEntries[lineNumber].FirstName + " " + addressBookEntries[lineNumber].LastName + "\n" +
			                  "Address: ".PadLeft(16) + addressBookEntries[lineNumber].Address + "\n" +
			                  "City\\State\\Zip: ".PadLeft(16) + addressBookEntries[lineNumber].City + ", " + addressBookEntries[lineNumber].State + ".  " + addressBookEntries[lineNumber].Zipcode + "\n" +
			                  "Email: ".PadLeft(16) + addressBookEntries[lineNumber].Email + "\n" +
			                  "Hone Phone: ".PadLeft(16) + addressBookEntries[lineNumber].HomePhone + "\n" +
			                  "Cell Phone: ".PadLeft(16) + addressBookEntries[lineNumber].CellPhone + "\n" + 
			                  "Comment: ".PadLeft(16) + addressBookEntries[lineNumber].Comment);
		}
		
		
		public void SaveAsBinaryFormat(object objGraph, string fileName)
		{			
			//Save object to a file in binary.
			BinaryFormatter binFormat = new BinaryFormatter();
			using(Stream fStream = new FileStream(Path + fileName + ".dat",
			FileMode.Create, FileAccess.Write, FileShare.None))
			{
				binFormat.Serialize(fStream, objGraph);
			}		
		}
		

		public void LoadFromBinaryFile(string fileName)
		{			
			BinaryFormatter binFormat = new BinaryFormatter();
			
			//Read the binary file.
			using(Stream fStream = File.OpenRead(Path + fileName + ".dat"))
			{
				if(fStream.Length != 0)
				{
					addressBookEntries = (List<AddressEntry>)binFormat.Deserialize(fStream);	
				}				
			}			
		}

		private string ProperCase(string str)
		{
			if(str != "")
			{
				StringBuilder sb = new StringBuilder(str);
			
				//2nd index character capitalized.
				if(str.Substring(0, 2) == "mc")
				{
					sb[2] = char.ToUpper(sb[2]);
				}
				//Indexed chracter + 1 capitalized.
				else if(str.Contains("-"))
				{
					sb[str.IndexOf('-') + 1] = char.ToUpper(sb[str.IndexOf('-') + 1]);
				}
				//Indexed character + 1 capitalized.
				else if(str.Contains(" "))
				{
					sb[str.IndexOf(' ') + 1] = char.ToUpper(sb[str.IndexOf(' ') + 1]);
				}
				//Every 1st character capitalized.
				sb[0] = char.ToUpper(sb[0]);
				
				return sb.ToString();
			}
			else
			{
				return str;
			}
		}
		
		public void DeleteAddressBook(int removeBook)
		{
			File.Delete(Path + addressBooks[removeBook - 1] + ".dat");
			getAddressBooks();
		}		
	}
}