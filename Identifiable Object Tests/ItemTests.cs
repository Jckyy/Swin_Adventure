using NUnit.Framework;
using Swin_Adventure;

namespace SwinAdventureTests
{
    public class ItemTests
    {
        Item _item;
        Item _item2;
        
        [SetUp]
        public void Setup()
        {
            _item = new Item(new string[] { "sword" }, "Bronze Sword", "A sword made out of bronze");
            _item2 = new Item(new string[] { "water" }, "Water", "You can drink this.");
        }



        [Test]
        public void TestItemIsIdentifiable()
        {
            Assert.That(_item.AreYou("sword"), Is.EqualTo(true));
        }

        [Test]
        public void TestShortDescription()
        {
            Assert.That(_item.ShortDescription, Is.EqualTo("Bronze Sword (sword)"));
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_item.FullDescription, Is.EqualTo("A sword made out of bronze"));
        }


    }
}
