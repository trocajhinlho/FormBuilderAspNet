using FormBuilder.API.Models.Dto.FormDtos.Create;
using FormBuilder.API.Models.Dto.FormDtos.Update;
using FormBuilder.Domain.Forms;
using Microsoft.AspNetCore.Http.HttpResults;

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

        if (updateDto.HasQuestionsToCreate)
        {
            HandleCreateQuestions(form, updateDto.QuestionsToCreate!);
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

    public void HandleCreateQuestions(Form form, IEnumerable<CreateQuestionDto> createQuestionDtos)
    {
        foreach(var q in createQuestionDtos)
        {
            var question = Question.Create(q.Label, q.Type, q.IsRequired);

            if (q.Constraint != null)
            {
                var constraint = QuestionConstraint.Create(
                    q.Constraint.MinLength,
                    q.Constraint.MaxLength);
                question.SetConstraints(constraint);
            }

            if (q.HasOptions)
            { 
                foreach (var optionDto in q.Options)
                {
                    question.AddOption(QuestionOption.Create(optionDto.Value, optionDto.Label));
                }
            }
            form.Questions.Add(question);
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
