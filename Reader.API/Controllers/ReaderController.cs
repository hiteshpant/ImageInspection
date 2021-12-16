using ImageInspection.EventBus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reader.API.Application;
using Reader.API.Application.Command;
using Reader.API.Application.Queries;
using System;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IReader _ReaderQuery;
        private readonly IEventBus _EventBus;

        public ReaderController(IReader reader, IMediator mediator, IEventBus eventBus)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _ReaderQuery = reader;
            _EventBus = eventBus;
        }

        [Route("{Id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(CADViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCadAsync(int Id)
        {
            try
            {
                var order = await _ReaderQuery.GetSelectedCad(Id);

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }


        [HttpGet("parts")]
        [ProducesResponseType(typeof(InspectionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPartsAsync()
        {
            try
            {
                var order = await _ReaderQuery.GetAllCad();

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CADViewModel>> Post([FromBody] AddCadCommand model)
        {
            var cadModel = await _mediator.Send(model);

           return new CADViewModel()
            {
                CreationDate = cadModel.CreationDate,
                Description = cadModel.Description,
                IsInspected = cadModel.IsInspected,
                Id = cadModel.Id
            };
        }

    }
}
