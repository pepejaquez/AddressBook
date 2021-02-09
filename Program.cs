using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AddressBook
{
	class Program
	{
		public static void Main(string[] args)
		{
			AddressBookElements abe = new AddressBookElements();
			string userChoice = string.Empty;
			bool runBooks = true;

			while (runBooks)
			{
				abe.GetAvailableBooks();
							
				Console.WriteLine("You have {0} address book(s) available.\n", abe.availableBooks.Count);
				
				// List each available address book.
				int counter = 0;
				foreach (string entry in abe.availableBooks)
				{
					counter++;
					Console.WriteLine("{0}) {1}", counter, entry);				
				}
				
				// Default choices always available to the user.
				Console.WriteLine("{0}) Create New Address Book.", ++counter);
				Console.WriteLine("{0}) Delete Address Book.", ++counter);
				Console.WriteLine("{0}) Exit Program.\n", ++counter);
				
				Console.Write("Please enter your choice: ");
				
				// Try to parse user input for the While(runBooks). If unsuccessful user is kicked back to start through the else statement.
				int WhileRunBooks;
				if (int.TryParse(Console.ReadLine(), out WhileRunBooks) == true)
				{
					// View an address book.
					if (WhileRunBooks > 0 && WhileRunBooks <= abe.availableBooks.Count)
					{
						bool viewEntries = true;
						while (viewEntries)
						{
							Console.Clear();
							// Populate the abe.currentBookEntries with requested address book.
							abe.GetEntriesFromFile(abe.availableBooks[WhileRunBooks - 1]);
							// Address book name as header
							Console.WriteLine(abe.availableBooks[WhileRunBooks - 1]);							
							// User choices.
							Console.WriteLine("\n1) View full entry.");
							Console.WriteLine("2) Edit entry.");
							Console.WriteLine("3) Add new entry.");
							Console.WriteLine("4) Delete entry.");
							Console.WriteLine("5) Exit to main menu.\n");
							Console.Write("Please enter your choice: ");
							
							// Try to parse user input for the While(viewEntries). If unsuccessful user is kicked back to start through the else statement.
							int WhileViewEntries;
							if (int.TryParse(Console.ReadLine(), out WhileViewEntries) == true)
							{
								switch (WhileViewEntries)
								{
									// View full entry.
									case 1:
										Console.Clear();
										// Address book name as header
										Console.WriteLine(abe.availableBooks[WhileRunBooks - 1] + "\n");
										
										// List entries by LastName, FirstName.
										int counter2 = 0;
										foreach (string entry in abe.currentBookEntries)
										{
											string[] splitEntry = entry.Split('|');
											Console.WriteLine("{0}) {1}, {2}", ++counter2, splitEntry[2], splitEntry[1]);
										}
										Console.Write("\nEnter the number to view: ");
										
										// View the full entry.
										int fullEntry;
										if ( int.TryParse(Console.ReadLine(), out fullEntry) == true && (fullEntry > 0 && (fullEntry <= abe.currentBookEntries.Count)))
										{
											Console.Clear();
											// Address book name as header
											Console.WriteLine(abe.availableBooks[WhileRunBooks - 1] + "\n"); 
											
											string[] entries = abe.currentBookEntries[fullEntry - 1].Split('|');
											Console.WriteLine("Business Name: {0}", entries[0]);
											Console.WriteLine("   First Name: {0}", entries[1]);
											Console.WriteLine("    Last Name: {0}", entries[2]);
											Console.WriteLine("      Address: {0}", entries[3]);
											Console.WriteLine("         City: {0}", entries[4]);
											Console.WriteLine("        State: {0}", entries[5]);
											Console.WriteLine("      ZipCode: {0}", entries[6]);
											Console.WriteLine(" Phone Number: {0}", entries[7]);
											Console.WriteLine("   Cell Phone: {0}", entries[8]);
											Console.WriteLine("      Comment: {0}", entries[9]);
											Console.Write("\nPress any key to continue . . .");
											Console.ReadKey(true);
										}										
										break;
									// Edit entry.
									case 2:										
										// View the full entry.
										Console.Clear();
										// Address book name as header
										Console.WriteLine(abe.availableBooks[WhileRunBooks - 1] + "\n");
										
										// List entries by LastName, FirstName.
										int counter3 = 0;
										foreach (string entry in abe.currentBookEntries)
										{
											string[] splitEntry = entry.Split('|');
											Console.WriteLine("{0}) {1}, {2}", ++counter3, splitEntry[2], splitEntry[1]);
										}
										Console.Write("\nEnter the number to edit: ");
										int editEntry;
										if ( int.TryParse(Console.ReadLine(), out editEntry) == true && (editEntry > 0 && (editEntry <= abe.currentBookEntries.Count)))
										{
											bool whileEditEntry = true;
											while (whileEditEntry)
											{
												Console.Clear();
												// Address book name as header
												Console.WriteLine(abe.availableBooks[WhileRunBooks - 1] + "\n"); 
														
												string[] entries = abe.currentBookEntries[editEntry - 1].Split('|');
												Console.WriteLine("1) Business Name: {0}", entries[0]);
												Console.WriteLine("2)    First Name: {0}", entries[1]);
												Console.WriteLine("3)     Last Name: {0}", entries[2]);
												Console.WriteLine("4)       Address: {0}", entries[3]);
												Console.WriteLine("5)          City: {0}", entries[4]);
												Console.WriteLine("6)         State: {0}", entries[5]);
												Console.WriteLine("7)       ZipCode: {0}", entries[6]);
												Console.WriteLine("8)  Phone Number: {0}", entries[7]);
												Console.WriteLine("9)    Cell Phone: {0}", entries[8]);
												Console.WriteLine("10)      Comment: {0}", entries[9]);
												Console.WriteLine("11) Exit");
												Console.Write("\nEnter the line number to edit: ");
												
												switch (Console.ReadLine())
												{
													case "1":
														Console.Clear();
														Console.WriteLine("1) Business Name: {0}", entries[0]);
														Console.Write("\nChange to: ");
														entries[0] = Console.ReadLine();
														break;
													case "2":
														Console.Clear();
														Console.WriteLine("2) First Name: {0}", entries[1]);
														Console.Write("\nChange to: ");
														entries[1] = Console.ReadLine();
														break;
													case "3":
														Console.Clear();
														Console.WriteLine("3) Last Name: {0}", entries[2]);
														Console.Write("\nChange to: ");
														entries[2] = Console.ReadLine();
														break;
													case "4":
														Console.Clear();
														Console.WriteLine("4) Address: {0}", entries[3]);
														Console.Write("\nChange to: ");
														entries[3] = Console.ReadLine();
														break;
													case "5":
														Console.Clear();
														Console.WriteLine("5) City: {0}", entries[4]);
														Console.Write("\nChange to: ");
														entries[4] = Console.ReadLine();
														break;
													case "6":
														Console.Clear();
														Console.WriteLine("6) State: {0}", entries[5]);
														Console.Write("\nChange to: ");
														entries[5] = Console.ReadLine();
														break;
													case "7":
														Console.Clear();
														Console.WriteLine("7) ZipCode: {0}", entries[6]);
														Console.Write("\nChange to: ");
														entries[6] = Console.ReadLine();
														break;
													case "8":
														Console.Clear();
														Console.WriteLine("8) Phone Number: {0}", entries[7]);
														Console.Write("\nChange to: ");
														entries[7] = Console.ReadLine();
														break;
													case "9":
														Console.Clear();
														Console.WriteLine("9) Cell Phone: {0}", entries[8]);
														Console.Write("\nChange to: ");
														entries[8] = Console.ReadLine();
														break;
													case "10":
														Console.Clear();
														Console.WriteLine("10) Comment: {0}", entries[9]);
														Console.Write("\nChange to: ");
														entries[9] = Console.ReadLine();
														break;
													case "11":
														whileEditEntry = false;
														break;
												}
												
												if (whileEditEntry == true)
												{
													// Reassemble the full entry with the updated information and update the list.
													string assembleEntry = "";
													for (int i = 0; i <=9; i++)
													{
														if (i != 9)
														{
															assembleEntry += entries[i] + "|";
														}
														else
														{
															assembleEntry += entries[i];
														}
													}
													abe.currentBookEntries[editEntry - 1] = assembleEntry;
												}												
											}											
										}
										break;
									// Add new entry.
									case 3:
										Console.Clear();
										// Address book name as header
										Console.WriteLine(abe.availableBooks[WhileRunBooks - 1] + "\n");
										// Enter address information.
										Console.Write("Business Name: ");
										abe.BusinessName = Console.ReadLine();
										Console.Write("   First Name: ");
										abe.FirstName = Console.ReadLine();
										Console.Write("    Last Name: ");
										abe.LastName = Console.ReadLine();
										Console.Write("      Address: ");
										abe.Address = Console.ReadLine();
										Console.Write("         City: ");
										abe.City = Console.ReadLine();
										Console.Write("        State: ");
										abe.State = Console.ReadLine();
										Console.Write("      ZipCode: ");
										abe.ZipCode = Console.ReadLine();
										Console.Write(" Phone Number: ");
										abe.PhoneNumber = Console.ReadLine();
										Console.Write("   Cell Phone: ");
										abe.CellPhone = Console.ReadLine();
										Console.Write("      Comment: ");
										abe.Comment = Console.ReadLine();
										
										// Add the new entry to the list.
										abe.AddEntry();											
										break;
									// Delete entry
									case 4:
										Console.Clear();
										// Address book name as header
										Console.WriteLine(abe.availableBooks[WhileRunBooks - 1] + "\n");
										
										// List entries by LastName, FirstName.
										int counter4 = 0;
										foreach (string entry in abe.currentBookEntries)
										{
											string[] splitEntry = entry.Split('|');
											Console.WriteLine("{0}) {1}, {2}", ++counter4, splitEntry[2], splitEntry[1]);
										}
										Console.Write("\nEnter the number to delete: ");
										int deleteEntry;
										if (int.TryParse(Console.ReadLine(), out deleteEntry) == true && deleteEntry > 0 && deleteEntry <= abe.currentBookEntries.Count)
										{
											abe.currentBookEntries.RemoveAt(deleteEntry - 1);
										}
										break;
									// Exit to main menu.
									case 5:
										viewEntries = false;										
										
										// Save any updates to the address book.
										abe.SaveToFile(abe.availableBooks[WhileRunBooks - 1]);
									
										Console.Clear();
										break;
								}
							}
						}
					}
					// Create an address book.
					else if (WhileRunBooks == abe.availableBooks.Count + 1)
					{
						Console.Clear();
						Console.Write("Enter the name of the new address book: ");
						string newName = Console.ReadLine();
						if (!string.IsNullOrEmpty(newName))
						{
							abe.CreateNewAddressBook(newName);
						}
						Console.Clear();
					}
					// Delete an address book.
					else if (WhileRunBooks == abe.availableBooks.Count + 2)
					{
						Console.Clear();
						Console.WriteLine("You have {0} address book(s) available.\n", abe.availableBooks.Count);
				
						// List each available address book.
						int counter5 = 0;
						foreach (string entry in abe.availableBooks)
						{
							counter++;
							Console.WriteLine("{0}) {1}", ++counter5, entry);				
						}
						
						Console.Write("\nEnter the book number to delete: ");
						int deleteBook;
						if (int.TryParse(Console.ReadLine(), out deleteBook) == true && (deleteBook > 0 && deleteBook <= abe.availableBooks.Count))
						{
							abe.DeleteAddressBook(abe.availableBooks[deleteBook -1]);
							abe.availableBooks.RemoveAt(deleteBook - 1);
						}
						Console.Clear();
					}
					// Exit the program.
					else if (WhileRunBooks == abe.availableBooks.Count + 3)
					{
						runBooks = false;
					}
					// Input mistake, return to main menu.
					else
					{
						Console.Clear();
					}
				}
				// Input did not parse to an int.
				else
				{
					Console.Clear();
				}
			}// End WhileRunBooks
		}// End Main
	}// End Class
}// End NameSpace