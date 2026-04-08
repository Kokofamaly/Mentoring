namespace RecentlyUsedList;

public class RecentlyUsedListClass
{
    List<string> strings = new List<string>();
    public void Add(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (strings.Contains(value))
        {
            strings.Remove(value);
            strings.Add(value);
        }
        else
        {
            strings.Add(value);
        }
    }

    public string FindByIndex(int index)
    {
        if(index < 0 || index >= strings.Count) throw new IndexOutOfRangeException();
        return strings[index];
    }

    public string Pop()
    {
        return strings[^1];
    }
}
