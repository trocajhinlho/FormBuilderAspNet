namespace FormBuilder.API.Models.Dto.FormDtos.FormSubmission;

public record QuestionAnswerDto(
    Guid  AnswerId,
    string QuestionLabel,
    string AnswerValue,
    bool IsRequired,
    bool IsDeleted);

