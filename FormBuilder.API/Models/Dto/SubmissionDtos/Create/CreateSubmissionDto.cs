namespace FormBuilder.API.Models.Dto.SubmissionDtos.Create;

public class CreateSubmissionDto
{
    public Guid FormId { get; set; }
    public List<CreateAnswerDto> Answers { get; init; } = [];

    public bool HasAnswers => Answers.Count > 0;
}
