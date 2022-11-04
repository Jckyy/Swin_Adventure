using NUnit.Framework;
using Swin_Adventure;

namespace SwinAdventureTests
{
    public class IdentifiableObjectTests
    {
        IdentifiableObject _id;

        [SetUp]
        public void Setup()
        {
            // Make IdentifiableObject, Item(s), and Inventory for testing
            _id = new IdentifiableObject(new string[] { "fred", "bob" });
        }




        // Identifiable Object Unit Tests

        [Test]
        public void TestAreYou()
        {
            Assert.That(_id.AreYou("fred"), Is.EqualTo(true));
            Assert.That(_id.AreYou("bob"), Is.EqualTo(true));
        }

        [Test]
        public void TestNotAreYou()
        {
            Assert.That(_id.AreYou("wilma"), Is.EqualTo(false));
            Assert.That(_id.AreYou("boby"), Is.EqualTo(false));

        }

        [Test]
        public void TestCaseSensitiveAreYou()
        {
            Assert.That(_id.AreYou("FRED"), Is.EqualTo(true));
            Assert.That(_id.AreYou("bOB"), Is.EqualTo(true));
        }

        [Test]
        public void TestFirstID()
        {
            Assert.IsTrue(_id.FirstID == "fred");
        }

        [Test]
        public void TestFirstIDWhenEmpty()
        {
            // Create _emptyid object
            IdentifiableObject _emptyid = new IdentifiableObject(new string[] { });
            Assert.IsTrue(_emptyid.FirstID == "");
        }

        [Test]
        public void TestAddIdentifier()
        {
            _id.AddIdentifier("Wilma");
            Assert.That(_id.AreYou("wilma"), Is.EqualTo(true));
            Assert.That(_id.AreYou("fred"), Is.EqualTo(true));
            Assert.That(_id.AreYou("bob"), Is.EqualTo(true));
        }
    }
}