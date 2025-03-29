namespace FormBuilder.API.Models.Dto.FormDtos;

public class FormDto
{
    public Guid Id { get; init; }
    public string PublicId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime CreatedAt { get; init; }
}


