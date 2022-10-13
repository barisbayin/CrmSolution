using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Fakes.Handlers.OperationClaims
{
    public class CreateOperationClaimInternalCommand : IRequest<IResult>
    {
        public string ClaimName { get; set; }

        public class
            CreateOperationClaimInternalCommandHandler : IRequestHandler<CreateOperationClaimInternalCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public CreateOperationClaimInternalCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<IResult> Handle(CreateOperationClaimInternalCommand request, CancellationToken cancellationToken)
            {
                if (IsClaimExists(request.ClaimName))
                {
                    return new ErrorResult(Messages.OperationClaimExists);
                }

                var operationClaim = new OperationClaim
                {
                    Name = request.ClaimName
                };
                _operationClaimRepository.Add(operationClaim);

                return new SuccessResult(Messages.Added);
            }

            private bool IsClaimExists(string claimName)
            {
                var result = _operationClaimRepository.GetAsync(x => x.Name == claimName).Result;
                if (result != null)
                {
                    return true;
                }

                return false;
            }
        }
    }
}