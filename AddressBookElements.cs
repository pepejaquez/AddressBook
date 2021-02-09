using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AddressBook
{
	/// <summary>
	/// All the necessary elements to build a customized address book.
	/// </summary>
	public class AddressBookElements
	{	
		// Properties for address book.		
		public string BusinessName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string PhoneNumber { get; set; }
		public string CellPhone { get; set; }
		public string Comment { get; set; }
		
		// Path where user stores address books.
		public string addressBookPath = "Address Books";
		
		// List for available address books.
		public List<string> availableBooks = new List<string>();
		
		// List for entries in current address books.
		public List<string> currentBookEntries = new List<string>();		
		
		/// <summary>
		/// Creates a directory called 'Address Books' if one does not exist.
		/// If it does exist then the names of all the available address books
		/// are added to the availableBooks List for use.
		/// </summary>
		public void GetAvailableBooks()
		{
			if (Directory.Exists(addressBookPath) == false)
			{
				Directory.CreateDirectory(addressBookPath);
			}
			
			string[] files = Directory.GetFiles(addressBookPath);
			
			foreach (string file in files)
			{
				string strip = file.Replace(addressBookPath + "\\", "").Replace(".txt", "");
				// Check if the book is already in the list. If so do not add it again.
				if (!(availableBooks.Contains(strip)))
				{
					availableBooks.Add(strip);	
				}				
			}
		}		
		
		/// <summary>
		/// Get all entries for the address book the user selected
		/// and add them to the currentBookEntries List for use.
		/// </summary>
		/// <param name="bookName">The string name of the address book the user chose.</param>
		public void GetEntriesFromFile(string bookName)
		{
			if (!string.IsNullOrEmpty(bookName) && currentBookEntries.Count == 0)
			{
				string completeBookPath = string.Concat(addressBookPath, "\\", bookName, ".txt");
			
				string[] entries = File.ReadAllLines(completeBookPath);
			
				foreach (string entry in entries)
				{
					// Check if the entry is already in the list. If so do not add it again.
					if (!(currentBookEntries.Contains(entry)))
					{
						currentBookEntries.Add(entry);	
					} 
				}	
			}			
		}
		
		/// <summary>
		/// Save the currentBookEntries to the file.
		/// </summary>
		/// <param name="bookName">Name of the book to save to.</param>
		public void SaveToFile(string bookName)
		{
			if (currentBookEntries.Count > 0)
			{
				string completeBookPath = string.Concat(addressBookPath, "\\", bookName, ".txt");
				
				using (StreamWriter sw = new StreamWriter(completeBookPath, false))
				{
					foreach (string entry in currentBookEntries)
					{
						sw.WriteLine(entry);
					}
				}
			}
		}				
		
		/// <summary>
		/// Create address book.
		/// </summary>
		/// <param name="bookName">Name of the new address book.</param>
		public void CreateNewAddressBook(string bookName)
		{
			if (!string.IsNullOrEmpty(bookName))
			{
				using(File.Create(string.Concat(addressBookPath, "\\", bookName, ".txt")));
			}			
		}
		
		/// <summary>
		/// Delete address book.
		/// </summary>
		/// <param name="addressBookName">Name of the address book to be deleted.</param>
		public void DeleteAddressBook(string addressBookName)
		{
			string buildAddress = String.Concat(addressBookPath, "\\", addressBookName, ".txt");
			if (!string.IsNullOrEmpty(addressBookName) && File.Exists(buildAddress))
			{
				File.Delete(buildAddress);
			}			
		}
		
		/// <summary>
		/// Add an entry to the currentBookEntries List.
		/// </summary>
		public void AddEntry()
		{
			// string made from joining all the properties for an address book entry.
			string bookEntry = string.Concat(BusinessName, "|", FirstName, "|", LastName, "|", Address, "|", City, "|", State, "|", ZipCode, "|", PhoneNumber, "|", CellPhone, "|", Comment);
			
			currentBookEntries.Add(bookEntry);
		}
	}	
}
