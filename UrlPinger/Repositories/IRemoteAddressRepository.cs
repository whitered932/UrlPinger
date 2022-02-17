using System.Collections.Generic;
using System.Threading.Tasks;
using UrlPinger.Models.Dto;

namespace UrlPinger.Repositories
{
    public interface IRemoteAddressRepository
    {
        Task<IEnumerable<RemoteAddressDto>> GetMany();
        Task<IEnumerable<RemoteAddressDto>> GetManyActive();
        Task<RemoteAddressDto> GetByIdOrFail(int id);
        Task<RemoteAddressDto> Create(RemoteAddressDto urlDto);
        Task<RemoteAddressDto> Update(int id, RemoteAddressDto urlDto);
        Task<bool> DeleteById(int id);
    }
}
