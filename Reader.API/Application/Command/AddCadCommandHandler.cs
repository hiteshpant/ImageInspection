using ImageInspection.EventBus;
using MediatR;
using Reader.API.Application.Queries;
using Reader.Domain;
using Reader.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reader.API.Application.Command
{
    /// <summary>
    /// Handler for updating repository
    /// </summary>
    public class AddCadCommandHandler : IRequestHandler<AddCadCommand, CADModel>
    {
        private readonly ICadRepostiory _CadRepo;
        private IEventBus _EventBus;

        public AddCadCommandHandler(ICadRepostiory cadRepo, IEventBus eventBus)
        {
            _CadRepo = cadRepo;
            _EventBus = eventBus;
        }

        public async Task<CADModel> Handle(AddCadCommand request, CancellationToken cancellationToken)
        {
            var cadModel = new CADModel(request.Name, request.CreationDate, request.IsInspected);

            foreach (var geometry in request.Geometry)
            {
                var geo = new Geometry(geometry.Name);
                geo.AddPosition(ConvertToDomainPos(geometry.Positions));
                cadModel.AddGeometry(new List<Geometry>() { geo });
            }
            _CadRepo.Add(cadModel);

            await _CadRepo.UnitOfWork.SaveChangesAsync();

            var partAddedEvent = new PartAddedForInspectionIntegrationEvent(cadModel.Id, cadModel.CreationDate, cadModel.IsInspected,
                                                                         cadModel.Description, cadModel.Geometry);
            //this Publishes the event to event bus 
            //Inspection Service can Listen when new part comes
            // and start Inspecting that to piblish it consistently
            //More resilient approach is to add the event to a event logs before comitting 
            //data to the repository, so that in case event bus is down or system just crashed after 
            // comitting transaction, so more Resilent approach would be to 
            //have a worker service which can poll on event logs and in case of failed commit
            //again pushes message  to a event bus
            //https://www.kamilgrzybek.com/design/the-outbox-pattern/
            _EventBus.Publish(partAddedEvent);

            return cadModel;
        }

        private IEnumerable<Position> ConvertToDomainPos(IReadOnlyCollection<PositionDTO> positions)
        {
            foreach (var pos in positions)
            {
                yield return new Position(pos.X, pos.Y, pos.Z);
            }
        }

    }
}
