namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {

        Database database;

        [SetUp]
        public void Setup()
        {
            database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"), new Person(3, "Sasho"));
        }

        [Test]
        public void CreatingDatabaseCountShouldBeCorrect()
        {
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void AddMethodShouldWorkCorrectly()
        {
            // Arrange
            int expectedCount = 4;
            Person person = new Person(4, "Misho");

            // Act
            database.Add(person);
            int actualCount = database.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveMethodShouldWorkCorrectly()
        {
            // Arrange
            int expectedCount = 2;
            
            // Act
            database.Remove();

            // Assert
            Assert.AreEqual(expectedCount, database.Count);
        }

        [Test]
        public void AddMethodThrowsExceptionIfDataLengthIsMoreThan16()
        {
            // Arrange
            Person[] persons = new Person[17];

            // Act
            // Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Database(persons));
            Assert.AreEqual("Provided data length should be in range [0..16]!", ex.Message);
        }

        [Test]
        public void CreatingDatabaseShouldThrowExceptionWhenCountIsMoreThan16()
        {

            Person[] persons =
            {
            new Person(1, "Pesho"),
            new Person(2, "Gosho"),
            new Person(3, "Ivan_Ivan"),
            new Person(4, "Pesho_ivanov"),
            new Person(5, "Gosho_Naskov"),
            new Person(6, "Pesh-Peshov"),
            new Person(7, "Ivan_Kaloqnov"),
            new Person(8, "Ivan_Draganchov"),
            new Person(9, "Asen"),
            new Person(10, "Jivko"),
            new Person(11, "Toshko"),
            new Person(12, "Moshko"),
            new Person(13, "Foshko"),
            new Person(14, "Loshko"),
            new Person(15, "Roshko"),
            new Person(16, "Boshko")
            };

            database = new Database(persons);

            Person finalPerson = new Person(17, "Moshlqk");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => database.Add(finalPerson));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
        }

        [Test]
        public void AddMethodShouldThrowExceptionIfThereIsAlreadyUserWithSameUsername()
        {
            // Arrange
            Database database = new Database(new Person(1, "Pesho"));
            Person person = new Person(2, "Pesho");

            // Act
            // Assert
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.Add(person));
            Assert.AreEqual("There is already user with this username!", ex.Message);
        }

        [Test]
        public void AddMethodShouldThrowExceptionIfThereIsAlreadyUserWithSameId()
        {
            // Arrange
            Database database = new Database(new Person(1, "Pesho"));
            Person person = new Person(1, "Gosho");

            // Act
            // Assert
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.Add(person));
            Assert.AreEqual("There is already user with this Id!", ex.Message);
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

        [Test]
        public void RemoveMethodShouldThrowExceptionIfDatabaseIsEmpty()
        {
            // Arrange
            database = new Database();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void FindByUsernameMethodShouldThrowExceptionIfThereIsNoUserWithGivenUsername()
        {
            // Arrange
            string username = "Misho";

            // Act
            // Assert
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.FindByUsername(username));
            Assert.AreEqual("No user is present by this username!", ex.Message);
        }

        [Test]
        public void FindByUsernameMethodShouldThrowExceptionIfUsernameIsNull()
        {
            // Arrange
            string username = null;

            // Act
            // Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => database.FindByUsername(username));
            Assert.AreEqual("Username parameter is null!", ex.ParamName);
        }

        [Test]
        public void FindByUsernameMethodShouldReturnCorrectUser()
        {
            // Arrange
            string username = "Pesho";
            Person expectedPerson = new Person(1, "Pesho");

            // Act
            Person actualPerson = database.FindByUsername(username);

            // Assert
            Assert.AreEqual(expectedPerson.UserName, actualPerson.UserName);
            Assert.AreEqual(expectedPerson.Id, actualPerson.Id);
        }

        [Test]
        public void FindByIdMethodShouldThrowExceptionIfThereIsNoUserWithGivenId()
        {
            // Arrange
            long id = 10;

            // Act
            // Assert
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.FindById(id));
            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }

        [Test]
        public void FindByIdMethodShouldThrowExceptionIfIdIsNegative()
        {
            // Arrange
            // Act
            // Assert
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
            Assert.AreEqual("Id should be a positive number!", ex.ParamName);
        }

        [Test]
        public void FindByIdMethodShouldReturnCorrectUser()
        {
            // Arrange
            long id = 1;
            Person expectedPerson = new Person(1, "Pesho");

            // Act
            Person actualPerson = database.FindById(id);

            // Assert
            Assert.AreEqual(expectedPerson.UserName, actualPerson.UserName);
            Assert.AreEqual(expectedPerson.Id, actualPerson.Id);
        }
    }
}