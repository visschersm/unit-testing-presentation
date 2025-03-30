using MathLibrary;

namespace MathLibraryTests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void Addtion_ValidXandY_SomeReturnAddition()
    {
        // Arrange
        var sut = new MathLib();
        var validX = 1;
        var validY = 2;
        var expectedResult = 3;

        // Act
        var result = sut.Addition(validX, validY);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    public void Addition_XBelowZero_ArgumentOutOfRangeException()
    {
        // Arrange
        var sut = new MathLib();
        int invalidX = -1;
        int validY = 2;

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(()
            => sut.Addition(invalidX, validY));
    }

    [TestMethod]
    public void Addition_YBelowZero_ArgumentOutOfRangeException()
    {
        // Arrange
        var sut = new MathLib();
        int validX = 1;
        int invalidY = -2;

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(()
            => sut.Addition(validX, invalidY));
    }
}
