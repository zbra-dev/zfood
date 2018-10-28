using System.Collections.Generic;

namespace ZFood.Persistence.API.Entity
{
    public class UserEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<VisitEntity> Visits { get; set; }
    }
}
