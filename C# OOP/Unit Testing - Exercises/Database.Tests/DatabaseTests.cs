namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void Setup()
        {
            database = new Database(1, 2, 3);
        }

        [Test]
        public void CreatedDatabaseCountShouldBeCorrect()
        {
            // Arrange
            int expectedCount = 3;

            // Act
            // Assert
            Assert.AreEqual(expectedCount, database.Count);
        }

        /*[Test]
        public void CreatedDatabaseShouldSetInnerArrayWith16Elements()
        {
            // Arrange
            int expectedResult = 16;

            // Act
            Type databaseType = database.GetType();
            FieldInfo dataField = databaseType.GetField("data", BindingFlags.NonPublic | BindingFlags.Instance);
            int[] data = (int[])dataField.GetValue(database);

            // Assert
            Assert.AreEqual(expectedResult, data.Length);
        }*/

        [Test]
        public void FullDatabaseShouldHaveACountOf16()
        {
            // Arrange
            int expectedResult = 16;

            // Act
            database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            // Assert
            Assert.AreEqual(expectedResult, database.Count);
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void CreatingDatabaseShouldAddElementsCorrectly(params int[] expectedData)
        {
            // Arrange
            Database database = new Database(expectedData);

            // Act
            int[] dbData = database.Fetch();

            // Assert
            Assert.That(dbData, Is.EqualTo(expectedData));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })]
        public void CreatingDatabaseShouldThrowExceptionWhenCountIsMoreThan16(params int[] expectedData)
        {
            //Assert.Throws<InvalidOperationException>(() => new Database(expectedData));

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => new Database(expectedData));
            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionWhenDatabaseIsEmpty()
        {
            // Arrange
            database.Remove();
            database.Remove();
            database.Remove();

            // Act
            // Assert
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(database.Remove);
            Assert.AreEqual("The collection is empty!", ex.Message);
        }

        [Test]
        public void DatabaseAddMethodShouldWorkCorrectly()
        {
            // Arrange
            int[] expectedResult = { 1, 2, 3, -4 };

            //Act
            database.Add(-4);

            // Assert
            Assert.AreEqual(expectedResult, database.Fetch());
        }

        [Test]
        public void DatabaseRemoveMethodShouldWorkCorrectly()
        {
            // Arrange
            int[] expectedResult = { 1, 2 };

            // Act
            database.Remove();

            // Assert
            Assert.AreEqual(expectedResult, database.Fetch());
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenCountIsMoreThan16()
        {
            // Arrange
            database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            // Act
            // Assert
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.Add(17));
            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        public void DatabaseShouldReturnElementsCorrectly(int[] expectedDatabaseFetch)
        {
            // Arrange
            database = new Database(expectedDatabaseFetch);

            // Act
            int[] actualFetch = database.Fetch();

            // Assert
            Assert.AreEqual(expectedDatabaseFetch, actualFetch);
        }
    }
}
