using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlPinger.DbContexts;
using UrlPinger.Exceptions;
using UrlPinger.Models;
using UrlPinger.Models.Dto;

namespace UrlPinger.Repositories
{
    public class RemoteAddressRepository : IRemoteAddressRepository
    {
        private readonly ApplicationContext _context;
        private IMapper _mapper;

        public RemoteAddressRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<RemoteAddressDto> Create(RemoteAddressDto remoteAddressDto)
        {
            var remoteAddress = _mapper.Map<RemoteAddressDto, RemoteAddress>(remoteAddressDto);
            _context.RemoteAddresses.Add(remoteAddress);
            await _context.SaveChangesAsync();

            return _mapper.Map<RemoteAddress, RemoteAddressDto>(remoteAddress);
        }

        public async Task<bool> DeleteById(int id)
        {
            var url = await _context.RemoteAddresses.FindAsync(id);
            if (url is null)
                throw new NotFoundException();
            _context.RemoteAddresses.Remove(url);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RemoteAddressDto>> GetManyActive()
        {
            var urlList = await _context.RemoteAddresses.AsNoTracking().Where(u => u.IsActive).ToListAsync();
            return _mapper.Map<List<RemoteAddressDto>>(urlList);
        }

        public async Task<RemoteAddressDto> GetByIdOrFail(int id)
        {
            var url = await _context.RemoteAddresses.FindAsync(id);
            if (url is null)
                throw new NotFoundException();
            return _mapper.Map<RemoteAddressDto>(url);
        }

        public async Task<IEnumerable<RemoteAddressDto>> GetMany()
        {
            var urlList = await _context.RemoteAddresses.ToListAsync();
            return _mapper.Map<List<RemoteAddressDto>>(urlList);
        }

        public async Task<RemoteAddressDto> Update(int id, RemoteAddressDto remoteAddressDto)
        {
            if (id != remoteAddressDto.Id)
                throw new Exception("Id doesn't equals dto Id");

            var isExists = UrlExists(id);
            if (isExists == false)
                throw new NotFoundException();

            var url = _mapper.Map<RemoteAddressDto, RemoteAddress>(remoteAddressDto);
            _context.Update(url);
            await _context.SaveChangesAsync();
            return remoteAddressDto;

        }

        private bool UrlExists(int id)
        {
            return _context.RemoteAddresses.Any(e => e.Id == id);
        }
    }
}
