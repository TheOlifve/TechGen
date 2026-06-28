namespace ex03;

public class MyClass
{
    private bool _init = false;
    
    private MyClass()
    {
        
    }

    private void Initialize()
    {
        _init = true;
    }

    static public MyClass Create()
    {
        MyClass ret = new MyClass();
        
        Console.WriteLine($"Instance Init Status: {ret._init}");
        ret.Initialize();
        Console.WriteLine($"Instance Init Status: {ret._init}");
        
        return ret;
    }
}