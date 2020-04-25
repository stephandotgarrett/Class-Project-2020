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
        private static string currentDirectory = Directory.GetCurrentDirectory();
		private static DirectoryInfo directory = new DirectoryInfo(currentDirectory);
		private static string pokemonFile = Path.Combine(directory.FullName, "Pokemon.txt");
		private static string playerFile = Path.Combine(directory.FullName, "Player.txt");

		static void Main(string[] args)
		{
            pokemon = CreatePokeList(pokemonFile);
			RunMenu();
		}

		public static void RunMenu()
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("Are you ready for an adventure?");
			Console.WriteLine("1) ");
			Console.WriteLine("2) ");
			Console.WriteLine("3) No, I'm scared!");
			Console.WriteLine("4) Get back to the 'real' world");

			string runChoice = Console.ReadLine();

			if (runChoice == "1")
            {
				WritePlayerInfo();
			}
			else if (runChoice == "2")
            {
				ListPokemon(pokemon);
            }
			else if (runChoice == "3")
            {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You should be. The world is a dangerous place...");
				Console.ResetColor();
				Console.WriteLine("Press ENTER to continue");
				Console.ReadLine();
				RunMenu();
            }
            else if (runChoice == "4")
			{
				Console.Clear();
				Console.WriteLine("Goodbye");
				System.Threading.Thread.Sleep(2000);
				return;
			}
			else { }
		}

		public static void ContinueMenu()
		{
			Console.Clear();
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("1) ");
			Console.WriteLine("2) ");
			Console.WriteLine("3) ");

			string ContChoice = Console.ReadLine();

			if (ContChoice == "1") { }
			else if (ContChoice == "2") { }
			else if (ContChoice == "3") { }
			else { }
		}

		public static List<Pokemon> CreatePokeList(string fileName)
		{
			var mons = new List<Pokemon>();
			using (var reader = new StreamReader(fileName))
			{
				string line = "";
				reader.ReadLine();
				while ((line = reader.ReadLine()) != null)
				{
					
					var pokemon = new Pokemon();
					string[] values = line.Split(',');

					string num = values[0];
					pokemon._number = Int32.Parse(num);

					string name = values[1];
					pokemon._name = name;

					string type1 = values[2];
					pokemon._type1 = type1;

					string type2 = values[3];
					pokemon._type2 = type2;

					string total = values[4];
					pokemon._total = Int32.Parse(total);

					string hitPoints = values[5];
					pokemon._hitPoints = Int32.Parse(hitPoints);

					string attack = values[6];
					pokemon._attack = Int32.Parse(attack);

					string defense = values[7];
					pokemon._defense = Int32.Parse(defense);

                    bool avail;
                    if (Boolean.TryParse(values[12], out avail))
                    {
                        pokemon._avail = avail;
                    }

                    mons.Add(pokemon);
				}
			}
			return mons;
		}



		public static void ListPokemon(List<Pokemon> pokemons)
		{
			foreach (Pokemon pokemon in pokemons)
			{
				if (pokemon._avail == true)
				{
					Console.WriteLine(pokemon._number + " " + pokemon._name);
				}
			}
		}



		public static void WritePlayerInfo()
		{
			using (StreamWriter sw = new StreamWriter("Character.txt"))
			{
				sw.WriteLine(name);
				foreach (Pokemon pokemon in player)
				{
					//#,Name,Type 1,Type 2,Total,HP,Attack,Defense,Sp. Atk,Sp. Def,Speed,Generation,Legendary
					//public int _number { get; set; }
					//public string _name { get; set; }
					//public string _type1 { get; set; }
					//public string _type2 { get; set; }
					//public int _total { get; set; }
					//public int _hitPoints { get; set; }
					//public int _attack { get; set; }
					//public int _defense { get; set; }
					//public bool _avail { get; set; }
					sw.WriteLine(pokemon._number + "," + pokemon._name + "," + pokemon._type1 + ", " + pokemon._type2 + ", " + pokemon._total + ", " + pokemon._hitPoints + ", " + pokemon._attack + ", " + pokemon._defense + ", " + pokemon._avail);
				}
			}
		}

	}
}