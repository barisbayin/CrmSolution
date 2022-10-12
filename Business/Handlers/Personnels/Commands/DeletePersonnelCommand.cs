
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Personnels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePersonnelCommand : IRequest<IResult>
    {
        public string Name { get; set; }

        public class DeletePersonnelCommandHandler : IRequestHandler<DeletePersonnelCommand, IResult>
        {
            private readonly IPersonnelRepository _personnelRepository;
            private readonly IMediator _mediator;

            public DeletePersonnelCommandHandler(IPersonnelRepository personnelRepository, IMediator mediator)
            {
                _personnelRepository = personnelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePersonnelCommand request, CancellationToken cancellationToken)
            {
                var personnelToDelete = _personnelRepository.Get(p => p.Name == request.Name);

                _personnelRepository.Delete(personnelToDelete);
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

