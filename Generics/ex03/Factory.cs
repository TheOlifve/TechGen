namespace ex03;

public interface IInitilizer
{
    public void Initilize();
    public bool Initilized { get; }
}

public class Factory<T> where T : IInitilizer, new()
{
    public T Create()
    {
        T instance = new T();
        instance.Initilize();
        return instance;
    }
}

public class ObjectA: IInitilizer
{
    public bool Initilized { get; private set; }
    public void Initilize()
    {
        Initilized = true;
    }
}