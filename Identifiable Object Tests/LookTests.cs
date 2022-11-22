using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Swin_Adventure;
using Path = Swin_Adventure.Path;

namespace SwinAdventureTests
{
    public class LookTests
    {
        Look _look;
        Player _player;
        Item _item = new Item(new string[] { "gem" }, "Ruby", "A bright red gem. Could be sold for a decent price.");
        //Item _item2 = new Item(new string[] { "water" }, "Water", "You can drink this.");
        Location _location, _destination;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Jacky", "a fledgling coder");
            _look = new Look();
            
            _location = new Location(new string[] { "town" }, "Town", "A random town.");
            _player.Location = _location;
            _player.Location.Inventory.Put(new Item(new string[] { "potion" }, "Red Potion", "Heals a small amount of health."));
        }

        [Test]
        public void TestLookAtMe()
        {
            string expected = "You are Jacky, a fledgling coder.\n\nYou are carrying:";
            string actual = _look.Execute(_player, new string[] { "look", "at", "inventory" });

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void TestLookAtUnknown()
        {
            string expected = "I can't find the gem.";
            string actual = _look.Execute(_player, new string[] { "look", "at", "gem" });

            Assert.That(actual, Is.EqualTo(expected));
        }


        [Test]
        public void TestLookAtGemInMe()
        {
            _player.Inventory.Put(_item);
            string expected = "A bright red gem. Could be sold for a decent price.";
            string actual = _look.Execute(_player, new string[] { "look","at","gem" });

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestLookAtGemInBag()
        {
            Bag _bag = new Bag(new string[] { "small", "bag" }, "Small Bag", "A bag that cannot hold many things.");
            _bag.Inventory.Put(_item);
            _player.Inventory.Put(_bag);

            string expected = "A bright red gem. Could be sold for a decent price.";
            string actual = _look.Execute(_player, new string[] { "look", "at", "gem", "in", "bag" });

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestLookAtGemInNoBag()
        {
            string expected = "I can't find the bag.";
            string actual = _look.Execute(_player, new string[] { "look", "at", "gem", "in", "bag" });

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestLookAtNoGemInBag()
        {
            Bag _bag = new Bag(new string[] { "small", "bag" }, "Small Bag", "A bag that cannot hold many things.");
            _player.Inventory.Put(_bag);

            string expected = "I can't find the gem.";
            string actual = _look.Execute(_player, new string[] { "look", "at", "gem", "in", "bag" });

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void TestInvalidLook()
        {
            string expected = "I don't know how to look like that.";
            string actual = _look.Execute(_player, new string[] { "look", "around" });
            Assert.That(actual, Is.EqualTo(expected));


            expected = "Error in look input.";
            actual = _look.Execute(_player, new string[] { "hello" });
            Assert.That(actual, Is.EqualTo(expected));

            expected = "What do you want to look at?";
            actual = _look.Execute(_player, new string[] { "look", "in", "bag" });
            Assert.That(actual, Is.EqualTo(expected));


            expected = "What do you want to look in?";
            actual = _look.Execute(_player, new string[] { "look", "at", "a", "at", "b" });
            Assert.That(actual, Is.EqualTo(expected));

            expected = "Error in look input.";
            actual = _look.Execute(_player, new string[] { "" });
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestLookAtLocation()
        {
            //_player.Location = new Location(new string[] { "town" }, "Town", "A random town.");

            string expected = "In the Town, there is:\n\t-Red Potion (potion)";
            string actual = _look.Execute(_player, new string[] { "look", "at", "town" });
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestLookForItemInLocation()
        {
            string expected = "Heals a small amount of health.";
            string actual = _look.Execute(_player, new string[] { "look", "at", "potion", "in", "town" });

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestLookAtPaths()
        {
            _destination = new Location(new string[] { "forest", "slime" },
            "Slime Forest",
            "Forest full of slimes for beginners.");
            Path _path = new Swin_Adventure.Path(new string[] { "north", "up" }, _destination);

            // Put Path in player's location
            _player.Location.AddPath(_path);

            string expected = "Current Location: Town (town).\n\t" +
                "A random town.\n\n" +
                "From your location, you can move:" +
                "\n\t-Slime Forest (forest). Take the north path to move to Slime Forest.";
            string actual = _look.Execute(_player, new string[] { "look" });
            Assert.That(expected, Is.EqualTo(actual));


            // Test paths when at destination
            Move _move = new Move();
            _move.Execute(_player, new string[] { "move", "north" });

            expected = "Current Location: Slime Forest (forest).\n\t" +
              "Forest full of slimes for beginners.\n\n" +
              "From your location, you can move:" +
              "\n\t-Town (town). Take the south path to move to Town";
            actual = _look.Execute(_player, new string[] { "look" });
            Assert.That(expected, Is.EqualTo(actual));

        }
    }
}
