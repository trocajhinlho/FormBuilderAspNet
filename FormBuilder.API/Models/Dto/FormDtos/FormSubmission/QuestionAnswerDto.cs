namespace FormBuilder.API.Models.Dto.FormDtos.FormSubmission;

public record QuestionAnswerDto(
    Guid Id,
    string Label,
    string Answer,
    bool IsRequired,
    bool IsDeleted);

