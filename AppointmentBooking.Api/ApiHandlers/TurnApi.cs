using AppointmentBooking.Api.Filters;
using AppointmentBooking.Application.Turns;
using AppointmentBooking.Domain.Dtos;
using MediatR;

namespace AppointmentBooking.Api.ApiHandlers
{
    public static class TurnApi
    {
        public static RouteGroupBuilder MapTurn(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediator, [Validate] TurnGenerateCommand turn) =>
            {
                return Results.Ok(await mediator.Send(turn));
            })
            .Produces(StatusCodes.Status200OK, typeof(TurnDto))
            .Produces(StatusCodes.Status400BadRequest);

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
