namespace PharmacySolution.Contracts.Validator
{
    public interface IValidator<in T> where T : class 
    {
        bool IsValid(T entity);
    }
}
