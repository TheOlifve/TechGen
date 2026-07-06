namespace GenericSpecifications;

public class NotSpecification<T>: ISpecification<T>
{
    ISpecification<T> _specification;
    public NotSpecification(ISpecification<T> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);
        _specification = specification;
    }

    public bool IsSatisfiedBy(T entity)
    {
        if (!_specification.IsSatisfiedBy(entity))
            return  true;
        return false;
    }
}