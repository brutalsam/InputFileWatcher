using NUnit.Framework;

namespace FileImporter.UnitTest
{
    [TestFixture]
    class ContextTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            try
            {
                Arrange();
                Act();
            }
            catch
            {
                TearDown();
                throw;
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            TidyUp();
        }

        protected virtual void Arrange()
        {
        }

        protected virtual void Act()
        {
        }

        protected virtual void TidyUp()
        {
        }
    }
}
