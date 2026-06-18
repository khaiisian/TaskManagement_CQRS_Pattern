using MediatR;
using TaskManagement_CQRS_Pattern.Db.AppDbContextModels;

namespace TaskManagement_CQRS_Pattern.Api.Features.Tasks.Commands.CreateTasks;

//public record CreatTaskCommand(string Title, string Description) : IRequest<int>;

public class CreateTaskCommand: IRequest<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateTaskHandler: IRequestHandler<CreateTaskCommand, int>
{
    private readonly AppDbContext _appDbContext;

    public CreateTaskHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle (CreateTaskCommand request, CancellationToken ct)
    {
        var taskItem = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Iscompleted = false,
            CreatedAt = DateTime.Now,
        };

        await _appDbContext.TaskItems.AddAsync(taskItem);
        var result = await _appDbContext.SaveChangesAsync(ct);
        return result;
    }
}
