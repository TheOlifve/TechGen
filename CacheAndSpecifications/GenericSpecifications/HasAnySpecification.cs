namespace GenericSpecifications;

public class HasAnySpecification<TEntity, TItem>: ISpecification<TEntity>
{
    readonly Func<TEntity, IEnumerable<TItem>> _getter;
    readonly ISpecification<TItem> _specification;

    public HasAnySpecification(Func<TEntity, IEnumerable<TItem>> getter, ISpecification<TItem> specification)
    {
        ArgumentNullException.ThrowIfNull(getter);
        ArgumentNullException.ThrowIfNull(specification);
        _getter = getter;
        _specification = specification;
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        IEnumerable<TItem> items = _getter(entity);
        
        if (items == null)
            return false;
        
        foreach (TItem item in items)
        {
            if (_specification.IsSatisfiedBy(item))
                return true;
        }
        
        return false;
    }
}