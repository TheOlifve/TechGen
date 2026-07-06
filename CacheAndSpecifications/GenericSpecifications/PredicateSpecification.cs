namespace GenericSpecifications;

public class PredicateSpecification<T>: ISpecification<T>
{
    Predicate<T> _predicate;
    public PredicateSpecification(Predicate<T> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        _predicate = predicate;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return _predicate(entity);
    }
}