namespace ToDoAPI.Requests
{
    public class AddToDoRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
