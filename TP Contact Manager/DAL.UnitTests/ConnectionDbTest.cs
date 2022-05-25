using DAL.UnitTests.Mocking;
using NUnit.Framework;

namespace DAL.UnitTests
{
    public class ConnectionDbTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void OpenConnection_WhenCalled_SetConnectionProperty()
        {
            FakeConnectionDb fakeConnectionDb = new FakeConnectionDb();
            
            fakeConnectionDb.OpenConnection();
            
            Assert.That(fakeConnectionDb.Connection, Is.EqualTo("a"));
        }
        
        [Test]
        public void CreateCommand_WhenCalled_SetCommandProperty()
        {
            FakeConnectionDb fakeConnectionDb = new FakeConnectionDb();
            
            fakeConnectionDb.OpenConnection();
            fakeConnectionDb.CreateCommand();
            
            Assert.That(fakeConnectionDb.Command, Is.EqualTo("a"));
        }
        
        [Test]
        public void ExecuteCommand_WhenCalled_SetReaderProperty()
        {
            FakeConnectionDb fakeConnectionDb = new FakeConnectionDb();
            
            fakeConnectionDb.OpenConnection();
            fakeConnectionDb.CreateCommand();
            fakeConnectionDb.ExecuteCommand("a");
            
            Assert.That(fakeConnectionDb.Reader, Is.EqualTo("aa"));
        }
        
        [Test]
        public void CloseConnection_WhenCalled_ClearProperties()
        {
            FakeConnectionDb fakeConnectionDb = new FakeConnectionDb();
            
            fakeConnectionDb.CloseConnection();
            
            Assert.That(fakeConnectionDb.Reader, Is.Null);
            Assert.That(fakeConnectionDb.Command, Is.Null);
            Assert.That(fakeConnectionDb.Connection, Is.Null);
        }
    }
}