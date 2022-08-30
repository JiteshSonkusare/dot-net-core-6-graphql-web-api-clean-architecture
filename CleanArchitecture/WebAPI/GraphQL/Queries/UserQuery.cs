using MediatR;
using Application.Features.Users.Queries.GetAll;
using Domain.Entities;

namespace WebAPI.GraphQL.Queries
{
    public class UserQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<User>> GetUsers([Service] IMediator _mediator)
        {
            return await _mediator.Send(new GetAllUserQuery());
        }
    }
}