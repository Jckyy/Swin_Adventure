using Swin_Adventure;
using NUnit.Framework;

namespace SwinAdventureTests
{
    public class InventoryTests
    {

        Inventory _inventory;
        Item _item;
        Item _item2;
        
        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory();
            _item = new Item(new string[] { "sword" }, "Bronze Sword", "A sword made out of bronze");
            _inventory.Put(_item);
            _item2 = new Item(new string[] { "water" }, "Water", "You can drink this.");

        }

        // Inventory Unit Tests

        [Test]
        public void TestFindItem()
        {
            // Add a sword to inventory and see if it can be found
            
            Assert.That(_inventory.HasItem("sword"), Is.EqualTo(true));
        }

        [Test]
        public void TestNoItemFind()
        {
            Assert.That(_inventory.HasItem("shovel"), Is.EqualTo(false));
        }

        [Test]
        public void TestFetchItem()
        {
            Assert.That(_inventory.Fetch("sword"), Is.EqualTo(_item));
            Assert.That(_inventory.HasItem("sword"), Is.EqualTo(true));
        }

        [Test]
        public void TestTakeItem()
        {
            Assert.That(_inventory.Take("sword"), Is.EqualTo(_item));
            Assert.That(_inventory.Fetch("sword"), Is.EqualTo(null));
        }

        [Test]
        public void TestItemList()
        {
            _inventory.Put(_item2);
            Assert.That(_inventory.ItemList, Is.EqualTo("\n\t-Bronze Sword (sword)\n\t-Water (water)"));
        }
    }
}
