namespace RecentlyUsedList;

public class RecentlyUsedListClass
{
    private readonly List<string> strings = new();

    public void Add(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        strings.Remove(value);

        strings.Insert(0, value);
    }

    public string FindByIndex(int index)
    {
        if (index < 0 || index >= strings.Count)
            throw new IndexOutOfRangeException();

        return strings[index];
    }

    public string Pop()
    {
        if (strings.Count == 0)
            throw new InvalidOperationException();

        var value = strings[0];
        strings.RemoveAt(0);
        return value;
    }
}