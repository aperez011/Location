using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOC.Entities
{
    public class EntityBase
    {
        public EntityBase()
        {
            this.GID = Guid.NewGuid();
            this.IsDeleted = false;
        }

        public int Id { get; set; } = default;

        public Guid GID { get; set; } = default;

        public bool IsDeleted { get; set; } = default;
    }
}
