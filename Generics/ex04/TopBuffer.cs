namespace ex04;

public class TopBuffer<T> where T: IComparable<T>
{
    public T[] Buffer;
    private int _index = -1;
    
    public TopBuffer(int size)
    {
        Buffer = new T[size];
    }

    public void FindTopElements(T[] other)
    {
        T[] tmp = new T[other.Length];
        
        Array.Copy(other, tmp, other.Length);
        Array.Sort(tmp);

        for (int i = tmp.Length - 1; i >= 0; i--)
        {
            _index++;
            if (_index >= Buffer.Length)
                break;
            Buffer[_index] = tmp[i];
        }
    }
}