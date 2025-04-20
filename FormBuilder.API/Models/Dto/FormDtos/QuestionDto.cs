using FormBuilder.Domain.Forms;

namespace FormBuilder.API.Models.Dto.FormDtos;

public class QuestionDto
{
    public Guid Id { get; init; }
    public string Label { get; init; }
    public int Order { get; init; }
    public bool IsRequired { get; init; }
    public bool IsDeleted { get; init; }
    public QuestionTypes Type { get; init; }
    public QuestionConstraintDto? QuestionConstraint { get; init; }
    public List<QuestionOptionDto>? Options { get; set; }
}

public class QuestionConstraintDto
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }
}