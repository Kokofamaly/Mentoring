using RecentlyUsedList;

namespace RecentlyUsedListTests;

public class UnitTest1
{
    [Fact]
    public void Pop_Returns_LastItem()
    {
        var list = new RecentlyUsedListClass();

        list.Add("Hello");
        list.Add("World");
        var result = list.Pop();

        Assert.Equal(result, list.FindByIndex(1));
    }

    [Fact]
    public void Add_Adds_Value_To_The_End_Of_The_List()
    {
        var list = new RecentlyUsedListClass();
        string expected = "World";

        list.Add("Hello");
        list.Add("World");
        var result = list.Pop();

        Assert.True(expected == result);
    }

    [Fact]
    public void AlreadyContainedValue_Moves_To_The_End_Of_The_List()
    {
        var list = new RecentlyUsedListClass();
        string expected = "World";

        list.Add("World");
        list.Add("Hello");
        list.Add("World");
        var firstValue = list.FindByIndex(0);
        
        Assert.NotSame(expected, firstValue);
    }

    [Fact]
    public void NegativeIndex_Throws_IndexOutOfRangeException()
    {
        var list = new RecentlyUsedListClass();

        Action action = () => list.FindByIndex(-1);

        Assert.Throws<System.IndexOutOfRangeException>(action);
    }


}
