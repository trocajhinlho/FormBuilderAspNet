namespace FormBuilder.API.Models.Dto.FormDtos.FormSubmission;

public record FormSubmissionDto(DateTime SubmittedOn, IEnumerable<QuestionAnswerDto> Answers);

