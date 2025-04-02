﻿namespace FormBuilder.API.Models.Dto.FormDtos.Update;

public class UpdateFormDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<UpdateQuestionDto>? QuestionsToUpdate { get; init; }
    public IEnumerable<Guid>? QuestionsToDelete { get; set; }
}
