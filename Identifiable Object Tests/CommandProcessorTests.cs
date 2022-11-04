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
    public class CommandProcessorTests
    {
        CommandProcessor _processor;
        Player _player;
        Location _location, _destination;
        Path _path;
        Move _move;

        [SetUp]
        public void Setup()
        {
            _processor = new CommandProcessor();

            
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
        public void TestLookAndMove()
        {
            string expected = String.Format("Current Location: {0}.\n\t{1}\n\n{2}",
                            _player.Location.ShortDescription,
                            _player.Location.FullDescription,
                            _player.Location.ShowPaths(_player)); ;
            string actual = _processor.Execute(_player, new string[] { "look" });
            Assert.That(expected, Is.EqualTo(actual));


            expected = "Jacky moved to Slime Forest.";
            actual = _processor.Execute(_player, new string[] { "move", "north" });
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestHelpCommand()
        {
            string expected = "The list of the available commands:\n\t-help\n\t-look\n\t-move\n";
            string actual = _processor.Execute(_player, new string[] { "help" });
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestInvalidCommands()
        {
            string expected = "Invalid command. Enter 'help' to see the list of available commands.";
            string actual = _processor.Execute(_player, new string[] { "hello" });
            Assert.That(actual, Is.EqualTo(expected));

            actual = _processor.Execute(_player, new string[] { "" });
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
