using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlPinger.Exceptions;
using UrlPinger.Models;
using UrlPinger.Models.Dto;
using UrlPinger.Repositories;

namespace UrlPinger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteAddressesController : ControllerBase
    {
        private readonly IRemoteAddressRepository _remoteAddressRepository;
        private readonly ILogger<RemoteAddressesController> _logger;

        public RemoteAddressesController(IRemoteAddressRepository remoteAddressRepository, ILogger<RemoteAddressesController> logger)
        {
            _remoteAddressRepository = remoteAddressRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemoteAddressDto>>> GetUrls()
        {
            var urlDtos = await _remoteAddressRepository.GetMany();
            return Ok(urlDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RemoteAddressDto>> GetUrl(int id)
        {
            try
            {
                var url = await _remoteAddressRepository.GetByIdOrFail(id);
                return url;
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrl(int id, RemoteAddressDto remoteAddressDto)
        {
            try
            {
                await _remoteAddressRepository.Update(id, remoteAddressDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<RemoteAddress>> PostUrl(RemoteAddressDto remoteAddressDto)
        {
            try
            {
                var createdRemoteAddress = await _remoteAddressRepository.Create(remoteAddressDto);
                return CreatedAtAction("GetUrl", new { id = createdRemoteAddress.Id }, createdRemoteAddress);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.InnerException.ToString());
                return StatusCode(500);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            try
            {
                await _remoteAddressRepository.DeleteById(id);

            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
