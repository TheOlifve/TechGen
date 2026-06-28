using System.Text;

namespace ex02;

public delegate bool Predicate<T>(T input);

public class Filter<T>
{
    public List<T> FilteredItems = new List<T>();
    public Predicate<T>? Predicate = null;
    
    public Filter(Predicate<T> predicate)
    {
        Predicate = predicate;
    }
    
    public void FilterList(List<T> items)
    {
        foreach (var item in items)
            if (Predicate.Invoke(item))
                FilteredItems.Add(item);
    }
}