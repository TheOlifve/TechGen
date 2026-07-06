namespace GenericSpecifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T obj);
}