using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOC.Entities.DTOs
{
    public class LocationRequest
    {
        public string Name { get; set; } = default;
        public string Address { get; set; } = default;
        public string OpenTime { get; set; } = default;
        public string CloseTime { get; set; } = default;
    }

    public class LocationRemove
    {
        public Guid Id { get; set; } = default;
    }

    public class LocationResponseModel
    {
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default;
        public string Address { get; set; } = default;
        public TimeOnly OpenTime { get; set; } = default;
        public TimeOnly CloseTime { get; set; } = default;
    }
}
