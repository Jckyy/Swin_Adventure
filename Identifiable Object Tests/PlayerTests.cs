using NUnit.Framework;
using Swin_Adventure;

namespace SwinAdventureTests
{
    public class PlayerTests
    {
        Player _player;
        Item _item;
        Item _item2;

        [SetUp]
        public void Setup()
        {
            _item = new Item(new string[] { "sword" }, "Bronze Sword", "A sword made out of bronze");
            _item2 = new Item(new string[] { "water" }, "Water", "You can drink this.");

            //Make player and populate their inventory
            _player = new Player("Jacky", "a fledgling coder");
            _player.Inventory.Put(_item);
            _player.Inventory.Put(_item2);
        }

        // Player Unit Tests

        [Test]
        public void TestPlayerIsIdentifiable()
        {
            // Using AreYou from IdentifiableObject, player returns itself on "me" and "inventory"
            Assert.That(_player.AreYou("me"), Is.EqualTo(true));
            Assert.That(_player.AreYou("inventory"), Is.EqualTo(true));
        }

        [Test]
        public void TestPlayerLocatesItems()
        {
            Assert.That(_player.Locate("sword"), Is.EqualTo(_item));
        }

        [Test]
        public void TestPlayerLocate()
        {
            // Making player locate itself
            Assert.That(_player.Locate("me"), Is.EqualTo(_player));
            Assert.That(_player.Locate("inventory"), Is.EqualTo(_player));
        }

        [Test]
        public void TestPlayerLocatesNothing()
        {
            Assert.That(_player.Locate("shield"), Is.EqualTo(null));
        }

        [Test]
        public void TestPlayerFullDescription()
        {
            Assert.That(_player.FullDescription, Is.EqualTo("You are Jacky, a fledgling coder.\n\n" +
                "You are carrying:" +
                "\n\t-Bronze Sword (sword)" +
                "\n\t-Water (water)"));
        }
    }
}
