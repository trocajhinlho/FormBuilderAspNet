namespace FormBuilder.API.Models.Dto.FormDtos;

public class FormDetailsDto : FormDto
{
    public List<QuestionDto> Questions { get; set; } = [];
}
