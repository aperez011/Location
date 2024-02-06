using LOC.Entities;
using LOC.Entities.DTOs;
using System.Linq.Expressions;

namespace LOC.Utilities.ServiceInterfaces
{
    public interface ILocationServices
    {
        Task<OperationResult<HashSet<LocationResponseModel>>> Get();
        Task<OperationResult<HashSet<LocationResponseModel>>> GetBy(Expression<Func<Location, bool>> condition);
        Task<OperationResult<GeneralResponseModel>> Post(LocationRequest location);
        Task<OperationResult> Delete(LocationRemove location);
    }
}
