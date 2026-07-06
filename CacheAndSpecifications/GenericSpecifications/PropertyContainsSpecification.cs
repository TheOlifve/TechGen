namespace GenericSpecifications;

public class PropertyContainsSpecification<TEntity>: ISpecification<TEntity>
{
    readonly Func<TEntity, string> _getter;
    readonly string _strToSearch;

    public PropertyContainsSpecification(Func<TEntity, string> getter, string strToSearch)
    {
        ArgumentNullException.ThrowIfNull(getter);
        ArgumentNullException.ThrowIfNull(strToSearch);
        _getter = getter;
        _strToSearch = strToSearch;
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        string str = _getter(entity);
        
        return str.Contains(_strToSearch);
    }
}