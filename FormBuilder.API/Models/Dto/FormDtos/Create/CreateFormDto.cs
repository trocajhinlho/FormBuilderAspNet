namespace FormBuilder.API.Models.Dto.FormDtos.Create;

public class CreateFormDto
{
        public string Title { get; init; }
        public string? Description { get; init; }
        public IEnumerable<CreateQuestionDto> Questions { get; init; }

        public bool HasQuestions => Questions.Any();
       
}
