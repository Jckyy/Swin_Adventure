using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Swin_Adventure;
using Path = Swin_Adventure.Path;

namespace SwinAdventureTests
{
    public class PathTests
    {
        Location _location;
        Swin_Adventure.Path _path;
        Location _destination;
        Player _player;

        [SetUp]
        public void Setup()
        {

            // Setup player and their location
            _player = new Player("Jacky", "Level 1 Sprout");
            _location = new Location(new string[] { "town", "starter" }, 
                        "Starter Town", 
                        "The town that every new adventurer starts in.");
            _player.Location = _location;

            // Setup path and its destination
            _destination = new Location(new string[] { "forest", "slime" }, 
                        "Slime Forest", 
                        "Forest full of slimes for beginners.");
            //_path = new Swin_Adventure.Path(new string[] { "north", "up" }, 
            //            "North", 
            //            "Take the North path to enter the Slime Forest.",
            //            _destination);
            _path = new Swin_Adventure.Path(new string[] { "north", "up" }, _destination);

            // Put Path in player's location
            _player.Location.AddPath(_path);
        }

        [Test]
        public void TestPathCanIdentifyItself()
        {
            bool expected = true;
            bool actual = _path.AreYou("north"); 

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestPlayerCanSeePathAndDestination()
        {
            GameObject expected = _path;
            GameObject actual = _player.Locate("north");
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestSeeAvailablePaths()
        {
            string expected = "From your location, you can move:\n\t" +
                "-Slime Forest (forest). Take the north path to move to Slime Forest.";
            string actual = _player.Location.ShowPaths(_player);
            Assert.That(actual, Is.EqualTo(expected));


            // Add another path
            // Setup 2nd path and its destination
            Location _destination2 = new Location(new string[] { "swamp" },
                        "Murky Swamp",
                        "Why is there a dangerous looking swamp next to the town?");
            Path _path2 = new Swin_Adventure.Path(new string[] { "south", "down" }, _destination2);
            // Put Path in player's location
            _player.Location.AddPath(_path2);

            expected = "From your location, you can move:\n\t" +
                "-Slime Forest (forest). Take the north path to move to Slime Forest.\n\t" +
                "-Murky Swamp (swamp). Take the south path to move to Murky Swamp.";
            actual = _player.Location.ShowPaths(_player);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestInvalidPathDestination()
        {
            GameObject expected = null;
            GameObject actual = _player.Locate("mountain");
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetPathDirectionFromDestination()
        {
            string expected = "Slime Forest is north of Starter Town.";
            string actual = _player.Location.FindDirection("forest");
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void TestMovePlayerUsingDirection()
        {
            // Valid id direction
            //_player.Move("north");
            _path.Move(_player, "north");

            Location expected = _destination;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestMovePlayerUsingDestination()
        {
            // Valid id direction
            //_player.Move("slime");
            _path.Move(_player, "slime");

            Location expected = _destination;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestPlayerStaysOnInvalidDirection()
        {
            //Invalid player movement
            _path.Move(_player, "around");

            Location expected = _location;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestSourceDirection()
        {
            string expected = "south";
            string actual = _path.SourceDirection;
            Assert.That(actual, Is.EqualTo(expected));
        }


    }
}
