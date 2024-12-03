using FluentAssertions;

namespace FluentAssertionsTest;

public class Tests
{
    [Test]
    public void Test()
    {
        MyObj[] arr = [new("first"), new("second")];
        arr.Should().BeEquivalentTo(["first", "second"], options => options
            .Using<object>(ctx => ctx.Subject.Should().Be(ctx.Expectation.ToString()))
            .When(o => o.RuntimeType == typeof(MyObj) || o.RuntimeType == typeof(string))
        );
    }
}

class MyObj(string val) : IEquatable<string>, IEquatable<MyObj>
{
    public override bool Equals(object? obj) => obj switch
    {
        MyObj myObj => Equals(myObj),
        string str => Equals(str),
        _ => false
    };

    public bool Equals(MyObj? other) => val == other?.Val;

    public bool Equals(string? other) => val == other;

    public string Val => val;

    public override string ToString() => val;

    public override int GetHashCode() => val.GetHashCode();
}