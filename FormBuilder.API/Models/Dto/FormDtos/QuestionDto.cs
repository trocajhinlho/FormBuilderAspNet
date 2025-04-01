using FormBuilder.Domain.Forms;

namespace FormBuilder.API.Models.Dto.FormDtos;

public class QuestionDto
{
    public Guid Id { get; init; }
    public string Label { get; init; }
    public bool IsDeleted { get; set; }
    public QuestionTypes Type { get; init; }
    public List<QuestionOptionDto>? Options { get; set; }
}

public class QuestionConstraintDto
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
}