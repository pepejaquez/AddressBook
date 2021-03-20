using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace AddressBook
{
	class Program
	{
		

		public static void Main(string[] args)
		{
			BookElements be = new BookElements();
			
			while(true)
			{
				Console.Clear();
				
				// Get all of the current address books.
				ShowAddressBooks(be.addressBooks);
				
				int userSelection;				
				
				//Check for valid user input.
				if(int.TryParse(Console.ReadLine(), out userSelection) == false ||
				      userSelection < 1 || userSelection > be.addressBooks.Length + 3)
				{
					InvalidEntryMessage();
				}
				
				// Create a new address book.
				if(userSelection == be.addressBooks.Length + 1)
				{
					Console.Clear();
					Console.Write("Address Book Name: ");
					string bookName = Console.ReadLine();
					if(bookName != "")
					{
						be.CreateNewAddressBook(bookName);	
					}
					
				}
				
				// Delete an address book.
				else if(userSelection == be.addressBooks.Length + 2 && be.addressBooks.Length > 0)
				{
					Console.Clear();
					byte counter = 1;
					foreach(string file in be.addressBooks)
					{
						//Display the current address books if they exist.
						string stripFileName = file.Replace("AddressBooks\\", "").Replace(".dat", "");
						Console.WriteLine("{0}) " + stripFileName, counter++);
					}
					
					Console.Write("\nEnter the number of your selection: ");
					
					int deleteSelection;
					//Check for valid user input.
					if(int.TryParse(Console.ReadLine(), out deleteSelection) == false ||
					      deleteSelection < 1 || deleteSelection > be.addressBooks.Length)
					{
						InvalidEntryMessage();
					}
					else
					{
						be.DeleteAddressBook(deleteSelection);	
					}					
				}
				
				// Exit the app.
				else if(userSelection == be.addressBooks.Length + 3)
				{					
					break;
				}
				
				else if(userSelection >= 1 && userSelection <= be.addressBooks.Length + 1)
				{
					// Load the chosen address book.
					be.LoadFromBinaryFile(be.addressBooks[userSelection - 1]);
					
					bool exitToMainScreen = false;
					
					while(!exitToMainScreen)
					{
						Console.Clear();
						Console.WriteLine("Current Address Book: " + be.addressBooks[userSelection - 1] + "\n********************\n");
						Console.WriteLine("1) View all names\n" +
						                  "2) Make new entries\n" +
						                  "3) Edit address information\n" +
						                  "4) Remove entry\n" +
						                  "5) Exit to main screen\n\n");
						Console.Write("Enter the number of your selection: ");
						
						
						int selectionInBook;
						//Check for valid user input.
						if(int.TryParse(Console.ReadLine(), out selectionInBook) == false ||
						      selectionInBook < 1 || selectionInBook > 5)
						{
							InvalidEntryMessage();
						}
						
						// Used to check TryParse in the following switch. 
						int parseOut;
						
						switch(selectionInBook)
						{
							// View names.
							case 1:
								if(be.addressBookEntries.Count == 0)
								{
									NoAddressMessage();
								}
								else
								{
									Console.Clear();
									
									// Sort the last names ascending.
									SortLastNames(be);
									
									be.ViewNamesOnly();
									
									Console.Write("\nTo view complete address information enter the line number\n" +
									                  "or press enter to return to the main menu: ");	
									
									string viewCompleteAddress = Console.ReadLine();
									
									//int parseOut;
									
									if(viewCompleteAddress != "")
									{
										
										//Check for valid user input.
										if(int.TryParse(viewCompleteAddress, out parseOut) == false ||
										      parseOut < 1 || parseOut > be.addressBookEntries.Count + 1)
										{
											InvalidEntryMessage();
										}
										
										Console.Clear();
										be.ViewAllAddressInformation(parseOut - 1);
										Console.Write("\nPress any key to continue...");
										Console.ReadKey(true);
									}
									break;
								}
								break;
								
							// Make new entries.
							case 2:
								be.EnterNewAddress();
								be.SaveAsBinaryFormat(be.addressBookEntries, be.addressBooks[userSelection - 1]);
								break;
								
							// Edit address information.
							case 3:
								if(be.addressBookEntries.Count == 0)
								{
									NoAddressMessage();									
									break;
								}
								
								// Sort the last names ascending.
								SortLastNames(be);
								
								be.ViewNamesOnly();
								Console.Write("\nEntry to edit: ");
								
								//Check for valid user input.
								if(int.TryParse(Console.ReadLine(), out selectionInBook) == false ||
								      selectionInBook < 1 || selectionInBook > be.addressBookEntries.Count + 1)
								{
									InvalidEntryMessage();
								}
								
								if(!(selectionInBook < 1 || selectionInBook > be.addressBookEntries.Count))
								{
									be.EditAddress(selectionInBook - 1);
									be.SaveAsBinaryFormat(be.addressBookEntries, be.addressBooks[userSelection - 1]);	
								}							
															
								break;
								
							// Remove address entry.
							case 4:
								
								if(be.addressBookEntries.Count == 0)
								{
									// warning for empty book.
									NoAddressMessage();
									break;
								}
							
								// Sort the last names ascending.
								SortLastNames(be);
									
								be.ViewNamesOnly();
								
								Console.Write("\nEntry to remove: ");								
								
								
								//Check for valid user input.
								if(int.TryParse(Console.ReadLine(), out parseOut) == false ||
								      parseOut < 1 || parseOut > be.addressBookEntries.Count + 1)
								{
									InvalidEntryMessage();
								}
								else
								{
									be.RemoveEntry(parseOut);									
								}
								break;
								
							// Exit to the main screen.
							case 5:
								be.SaveAsBinaryFormat(be.addressBookEntries, be.addressBooks[userSelection - 1]);
								exitToMainScreen = true;
								break;
						}						
					}					
				}				
			}
		}

		static void ShowAddressBooks(string[] addressBooks)
		{
			Console.WriteLine("You have " + addressBooks.Length + " address book(s).\n");
			
			byte counter = 1;
			foreach(string file in addressBooks)
			{
				//Display the current address books if they exist.
				string stripFileName = file.Replace("AddressBooks\\", "").Replace(".dat", "");
				Console.WriteLine("{0}) " + stripFileName, counter++);
			}
			
			Console.WriteLine("{0}) Create new address book", addressBooks.Length + 1);
			Console.WriteLine("{0}) Delete address book", addressBooks.Length + 2);
			
			Console.WriteLine("{0}) Exit", addressBooks.Length + 3);
			
			//Give the option to create a new address book.
			Console.Write("\nEnter the number of your selection: ");
		}
		
		static void SortLastNames(BookElements bookElements)
		{
			bookElements.addressBookEntries = bookElements.addressBookEntries.OrderBy(x => x.LastName).ToList();
			
		}
		
		static void NoAddressMessage()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.Write("\nThere are currently no addresses in this book.");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("\nPress any key to continue...");
			Console.ReadKey(true);
		}
		
		static void InvalidEntryMessage()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.Write("\nInvalid Entry");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("\nPress any key to continue...");
			Console.ReadKey(true);
		}
				
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}