using consoleapp;

namespace tests;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase("1,0,1;0,1,0", ExpectedResult = 3)]
    [TestCase("1,0,1;1,1,0", ExpectedResult = 2)]
    [TestCase("1,1,1,1;0,1,0,0", ExpectedResult = 1)]
    public int Calculations_Test(string input)
    {
        return input.Calculate();
    }
}
