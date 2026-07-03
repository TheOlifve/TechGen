namespace Cache;
class Cache<TKey, TValue>
{
    private int _lifeTime;
    private Dictionary<TKey,  Entity> Values { get; }
    
    private class Entity
    {
        public TValue Value;
        public DateTime Expiration;
        
        public Entity(TValue value, DateTime expiration)
        {
            Value = value;
            Expiration = expiration;
        }
    }
    
    public Cache(int lifeTime)
    {
        Values = new Dictionary<TKey, Entity>();
        _lifeTime = lifeTime;
    }

    public void Add(TKey key, TValue value)
    {
        try
        {
            Values.Add(key, new Entity(value, DateTime.Now.AddSeconds(_lifeTime)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool TryGet(TKey key, out TValue value)
    {
        Entity valueEntity;
        
        if (Values.TryGetValue(key, out valueEntity))
        {
            if (valueEntity.Expiration > DateTime.Now)
            {
                value = valueEntity.Value;
                return true;
            }
            else
                Values.Remove(key);
        }
        
        value = default(TValue);
        return false;
    }
}