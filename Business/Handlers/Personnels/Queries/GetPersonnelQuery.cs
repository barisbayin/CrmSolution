
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Personnels.Queries
{
    public class GetPersonnelQuery : IRequest<IDataResult<Personnel>>
    {
        public string Name { get; set; }

        public class GetPersonnelQueryHandler : IRequestHandler<GetPersonnelQuery, IDataResult<Personnel>>
        {
            private readonly IPersonnelRepository _personnelRepository;
            private readonly IMediator _mediator;

            public GetPersonnelQueryHandler(IPersonnelRepository personnelRepository, IMediator mediator)
            {
                _personnelRepository = personnelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Personnel>> Handle(GetPersonnelQuery request, CancellationToken cancellationToken)
            {
                var personnel = await _personnelRepository.GetAsync(p => p.Name == request.Name);
                return new SuccessDataResult<Personnel>(personnel);
            }
        }
    }
}
