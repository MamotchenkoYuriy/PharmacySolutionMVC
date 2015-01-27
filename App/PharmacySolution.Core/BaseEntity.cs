using PharmacySolution.Contracts;

namespace PharmacySolution.Core
{
    public class BaseEntity : IDbEntity
    {
        public int Id { get; set; }

        public BaseEntity()
        {
            
        }
    }
}
