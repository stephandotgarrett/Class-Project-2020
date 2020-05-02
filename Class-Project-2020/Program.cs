﻿using System;
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
			Console.WriteLine("1) Let's do it!");
			Console.WriteLine("2) No, I'm scared!");

			string runChoice = Console.ReadLine();

			if (runChoice == "1")
            {
				//CreatePlayer();
				//ListPokemon(pokemon);
				Console.Clear();
				Console.WriteLine("Awesome! I can't wait to get going!");
				Console.WriteLine("1) New adventurer");
				Console.WriteLine("2) Continue adventure");

				string contChoice = Console.ReadLine();

                if (contChoice == "1")
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
					//ListPokemon(player);
					ContinueMenu();
				}
                else if (contChoice == "2")
                {
					//look for player in player.txt file
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
							Console.WriteLine("Welcome back, {0} and {1}", name, partner);
							ContinueMenu();
						}
					}
					player = CreatePokeList(playerFile);

				}
			}
			else if (runChoice == "2")
            {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You should be. This world is a dangerous place...");
				Console.WriteLine("Goodbye");
				System.Threading.Thread.Sleep(2000);
				Console.ResetColor();
				return;
            }
   //         else if (runChoice == "4")
			//{
			//	Console.Clear();
			//	Console.WriteLine("Goodbye");
			//	System.Threading.Thread.Sleep(2000);
			//	return;
			else {
				Console.WriteLine("I'm sorry, that doesn't seem to be one of the options");
				RunMenu();
            }
		}

		public static void ContinueMenu()
		{
			Console.WriteLine("Good luck, {0} and {1}!", name, partner);
			Console.WriteLine("Which direction will you head first?");
			Console.WriteLine("1) North");
			Console.WriteLine("2) South");
			Console.WriteLine("3) East");
			Console.WriteLine("4) West");

			string contChoice = Console.ReadLine();

            //Create method here to decide what happens when travelling

			if (contChoice == "1") { Console.WriteLine("{0} it is", contChoice); }
			else if (contChoice == "2") { Console.WriteLine("{0} it is", contChoice); }
			else if (contChoice == "3") { Console.WriteLine("{0} it is", contChoice); }
			else if (contChoice == "4") { Console.WriteLine("{0} it is", contChoice); }
			else {}
		}

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



		public static void SavePlayer()
		{
			using (StreamWriter sw = new StreamWriter("Player.txt"))
			{
				sw.WriteLine(name);
				sw.WriteLine(partner);
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