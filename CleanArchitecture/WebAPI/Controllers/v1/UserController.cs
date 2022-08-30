using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Commands.UpsertUser;
using Application.Features.Users.Commands.DeleteUser;

namespace WebAPI.Controllers.v1
{
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// Get a User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await Mediator.Send(new GetUserByIdQuery() { Id = id });
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var users = await Mediator.Send(new GetAllUserQuery());
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        /// <summary>
        /// Create/Update a User
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost]
        public async Task<IActionResult> Post(UpsertUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));
        }
    }
}
