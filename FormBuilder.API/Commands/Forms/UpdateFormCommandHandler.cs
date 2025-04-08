using FormBuilder.API.Models.Dto.FormDtos.Update;
using FormBuilder.Domain.Forms;

namespace FormBuilder.API.Commands.Forms;

public interface IUpdateFormCommandHandler
{
    bool Handle(Form form, UpdateFormDto updateDto);
}

public class UpdateFormCommandHandler : IUpdateFormCommandHandler
{
    public bool Handle(Form form, UpdateFormDto updateDto)
    {
        form.Update(updateDto.Title, updateDto.Description);

        var deletedQuestions = new HashSet<Guid>();

        if (updateDto.HasQuestionsToDelete)
        {
            HandleDeleteQuestions(form, updateDto, deletedQuestions);
        }

        if (updateDto.HasQuestionsToUpdate)
        {
            HandleUpdateQuestions(form, updateDto, deletedQuestions);
        }

        return true;
    }

    private void HandleDeleteQuestions(Form form, UpdateFormDto updateDto, HashSet<Guid> deletedQuestions)
    {
        foreach (var questionId in updateDto.QuestionsToDelete!)
        {
            var question = form.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question != null) continue;
            {
                question.MarkAsDeleted();
                deletedQuestions.Add(questionId);

            }
        }
    }


    private void HandleUpdateQuestions(Form form, UpdateFormDto updateDto, HashSet<Guid> deletedQuestions)
    {
        foreach (var questionToUpdate in updateDto.QuestionsToUpdate!)
        {
            if (deletedQuestions.Contains(questionToUpdate.Id))
                continue;
            var question = form.Questions.FirstOrDefault(q => q.Id == questionToUpdate.Id);
            if (question == null)
                continue;
            
            UpdateQuestion(question, questionToUpdate);

        }
    }

    private void UpdateQuestion(Question question, UpdateQuestionDto questionToUpdate)
    {
        question?.Update(questionToUpdate.Label, questionToUpdate.IsRequired);
        if (!questionToUpdate.HasOptions)
            return;

        var optionsDict = question!.Options!.GroupBy(e => e.Id)
        .ToDictionary
        (
            g => g.Key,
            e => e.First()
        );

        foreach (var currentOption in questionToUpdate.Options!)
        {
            var optionId = currentOption.Id;
            if (optionId != Guid.Empty && optionsDict.TryGetValue(optionId, out var questionOption))
            {
                questionOption.Update(currentOption.Value, currentOption.Label);
                continue;
            }

                var newOption = QuestionOption.Create(currentOption.Value, currentOption.Label);
            question.AddOption(newOption);
        }
    }
}
