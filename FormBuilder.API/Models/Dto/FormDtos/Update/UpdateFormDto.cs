using FormBuilder.API.Models.Dto.FormDtos.Create;

namespace FormBuilder.API.Models.Dto.FormDtos.Update;

public class UpdateFormDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<CreateQuestionDto> QuestionsToCreate { get; init; } = [];
    public IEnumerable<UpdateQuestionDto> QuestionsToUpdate { get; init; } = [];
    public IEnumerable<Guid> QuestionsToDelete { get; set; } = [];

    public bool HasQuestionsToCreate => QuestionsToCreate.Any();
    public bool HasQuestionsToUpdate => QuestionsToUpdate.Any();
    public bool HasQuestionsToDelete => QuestionsToDelete.Any();

}
