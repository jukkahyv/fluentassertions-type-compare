using FluentAssertions;

namespace FluentAssertionsTest;

public class Tests
{
    [Test]
    public void Test()
    {
        MyObj[] arr = [new("first"), new("second")];
        arr.Should().BeEquivalentTo(["first", "second"], options => options
            //.WithMapping<string, MyObj>(e => e, s => s.Val)
            .Using<object>(ctx => ctx.Subject.Should().Be(ctx.Expectation.ToString()))
            .When(_ => true)
        );
    }
}

class MyObj(string val) : IEquatable<string>, IEquatable<MyObj>
{
    public override bool Equals(object? obj)
    {
        if (obj is MyObj myObj)
        {
            return Equals(myObj);
        }

        if (obj is string str)
        {
            return Equals(str);
        }

        return false;
    }

    public bool Equals(MyObj? other)
    {
        return val == other?.Val;
    }

    public bool Equals(string? other)
    {
        return val == other;
    }

    public string Val => val;

    public override string ToString()
    {
        return val;
    }
}