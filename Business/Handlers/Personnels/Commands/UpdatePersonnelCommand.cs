
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Personnels.ValidationRules;


namespace Business.Handlers.Personnels.Commands
{


	public class UpdatePersonnelCommand : IRequest<IResult>
	{
		public string Name { get; set; }
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

		public class UpdatePersonnelCommandHandler : IRequestHandler<UpdatePersonnelCommand, IResult>
		{
			private readonly IPersonnelRepository _personnelRepository;
			private readonly IMediator _mediator;

			public UpdatePersonnelCommandHandler(IPersonnelRepository personnelRepository, IMediator mediator)
			{
				_personnelRepository = personnelRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdatePersonnelValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdatePersonnelCommand request, CancellationToken cancellationToken)
			{
				
				
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

