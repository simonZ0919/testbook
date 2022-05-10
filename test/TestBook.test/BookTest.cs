using Xunit;

namespace TestBook.test;

public class BookTest
{
    [Fact]
    public void TestBookComputeAverage()
    {
        // arrange
        var book = new InMemoryBook("");
        book.AddGrade(99.9);
        book.AddGrade(89.6);
        book.AddGrade(82.5);

        //act
        var result = book.GetStatistics();

        //assert equal with 1 percision 
        Assert.Equal(82.5, result.Low, 1);
        Assert.Equal('A', result.Letter);
    }
}