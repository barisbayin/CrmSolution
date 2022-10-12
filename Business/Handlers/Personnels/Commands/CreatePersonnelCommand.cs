
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Business.Handlers.Personnels.ValidationRules;
using Entities.Dtos.PersonnelDtos;

namespace Business.Handlers.Personnels.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreatePersonnelCommand : IRequest<CreatedPersonnelDto>
	{

		public string LastName { get; set; }
		public System.DateTime BirthDay { get; set; }
		public string IdentityNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string FullAddress { get; set; }
		public string AddressLine { get; set; }
		public int? CountryId { get; set; }
		public int? CityId { get; set; }
		public int? CountyId { get; set; }
		public int? NeighbourhoodId { get; set; }
		public string ZipCode { get; set; }
		public string ImagePath { get; set; }
		public bool IsUser { get; set; }


		public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommand, CreatedPersonnelDto>
		{
			private readonly IPersonnelRepository _personnelRepository;
			private readonly IMediator _mediator;
            private readonly IMapper _mapper;

			public CreatePersonnelCommandHandler(IPersonnelRepository personnelRepository, IMediator mediator, IMapper mapper)
			{
				_personnelRepository = personnelRepository;
				_mediator = mediator;
                _mapper = mapper;
            }

			[ValidationAspect(typeof(CreatePersonnelValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<CreatedPersonnelDto> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
            {

                Personnel mappedPersonnel = _mapper.Map<Personnel>(request);
                Personnel createdPersonnel = await _personnelRepository.AddAsync(mappedPersonnel);
                CreatedPersonnelDto createdPersonnelDto = _mapper.Map<CreatedPersonnelDto>(createdPersonnel);
		
				return createdPersonnelDto;
			}
		}
	}
}