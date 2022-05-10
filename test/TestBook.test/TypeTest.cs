using System;
using Xunit;

namespace TestBook.test;

//  delegate: pointer that describe a method
public delegate string LogDelegate(string Message);

public class TypeTest
{
    int count = 0;
    
    [Fact]
    public void TestDelegateMethod()
    {
        // pointer to method, fit in delegate type, declare a variable and used as method
        LogDelegate log = ReturnMessage;
        log += IncreReturnMessage; //multi-cast delegates
        var result = log("test log"); // invoke method twice
        Assert.Equal(2, count);
    }

    public string ReturnMessage(string msg)
    {   
        count ++;
        return msg;
    }

    public string IncreReturnMessage(string msg)
    {
        count ++;
        return msg;
    }

    [Fact]
    public void TestVarReferece()
    {
        var book1 = GetBook("Book 1");
        var book2 = book1;
        //  book1, book2 point to same object
        Assert.Same(book1, book2);
        Assert.True(object.ReferenceEquals(book1, book2));
    }

    [Fact]
    public void TestPassFunction()
    {
        var book1 = GetBook("book 1");
        // pass by value, make the copy of pointer
        SetNameByValue(book1, "new name");
        Assert.Equal("book 1", book1.Name);

        // pass by reference, same object of book1 refers to 
        // SetNameByReference(out book1, "new name");
        SetNameByReference(ref book1, "new name");
        Assert.Equal("new name", book1.Name);
    }

    // pass by reference, identical with "out"(require initalize var)
    //  private void SetNameByReference(out Book book, string name);
    private void SetNameByReference(ref InMemoryBook book, string name)
    {
        book = new InMemoryBook(name);
    }

    private void SetNameByValue(InMemoryBook book, string name)
    {
        book = new InMemoryBook(name);
    }


    InMemoryBook GetBook(string name)
    {
        return new InMemoryBook(name);
    }
}