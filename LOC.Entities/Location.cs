using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOC.Entities
{
    public class Location : EntityBase
    {
        public Location() { }

        public string Name { get; set; } = default;
        public string Address { get; set; } = default;
        public TimeOnly OpenTime { get; set; } = default;
        public TimeOnly CloseTime { get; set; } = default;
    }
}
