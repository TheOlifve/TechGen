namespace ex04;

public class TopBuffer<T> where T: IComparable<T>
{
    public T[] Buffer;
    private int _index = 0;
    private int _indexOfMin = 0;
    
    public TopBuffer(int size)
    {
        Buffer = new T[size];
    }

    public void Add(T obj)
    {
        if (_index < Buffer.Length)
        {
            Buffer[_index++] = obj;
            return;
        }

        for (int i = 0; i < Buffer.Length; i++)
        {
            if (Buffer[i].CompareTo(Buffer[_indexOfMin]) < 0)
                _indexOfMin = i;
        }
        
        if (obj.CompareTo(Buffer[_indexOfMin]) > 0)
            Buffer[_indexOfMin] = obj;
    }

    public void AddRange(T[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            Add(arr[i]);
    }
}