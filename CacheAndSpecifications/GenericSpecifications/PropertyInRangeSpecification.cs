namespace GenericSpecifications;

public class PropertyInRangeSpecification<TEntity, TProperty>: ISpecification<TEntity> where TProperty : IComparable<TProperty>
{
    readonly TProperty _minimum;
    readonly TProperty _maximum;
    readonly Func<TEntity, TProperty> _getter;
    
    public PropertyInRangeSpecification(Func<TEntity, TProperty> getProperty, TProperty minimum, TProperty maximum)
    {
        ArgumentNullException.ThrowIfNull(getProperty);
        ArgumentNullException.ThrowIfNull(minimum);
        ArgumentNullException.ThrowIfNull(maximum);
        _minimum = minimum;
        _maximum = maximum;
        _getter = getProperty;
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        TProperty value = _getter(entity);
        
        return value.CompareTo(_minimum) >= 0 && value.CompareTo(_maximum) <= 0;
    }
}