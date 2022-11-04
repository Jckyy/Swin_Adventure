using System;

namespace Swin_Adventure // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a player
            Player thePlayer = CreatePlayer();

            // Add starting items for player
            InitialiseInventory(thePlayer);

            // Create Locations & Paths for the player
            InitialiseLocations(thePlayer);

            // Introduction message
            Console.WriteLine("\n" + thePlayer.FullDescription);
            Console.WriteLine(String.Format("Current Location: {0}\n\t{1}", thePlayer.Location.ShortDescription, thePlayer.Location.FullDescription));

            // Keep getting commands from the user
            while (true)
            {
                Console.Write("{0}>",thePlayer.Name);
                string[] userInput = Console.ReadLine().Split();
                
                if (userInput.Contains("exit"))
                {
                    break;
                }

                Look userLook = new Look();
                Console.WriteLine(userLook.Execute(thePlayer, userInput));
            }
        }

        private static Player CreatePlayer()
        {
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            Console.Write("Enter a description: ");
            string playerDescription = Console.ReadLine();

            // Create player
            return new Player(playerName, playerDescription);
        }

        private static void InitialiseInventory(Player p)
        {
            // Create items and place them in the player's inventory
            Item itemSword = new Item(new string[] { "sword" }, "Bronze Sword", "A sword made out of bronze");
            Item itemWater = new Item(new string[] { "water" }, "Water", "You can drink this.");
            p.Inventory.Put(itemSword);
            p.Inventory.Put(itemWater);

            // Create bag and place them in the player's inventory
            Bag bagStarterBag = new Bag(new string[] { "bag" }, "Starter Bag", "A bag for all new adventurers.");
            p.Inventory.Put(bagStarterBag);
            // Create bow and place in the bag
            Item itemBow = new Item(new string[] { "bow" }, "Wooden Bow", "A bow fit for beginners");
            bagStarterBag.Inventory.Put(itemBow);
        }

        private static void InitialiseLocations(Player p)
        {
            // Starting Location
            Location _location1 = new Location(new string[] { "town", "starter" }, "Starter Town", "The town that every new adventurer starts in.");
            _location1.Inventory.Put(new Item(new string[] { "cabbage" }, "Cabbage", "It doesn't look very appetising."));
            
            // Create second location and add it as a path to starting location.
            Location _location2 = new Location(new string[] { "forest", "slime" }, "Slime forest", "You are in the Slime forest. Every adventurer has to start somewhere.");
            _location2.Inventory.Put(new Item(new string[] { "potion" }, "Red Potion", "A small red potion. Restores a meager amount of health."));
            // Create Path and add it to the player
            Path _path1 = new Path(new string[] { "north", "up" }, _location2);
            _location1.AddPath(_path1);
            p.Location = _location1;

            // Setup 2nd path and its destination
            Location _destination2 = new Location(new string[] { "swamp" },
                        "Murky Swamp",
                        "Why is there a dangerous looking swamp next to the town?");
            Path _path2 = new Swin_Adventure.Path(new string[] { "south", "down" }, _destination2);
            // Put Path in player's location
            p.Location.AddPath(_path2);
        }
    }
}