using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swin_Adventure;

namespace SwinAdventureTests
{
    internal class LocationTests
    {
        // Need to declare these here since it doesn't work inside of the SetUp
        Location _location;
        Player _player;
        Item _item;

        [SetUp]
        public void Setup()
        {
            _location = new Location(new string[] { "beginnerland" }, "Beginnerland", "Everyone needs to start their journey somewhere.");

            _player = new Player("Jacky", "New Adventurer");
            _player.Location = _location;

            _item = new Item(new string[] { "sword" }, "Bronze Sword", "A sword made out of bronze");
            _location.Inventory.Put(_item);
        }

        [Test]
        public void TestLocationCanFindItself()
        {
            GameObject expected = _location;
            GameObject actual = _location.Locate("beginnerland");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestFindItemsInLocation()
        {
            GameObject expected = _item;
            GameObject actual = _location.Locate("sword");
            Assert.That(actual, Is.EqualTo(expected));

            // Find item that doesn't exist
            GameObject expected2 = null;
            GameObject actual2 = _location.Locate("water");
            Assert.That(actual2, Is.EqualTo(expected2));
        }

        [Test]
        public void TestLocationDetails()
        {
            string expected = "Beginnerland (beginnerland)";
            string actual = _location.ShortDescription;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestPlayerLocation()
        {
            GameObject expected = _location;
            GameObject actual = _player.Location;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestSearchLocationItems()
        { 
            GameObject expected = _item;
            GameObject actual = _player.Location.Locate("sword");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestPlayerFindsItemsInLocation()
        {
            GameObject expected = _item;
            GameObject actual = _player.Locate("sword");

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
