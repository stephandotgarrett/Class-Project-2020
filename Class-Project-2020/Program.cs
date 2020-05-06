using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Class_Project_2020
{
	class Program
	{
		private static List<Pokemon> pokemon = new List<Pokemon>();
		private static List<Pokemon> player = new List<Pokemon>();
		private static string name = "";
		private static string partner = "";
		private static string toCatch = "";
		private static string caught = "";
		private static string currentDirectory = Directory.GetCurrentDirectory();
		private static DirectoryInfo directory = new DirectoryInfo(currentDirectory);
		private static string pokemonFile = Path.Combine(directory.FullName, "Pokemon.txt");
		private static string playerFile = Path.Combine(directory.FullName, "Player.txt");

		static void Main(string[] args)
		{
            pokemon = CreatePokeList(pokemonFile);
			RunMenu();
		}


        //////    MENU METHODS    //////

        //Method for main menu
		public static void RunMenu()
		{
			Console.Clear();
			Console.WriteLine("Are you ready for an adventure?");
			Console.WriteLine("1) Let's do it!");
			Console.WriteLine("2) No, I'm scared!");

			string runChoice = Console.ReadLine();

			if (runChoice == "1")
            {
				Console.Clear();
				Console.WriteLine("Awesome! I can't wait to get going!");
				RunMenuPartTwo();
			}
			else if (runChoice == "2")
            {
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You should be. This world is a dangerous place...");
				Console.WriteLine("Goodbye");
				System.Threading.Thread.Sleep(1000);
				Console.ResetColor();
				return;
            }
			else {
				Console.WriteLine("I'm sorry, that doesn't seem to be one of the options");
				System.Threading.Thread.Sleep(1000);
				RunMenu();
            }
		}

        //Method for secondary menu to run if player chooses to proceed with playing the game
        public static void RunMenuPartTwo()
        {
			Console.WriteLine("1) New adventurer");
			Console.WriteLine("2) Continue adventure");
			Console.WriteLine("3) Back to main menu");
			Console.WriteLine("4) I changed my mind, I really am scared!");

			string contChoice = Console.ReadLine();

			if (contChoice == "1")
			{
                CreateNewPlayer();
				PrintPokemon();
				Console.WriteLine("Good luck, {0} and {1}!", name, partner);
				System.Threading.Thread.Sleep(1000);
				TravelMenu();
			}
			else if (contChoice == "2")
			{
				//look for player in player.txt file
				LookForPlayerFile();
			}
			else if (contChoice == "3")
			{
				RunMenu();
			}
			else if (contChoice == "4")
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You should be. This world is a dangerous place...");
				Console.WriteLine("Goodbye");
				System.Threading.Thread.Sleep(1000);
				Console.ResetColor();
				return;
			}
			else
			{
				Console.WriteLine("I'm sorry, that's not one of the options");
				System.Threading.Thread.Sleep(1000);
				Console.Clear();
				Console.WriteLine("I can't wait to get going!");
				RunMenuPartTwo();
			}
		}


        //Method menu for creating new player
		public static void CreateNewPlayer()
		{
			Console.Clear();
			Console.WriteLine("It's always great to meet new people!");
			Console.WriteLine("What's your name?");
			name = Console.ReadLine();
			player = CreatePokeList(pokemonFile);
			Console.Clear();
			Console.WriteLine("You souldn't go alone, {0}", name);
			Console.WriteLine("Pick one of these pokemon as your partner for the journey:");
			Console.WriteLine("1) Bulbasaur");
			Console.WriteLine("2) Charmander");
			Console.WriteLine("3) Squirtle");
			Console.WriteLine("4) Pikachu");

			string partnerChoice = Console.ReadLine();

			if (partnerChoice == "1") { partner = "Bulbasaur"; }
			else if (partnerChoice == "2") { partner = "Charmander"; }
			else if (partnerChoice == "3") { partner = "Squirtle"; }
			else if (partnerChoice == "4") { partner = "Pikachu"; }
			else { Console.WriteLine("That's not an option"); };

			foreach (Pokemon pokemon in player)
			{
				if (partner == pokemon._name) { pokemon._avail = true; };
			}
			SavePlayer();
		}


		//Method menu for player travel to find pokemon
		public static void TravelMenu()
		{
			string direction = "";

			Console.Clear();
			Console.WriteLine("Which direction will you travel?");
			Console.WriteLine("1) North");
			Console.WriteLine("2) South");
			Console.WriteLine("3) East");
			Console.WriteLine("4) West");

			string directionChoice = Console.ReadLine();

			if (directionChoice == "1") { direction = "North"; }
			else if (directionChoice == "2") { direction = "South"; }
			else if (directionChoice == "3") { direction = "East"; }
			else if (directionChoice == "4") { direction = "West"; }
			else { direction = "fail";  }

			Console.Clear();
			if (direction != "fail")
			{
				Console.WriteLine("Heading {0}.", direction);
				System.Threading.Thread.Sleep(350);
				Console.Clear();
				Console.WriteLine("Heading {0}..", direction);
				System.Threading.Thread.Sleep(350);
				Console.Clear();
				Console.WriteLine("Heading {0}...", direction);
				System.Threading.Thread.Sleep(350);
				Console.Clear();
				Console.WriteLine("Heading {0}.", direction);
				System.Threading.Thread.Sleep(350);
				Console.Clear();
				Console.WriteLine("Heading {0}..", direction);
				System.Threading.Thread.Sleep(350);
				Console.Clear();
				Console.WriteLine("Heading {0}...", direction);
				System.Threading.Thread.Sleep(350);
				Console.Clear();

				toCatch = FindPokemon();
				Console.WriteLine("A wild {0} has appeared!", toCatch);
				AttemptToCatchMenu();
			}
			else
			{
				Console.WriteLine("That's not one of the options");
				System.Threading.Thread.Sleep(1000);
				TravelMenu();
			}
		}


		//Method menu to allow player the option to catch a pokemon found in travel
		public static void AttemptToCatchMenu()
		{
			string catchChoice = "";

			Console.WriteLine("What are you going to do with {0}?", toCatch);
			Console.WriteLine("1) Catch it!");
			Console.WriteLine("2) Leave it and continue on my journey");
			Console.WriteLine("3) Leave it and go home");
			catchChoice = Console.ReadLine();

			if (catchChoice == "1")
			{
				CatchMenu();
			}
			else if (catchChoice == "2")
			{
				PrintPokemon();
				TravelMenu();
			}
			else if (catchChoice == "3")
			{
				Console.WriteLine("Thanks for playing!");
				System.Threading.Thread.Sleep(1000);
				RunMenu();
			}
			else
			{
				Console.WriteLine("That's not one of the options");
				AttemptToCatchMenu();
			}
		}


		//Method menu if player chooses to attempt to catch a pokemon found in travel
		public static void CatchMenu()
		{
			caught = AttemptCatch(toCatch);
			if (caught == toCatch)
			{
				CaughtMenu();
			}
			else
			{
				Console.WriteLine(caught);
				Console.WriteLine("Let's keep going and see what we find");
				System.Threading.Thread.Sleep(2000);
				PrintPokemon();
				TravelMenu();
			}
		}


        //Method menu if players attempt to catch pokemon is successful
		public static void CaughtMenu()
		{
			Console.WriteLine("You caught {0}", caught);
			Console.WriteLine("What now?");
			Console.WriteLine("1) Keep going");
			Console.WriteLine("2) Head home");
			string choice = Console.ReadLine();
			if (choice == "1")
			{
				PrintPokemon();
				TravelMenu();
			}
			else if (choice == "2")
			{
				RunMenu();
			}
			else
			{
				Console.WriteLine("That's not one of the options");
				CaughtMenu();
			}
		}



		////////    GAMEPLAY METHODS    ////////

		//Method for finding pokemon in travel
		public static string FindPokemon()
		{
			string pokemonToCatch = "";
			List<string> availablePokemon = new List<string>();

			foreach (Pokemon availPokemon in player)
			{
				if (availPokemon._avail == false)
				{
					availablePokemon.Add(availPokemon._number);
				}
			}

			string[] availArray = availablePokemon.ToArray();

			var random = new Random();
			int index = random.Next(0, availArray.Length);
			string pokemonNumber = availArray[index];

			foreach (Pokemon pokemon in player)
			{
				if (pokemonNumber == pokemon._number)
				{
					pokemonToCatch = pokemon._name;
					return pokemonToCatch;
				}
				else if (availArray.Length == 0)
				{
					pokemonToCatch = "You caught them all! You really are the best!";
					SavePlayer();
				}
			}
			return pokemonToCatch;
		}

        //Method to allow printing of pokemon caught
        public static void PrintPokemon()
        {
			string choice = "";
			Console.WriteLine("Would you like to see your collection of pokemon?");
			Console.WriteLine("1) Yes");
			Console.WriteLine("2) No");
			choice = Console.ReadLine();

			if (choice == "1")
			{
				foreach (Pokemon caughtPokemon in player)
				{
					if (caughtPokemon._avail == true)
					{
						Console.WriteLine(caughtPokemon._name);
					}
					else { }
				}
			}
            else if (choice == "2")
            {
				Console.WriteLine("Ok, you'd better get going then");
            }
            else
            {
				Console.WriteLine("I didn't understand that choice");
				PrintPokemon();
            }
			Console.WriteLine("Press any key to continue");
			Console.ReadLine();
        }

		//Method to catch a pokemon found while travelling
		public static string AttemptCatch(string pokemonToAttempt)
        {
			string Caught = "";
			string pokeName = pokemonToAttempt;
			bool caughtOrNot = TryToCatch();
			foreach (Pokemon playerpokemon in player)
            {
                if (pokemonToAttempt == playerpokemon._name && caughtOrNot == true)
                {
					playerpokemon._avail = true;
					SavePlayer();
					Caught = playerpokemon._name;
					return Caught;
                }
                else if (caughtOrNot == false)
                {
                    Caught = pokemonToAttempt + " got away!";
					return Caught;
                }
            }
			return Caught;
        }


		//Method to return result of attempt to catch pokemon
		public static bool TryToCatch()
		{
			var rand1 = new Random();
			int val1 = rand1.Next(1, 3);
			var rand2 = new Random();
			int val2 = rand2.Next(1, 10);

			if (val1 > val2) { return false; }
			else { return true; }
		}


        //Method to read pokemon txt file into list for gameplay
		public static List<Pokemon> CreatePokeList(string fileName)
		{
			var mons = new List<Pokemon>();
			using (var reader = new StreamReader(fileName))
			{
				string line = "";
				reader.ReadLine();
				reader.ReadLine();
				while ((line = reader.ReadLine()) != null)
				{
					
					var pokemon = new Pokemon();
					string[] values = line.Split(',');

					string num = values[0];
					pokemon._number = num;

					string name = values[1];
					pokemon._name = name;

                    bool avail;
                    if (Boolean.TryParse(values[2], out avail))
                    {
                        pokemon._avail = avail;
                    }

                    mons.Add(pokemon);
				}
			}
			return mons;
		}


		//Method to check if previous player file has information
		public static void LookForPlayerFile()
		{
			using (var reader = new StreamReader(playerFile))
			{
				name = reader.ReadLine();
				partner = reader.ReadLine();
			}
			if (name == "") // Delivers message if no stored player is found
			{
				Console.Clear();
				Console.WriteLine("It looks like there are no previous players");
				RunMenu();
			}
			else if (name != "") // Runs continue if previous player is found
			{
				Console.Clear();
				Console.WriteLine("Good luck, {0} and {1}!", name, partner);
				System.Threading.Thread.Sleep(1000);
				player = CreatePokeList(playerFile);
				PrintPokemon();
				TravelMenu();
			}
		}


		//Method to save a new players name, his partner, and all pokemon available to catch within the game
		public static void SavePlayer()
		{
			using (StreamWriter sw = new StreamWriter("Player.txt"))
			{
				sw.WriteLine(name);
				sw.WriteLine(partner);
				foreach (Pokemon pokemon in player)
				{
					sw.WriteLine(
                        pokemon._number + "," +
                        pokemon._name + "," +
                        pokemon._avail);
				}
			}
		}

	}
}