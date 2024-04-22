using ToDoAPI.Models;

namespace ToDoAPI.Mocks
{
    public static class ToDos
    {
        public static List<ToDo> GetAllToDos()
        {
            return new()
            {
                new()
                {
                    EventId = new Guid("c912bfab-10bd-4d8d-801d-052fe42ee910"),
                    Name = "Test",
                    Description = "Test",
                },
                new()
                {
                    EventId = new Guid("c912bfab-10bd-4d8d-801d-052fe42ee910"),
                    Name = "Test1",
                    Description = "Test1",
                },
                new()
                {
                    EventId = new Guid("c912bfab-10bd-4d8d-801d-052fe42ee910"),
                    Name = "Test2",
                    Description = "Test2",
                }
            };
        }
    }
}
