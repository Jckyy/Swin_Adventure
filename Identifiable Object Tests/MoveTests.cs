using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swin_Adventure;
using NUnit.Framework;
using Path = Swin_Adventure.Path;

namespace SwinAdventureTests
{
    public class MoveTests
    {
        Move _move;
        Player _player;
        Location _location, _destination;
        Path _path;

        [SetUp]
        public void Setup()
        {
            _move = new Move();


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
            _path = new Swin_Adventure.Path(new string[] { "north", "up" }, _destination);

            // Put Path in player's location
            _player.Location.AddPath(_path);
        }

        [Test]
        public void TestMoveIdentifiers()
        {
            bool expected = true;

            // "move"
            bool actual = _move.AreYou("move");
            Assert.That(actual, Is.EqualTo(expected));

            // "go"
            actual = _move.AreYou("go");
            Assert.That(actual, Is.EqualTo(expected));

            // "head"
            actual = _move.AreYou("head");
            Assert.That(actual, Is.EqualTo(expected));

            // "leave"
            actual = _move.AreYou("leave");
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestMovePlayerLength2()
        {
            // case 2: "move north"
            _move.Execute(_player, new string[] { "move", "north" });
            Location expected = _destination;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));

            // Moving the player back to the starting location
            // case 2: "move south"
            _player.Location = _destination;
            _move.Execute(_player, new string[] { "move", "south" });
            expected = _location;
            actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));

            // case 2: "move forest"
            _player.Location = _location;
            _move.Execute(_player, new string[] { "move", "forest" });
            expected = _destination;
            actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));

            // case 2: "move town"
            _player.Location = _destination;
            _move.Execute(_player, new string[] { "move", "town" });
            expected = _location;
            actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));


        }

        [Test]
        public void TestMovePlayerLength3()
        {
            // case 3: "move to forest"
            _player.Location = _location;
            _move.Execute(_player, new string[] { "move", "to", "forest" });
            Location expected = _destination;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));

            // Moving back to source
            // case 3: "move to town"
            _player.Location = _destination;
            _move.Execute(_player, new string[] { "move", "to", "town" });
            expected = _location;
            actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));

        }

        // Testing the string return of a move command
        [Test]
        public void TestMovePlayer2()
        {
            string expected_string = "Jacky moved to Slime Forest.";
            string actual_string = _move.Execute(_player, new string[] { "move", "north" });
            Assert.That(actual_string, Is.EqualTo(expected_string));
        }

        [Test] 
        public void TestInvalidPlayerMove()
        {
            // Command = "move"
            string expected = "I don't know how to move like that.";
            string actual = _move.Execute(_player, new string[] { "move" });
            Assert.That(actual, Is.EqualTo(expected));

            // Player is already at the destination
            _player.Location = _destination;
            expected = "Could not move the player.";
            actual = _move.Execute(_player, new string[] { "move", "forest" });
            Assert.That(actual, Is.EqualTo(expected));

            // Command = "jump north"
            expected = "Error in move input.";
            actual = _move.Execute(_player, new string[] { "jump", "north" });
            Assert.That(actual, Is.EqualTo(expected));

            // There is no path with the id or direction
            _player.Location = _location;
             expected = "Could not move the player";
            actual = _move.Execute(_player, new string[] { "move", "south" });
        }

        [Test]
        public void TestMultiplePaths()
        {
            // Setup 2nd path and its destination
            Location _destination2 = new Location(new string[] { "swamp" },
                        "Murky Swamp",
                        "Why is there a dangerous looking swamp next to the town?");
            Path _path2 = new Swin_Adventure.Path(new string[] { "south", "down" }, _destination2);
            // Put Path in player's location
            _player.Location.AddPath(_path2);


            // Move player to the 2nd path
            _move.Execute(_player, new string[] { "move", "south" });

            Location expected = _destination2;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));

            // Player moves back
            _player.Location = _destination2;
            _move.Execute(_player, new string[] { "move", "north" });
            expected = _location;
            actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestBlockedPath()
        {
            _path.IsBlocked = true;
            _move.Execute(_player, new string[] { "move", "north" });

            Location expected = _location;
            Location actual = _player.Location;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
