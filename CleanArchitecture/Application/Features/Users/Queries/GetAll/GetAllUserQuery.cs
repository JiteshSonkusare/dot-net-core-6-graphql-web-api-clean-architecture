using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Queries.GetAll
{
    public class GetAllUserQuery : IRequest<IQueryable<User>>
    {
        public GetAllUserQuery() { }
    }

    internal class GetUserQueryHandler : IRequestHandler<GetAllUserQuery, IQueryable<User>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;

        public GetUserQueryHandler(IUnitOfWork<Guid> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.Repository<User>().Entities();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get User data! Error: {ex.Message}");
            }
        }
    }
}
