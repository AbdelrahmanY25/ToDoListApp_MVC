namespace ToDoList.Models
{
    public class Assignment
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DeadLine { get; set; }
        public string PdfFile { get; set; }
    }
}
