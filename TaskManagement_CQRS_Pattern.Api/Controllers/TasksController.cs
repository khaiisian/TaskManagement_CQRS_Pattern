using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement_CQRS_Pattern.Api.Features.Tasks.Commands.CreateTasks;
using TaskManagement_CQRS_Pattern.Api.Features.Tasks.Queries.GetAllTasks;

namespace TaskManagement_CQRS_Pattern.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _mediator.Send(new GetAllTasksQuery());
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        var message = result > 0 ? "Create successful" : "Create failed";
        return Ok(message);
    }
}
