using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Swin_Adventure;

namespace SwinAdventureTests
{
    public class BagTests
    {
        Bag _bag;
        Item _item;

        [SetUp]
        public void Setup()
        {
            _bag = new Bag(new string[] { "small", "bag" }, "Small Bag", "A bag that cannot hold many things.");
            _item = new Item(new string[] { "sword" }, "Bronze Sword", "A sword made out of bronze");
            _bag.Inventory.Put(_item);
        }

        [Test]
        public void TestBagLocatesItem() 
        {
            Assert.That(_bag.Locate("sword"), Is.EqualTo(_item));
            Assert.That(_bag.Inventory.HasItem("sword"), Is.EqualTo(true));
        }

        [Test]
        public void TestBagLocatesItself()
        {
            Assert.That(_bag.Locate("bag"), Is.EqualTo(_bag));
        }

        [Test]
        public void TestBagLocatesNothing()
        {
            Assert.That(_bag.Locate("Gold bar"), Is.EqualTo(null));
        }

        [Test]
        public void TestBagFullDescription()
        {
            Assert.That(_bag.FullDescription, Is.EqualTo("In the Small Bag you can see:\n\t-Bronze Sword (sword)"));
        }

        [Test]
        public void TestBagInBag()
        {
            Bag b1 = new Bag(new string[] { "b1" }, "First small bag", "This is the first bag");
            Bag b2 = new Bag(new string[] { "b2" }, "Second small bag", "This is the second bag");
            b1.Inventory.Put(b2);
            Assert.That(b1.Locate("b2"), Is.EqualTo(b2));
            
          
            b1.Inventory.Put(_item);
            Assert.That(b1.Locate("sword"), Is.EqualTo(_item));

            Item _item2 = new Item(new string[] { "water" }, "Water", "You can drink this.");
            b2.Inventory.Put(_item2);

            Assert.That(b1.Locate("water"), Is.EqualTo(null));
        }
    }
}
