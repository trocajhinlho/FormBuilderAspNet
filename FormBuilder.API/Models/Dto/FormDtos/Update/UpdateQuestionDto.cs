using Microsoft.Identity.Client;

namespace FormBuilder.API.Models.Dto.FormDtos.Update;

public class UpdateQuestionDto
{
    public Guid Id { get; init; }
    public string Label { get; init; }
    public bool IsRequired { get; init; }
    public QuestionConstraintDto? Constraints { get; init; }
    public IEnumerable<QuestionOptionDto>? Options { get; init; }

    public bool HasOptions => Options != null && Options.Any();
}
