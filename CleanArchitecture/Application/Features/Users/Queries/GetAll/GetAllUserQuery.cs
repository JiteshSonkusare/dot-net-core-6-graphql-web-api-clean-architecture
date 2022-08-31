using MediatR;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Shared.Wrapper;
using Application.Features.Users.Queries.ViewModels;
using AutoMapper;

namespace Application.Features.Users.Queries.GetAll
{
    public class GetAllUserQuery : IRequest<Result<List<UserViewModel>>>
    {
        public GetAllUserQuery() { }
    }

    internal class GetUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserViewModel>>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<UserViewModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.Repository<User>().GetAllAsync();
                var mappeduser = _mapper.Map<List<UserViewModel>>(user);
                return await Result<List<UserViewModel>>.SuccessAsync(mappeduser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get User data! Error: {ex.Message}");
            }
        }
    }
}
