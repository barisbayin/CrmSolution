
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos.PersonnelDtos;

namespace Business.Handlers.Personnels.Queries
{

    public class GetPersonnelsQuery : IRequest<IEnumerable<GetListPersonnelDto>>
    {
        public class GetPersonnelsQueryHandler : IRequestHandler<GetPersonnelsQuery, IEnumerable<GetListPersonnelDto>>
        {
            private readonly IPersonnelRepository _personnelRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public GetPersonnelsQueryHandler(IPersonnelRepository personnelRepository, IMediator mediator, IMapper mapper)
            {
                _personnelRepository = personnelRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IEnumerable<GetListPersonnelDto>> Handle(GetPersonnelsQuery request, CancellationToken cancellationToken)
            {
                var personnelList = await _personnelRepository.GetListAsync(null);
                var mappedGetListPersonnelDto = _mapper.Map<IEnumerable<GetListPersonnelDto>>(personnelList);

                return mappedGetListPersonnelDto;
            }
        }
    }
}