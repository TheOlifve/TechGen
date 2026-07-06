namespace GenericSpecifications;

public static class SpecificationCollectionExtensions
{
    public static IEnumerable<T> Where<T>(this IEnumerable<T> source, ISpecification<T> specification)
    {
        List<T> result = new List<T>();
        
        foreach (var enitity in source)
            if (specification.IsSatisfiedBy(enitity))
                result.Add(enitity);
        
        return result;
    }

    public static bool Any<T>(this IEnumerable<T> source, ISpecification<T> specification)
    {
        foreach (var enitity in source)
            if (specification.IsSatisfiedBy(enitity))
                return true;
        
        return false;
    }
    
    public static bool All<T>(this IEnumerable<T> source, ISpecification<T> specification)
    {
        foreach (var enitity in source)
            if (!specification.IsSatisfiedBy(enitity))
                return false;
        
        return true;
    }

    public static T FirstOrDefault<T>(this IEnumerable<T> source, ISpecification<T> specification)
    {
        foreach (var enitity in source)
            if (specification.IsSatisfiedBy(enitity))
                return enitity;
        
        return default(T);
    }

    public static int Count<T>(this IEnumerable<T> source, ISpecification<T> specification)
    {
        int ret = 0;
        
        foreach (var enitity in source)
            if (specification.IsSatisfiedBy(enitity))
                ret++;

        return ret;
    }
}