using ToDoAPI.Mocks;
using ToDoAPI.Models;
using ToDoAPI.Requests;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var allToDos = ToDos.GetAllToDos();

app.MapGet("/", () =>
{
    return allToDos;
});

app.MapGet("/feed/", (Guid lastEventId, int seconds) =>
{
    var timeout = TimeSpan.FromSeconds(seconds);
    var startTime = DateTime.UtcNow;

    while (true)
    {
        var toDos = allToDos.Where(e => e.EventId != lastEventId).ToList();
        if (toDos.Any())
        {
            return toDos;
        }

        var elapsedTime = DateTime.UtcNow - startTime;
        if (elapsedTime >= timeout)
        {
            return [];
        }
    }
});

app.MapGet("/{name}", (string name) =>
{
    var toDo = allToDos.FirstOrDefault(x => x.Name.ToLower() == name.ToLower().Trim());

    return toDo is null ? throw new Exception("ToDo not found") : toDo;
});

app.MapPost("/", (AddToDoRequest request) =>
{
    if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Description))
    {
        throw new Exception("Name and description are required");
    }

    allToDos.Add(new ToDo
    {
        EventId = new Guid(),
        Name = request.Name,
        Description = request.Description
    });
});

app.MapPut("/{name}", (AddToDoRequest request, string name) =>
{
    if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Description))
    {
        throw new Exception("Name and description are required");
    }

    var toDo = allToDos.FirstOrDefault(x => x.Name.ToLower() == name.ToLower().Trim()) ?? throw new Exception("ToDo not found");
    allToDos.Remove(toDo);

    toDo.EventId = new Guid();
    toDo.Name = request.Name;
    toDo.Description = request.Description;

    allToDos.Add(toDo);

});

app.MapDelete("/{name}", (string name) =>
{
    var toDo = allToDos.FirstOrDefault(x => x.Name.ToLower() == name.ToLower().Trim()) ?? throw new Exception("ToDo not found");

    allToDos.Remove(toDo);
});

app.Run();