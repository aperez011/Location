using LOC.DataAccess;
using LOC.Entities;
using LOC.Entities.DTOs;
using LOC.Utilities;
using LOC.Utilities.ServiceInterfaces;
using System.Linq.Expressions;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LOC.Services
{
    public class LocationServices : ILocationServices
    {
        private dbContextLocations _ctx;

        public LocationServices(dbContextLocations ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<HashSet<LocationResponseModel>>> Get()
        {
            try
            {
                var data = _ctx.FindAll<Location>();
                var resp = await this.LocationToLocationResponse(data);

                return OperationResult<HashSet<LocationResponseModel>>.Success(resp);

            }
            catch (Exception ex)
            {
                return OperationResult<HashSet<LocationResponseModel>>.Fail((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        public async Task<OperationResult<HashSet<LocationResponseModel>>> GetBy(Expression<Func<Location, bool>> condition)
        {
            try
            {
                var data = _ctx.FindBy(condition);
                var resp = await this.LocationToLocationResponse(data);

                return OperationResult<HashSet<LocationResponseModel>>.Success(resp);

            }
            catch (Exception ex)
            {
                return OperationResult<HashSet<LocationResponseModel>>.Fail((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        public async Task<OperationResult<GeneralResponseModel>> Post(LocationRequest location)
        {
            try
            {

                var _loc = new Location
                {
                    Name = location.Name,
                    Address = location.Address,
                    OpenTime = location.OpenTime,
                    CloseTime = location.CloseTime
                };

                var data = _ctx.Insert(_loc);

                return OperationResult<GeneralResponseModel>.Success(new GeneralResponseModel { GID = _loc.GID });

            }
            catch (Exception ex)
            {
                return OperationResult<GeneralResponseModel>.Fail((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        public async Task<OperationResult> Delete(LocationRemove location)
        {
            try
            {
                var loc = _ctx.FindOne<Location>(location.Id);

                if (loc != null)
                {
                    loc.IsDeleted = true;
                    _ = _ctx.Update(loc);
                }

                return OperationResult.Success();

            }
            catch (Exception ex)
            {
                return OperationResult.Fail((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        #region [Mappers]
        private async Task<HashSet<LocationResponseModel>> LocationToLocationResponse(IEnumerable<Location> locations)
        {
            return await Task.FromResult(locations.Select(c => new LocationResponseModel
            {
                Id = c.GID,
                Name = c.Name,
                Address = c.Address,
                OpenTime = c.OpenTime,
                CloseTime = c.CloseTime
            }).ToHashSet());
        }
        #endregion [Mappers]

    }
}
