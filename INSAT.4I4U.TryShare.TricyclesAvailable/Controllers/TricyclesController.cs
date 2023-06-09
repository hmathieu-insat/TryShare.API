﻿using Microsoft.AspNetCore.Mvc;
using INSAT._4I4U.TryShare.Core.Models;
using INSAT._4I4U.TryShare.Core.Interfaces.Services;
using INSAT._4I4U.TryShare.Core.Exceptions;
using INSAT._4I4U.TryShare.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using INSAT._4I4U.TryShare.Core.Models.Dtos;

namespace INSAT._4I4U.TryShare.TricyclesAvailable.Controllers
{
    /// <summary>
    /// Controller exposing the Tricycles API.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class TricyclesController : ControllerBase
    {
        private readonly ITricyleService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TricyclesController"/> class.
        /// </summary>
        /// <param name="service">The _repository operating on Tricycles.</param>
        public TricyclesController(ITricyleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all the tricycles. 
        /// </summary>
        /// <returns>A list of all tricycles available</returns>
        [HttpGet(Name = nameof(GetTricycles))]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Tricycle>>> GetTricycles()
        {
            return await _service.GetAvailableTricyclesAsync();
        }

        /// <summary>
        /// Gets the tricycle.
        /// </summary>
        /// <param name="id">The ID of the tricycle.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetTricycle))]
        [Produces("application/json")]
        public async Task<ActionResult<Tricycle>> GetTricycle(int id)
        {
            var tricycle = await _service.GetByIdAsync(id);

            if (tricycle is null)
                return NotFound();

            return Ok(tricycle);
        }

        /// <summary>
        /// Requests the start of the booking for a tricycle.
        /// </summary>
        /// <param name="id">The identifier of the tricycle.</param>
        /// <param name="tricycle">
        /// The tricycle requesting to be booked. 
        /// Its rating is used to compute the new rating.
        /// </param>
        /// <returns></returns>
        [Authorize]
        [RequiredScope("access_as_user")]
        [HttpPost("{id}/requestBooking", Name = nameof(RequestTricycleBooking))]
        public async Task<ActionResult> RequestTricycleBooking(int id, TricycleApplicationDto tricycle)
        {
            if (id != tricycle.Id)
                return BadRequest("The tricycle ID in the URL and in the body do not match");

            var tricycleDb = await _service.GetByIdAsync(id);
            if (tricycleDb is null)
                return NotFound(id);
            
            try
            {
                await _service.RequestTricycleBookingAsync(tricycleDb, tricycle.Rating);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound(id);
            }
            catch (TricycleNotAvailableException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Requests the start of the booking for a tricycle.
        /// </summary>
        /// <param name="id">The identifier of the tricycle.</param>
        /// <param name="tricycle"> 
        /// The tricycle requesting to be booked.
        /// Its rating is used to compute the new rating.
        /// </param>
        /// <returns></returns>
        [Authorize]
        [RequiredScope("access_as_user")]
        [HttpPost("{id}/requestEndOfBooking", Name = nameof(RequestTricycleEndOfBooking))]
        public async Task<ActionResult> RequestTricycleEndOfBooking(int id, TricycleApplicationDto tricycle)
        {
            if (id != tricycle.Id)
                return BadRequest("The tricycle ID in the URL and in the body do not match");

            var tricycleDb = await _service.GetByIdAsync(id);
            if (tricycleDb is null)
                return NotFound(id);

            try
            {
                await _service.RequestEndOfBookingAsync(tricycleDb, tricycle.Rating);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Signals the tricycle entering a danger zone.
        /// </summary>
        /// <param name="id">The ID of the tricycle.</param>
        /// <returns></returns>
        //[Authorize]
        //[RequiredScope("access_as_user")]
        [HttpPost("{id}/signalDanger", Name = nameof(SignalEnteringDangerZone))]
        public async Task<ActionResult> SignalEnteringDangerZone(int id)
        {
            var tricycle = await _service.GetByIdAsync(id);
            if (tricycle is null)
                return NotFound(id);

            try
            {
                await _service.SignalEnteringDangerZoneAsync(tricycle);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Signals the tricycle is leaving a danger zone.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [RequiredScope("access_as_user")]
        [HttpPost("{id}/signalDangerEnd", Name = nameof(SignalLeavingDangerZone))]
        public async Task<ActionResult> SignalLeavingDangerZone(int id)
        {
            var tricycle = await _service.GetByIdAsync(id);
            if (tricycle is null)
                return NotFound(id);

            try
            {
                await _service.SignallLeavingDangerZoneAsync(tricycle);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound(id);
            }
        }
    }
}