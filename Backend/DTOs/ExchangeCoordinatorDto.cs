namespace Backend.DTOs
{
    /// <summary>A data transfer object for an exchange coordinator.</summary>
    public class ExchangeCoordinatorDto : DomainUserDto
    {
        public DepartmentInfoDto Department { get; set; }

        public ICollection<ToDoItemDto> ToDoList { get; set; }
    }
}
