namespace GenericSpecifications;

public class AndSpecification<T>: ISpecification<T>
{
    ISpecification<T> _left;
    ISpecification<T> _right;
    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T entity)
    {
        if (_left.IsSatisfiedBy(entity) && _right.IsSatisfiedBy(entity))
            return  true;
        return false;
    }
}