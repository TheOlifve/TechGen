namespace GenericSpecifications;

public static class Specification
{
    
    public static ISpecification<T> Create<T>(Predicate<T> predicate)
    {
        return new PredicateSpecification<T>(predicate);
    }

    public static ISpecification<T> AllOf<T>(params ISpecification<T>[] specifications)
    {
        if (specifications == null || specifications.Length == 0)
            throw new ArgumentException("You must provide at least one specification");
        
        ISpecification<T> newSpecification = specifications[0];
        
        for (int i = 1; i < specifications.Length; i++)
            newSpecification = new AndSpecification<T>(newSpecification, specifications[i]);
        
        return newSpecification;
    }
    
    public static ISpecification<T> AnyOf<T>(params ISpecification<T>[] specifications)
    {
        if (specifications == null || specifications.Length == 0)
            throw new ArgumentException("You must provide at least one specification");
        
        ISpecification<T> newSpecification = specifications[0];
        
        for (int i = 1; i < specifications.Length; i++)
            newSpecification = new OrSpecification<T>(newSpecification, specifications[i]);
        
        return newSpecification;
    }
}