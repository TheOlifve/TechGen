using System.Text;

namespace ex02;

public delegate bool Predicate<T>(T input);

public class Filter<T>
{
    public T[] FilteredItems;
    public Predicate<T> Predicate;
    
    public Filter(Predicate<T> predicate)
    {
        Predicate = predicate;
    }
    
    public void FilterList(T[] items)
    {
        int cnt = 0;
        T[] ret = new T[items.Length];

        foreach (T item in items)
        {
            if (Predicate.Invoke(item))
            {
                ret[cnt] = item;
                cnt++;
            }
        }
        
        Array.Resize(ref ret, cnt);
        FilteredItems = ret;
    }
}