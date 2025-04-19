using FormBuilder.API.Models.Dto.SubmissionDtos.Create;
using FormBuilder.Domain.Context;
using FormBuilder.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Service;

public interface ISubmissionService
{
    Task<Submission> Create(CreateSubmissionDto createDto);
}

public class SubmissionService(ApplicationDbContext db, IFormService formService) : ISubmissionService
{
    public async Task<Submission> Create(CreateSubmissionDto createDto)
    {
        var form = await formService.FormAgreggate
            .SingleOrDefaultAsync(f => f.Id == createDto.FormId)
            ?? throw new NullReferenceException($"form with id {createDto.FormId} not found");

        var answers = new List<Answer>();
        var answerQuestionIds = new HashSet<Guid>();

        foreach (var answerDto in createDto.Answers)
        {
            var question = form.Questions.SingleOrDefault(q => q.Id == answerDto.QuestionId);
            if (question == null)
                continue;

            if (question.Type != QuestionTypes.Checkbox && answerQuestionIds.Contains(question.Id))
                continue;

            answers.Add(Answer.Create(answerDto.Value, question));
            answerQuestionIds.Add(question.Id);
        }

        var requiredQuestionsMissing = form.Questions.Any(q => q.IsRequired && !answerQuestionIds.Contains(q.Id));
        if (requiredQuestionsMissing)
            throw new NullReferenceException($"Required questions not met with answer values");

        var submission = Submission.Create(createDto.FormId, answers);
        await db.Submission.AddAsync(submission);
        await db.SaveChangesAsync();
        return submission;
    }
}
