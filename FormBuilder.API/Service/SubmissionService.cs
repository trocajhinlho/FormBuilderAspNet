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
        foreach (var question in form.Questions)
        {
            if (question.IsDeleted) continue;

            var answerDto = createDto.Answers.SingleOrDefault(a => a.QuestionId == question.Id);
            if (answerDto == null)
            { 
                if (question.IsRequired)
                    throw new NullReferenceException($"Question \"{question.Label}\" is required");
                continue;
            }

            answers.Add(Answer.Create(answerDto.Value, question));
        }

        var submission = Submission.Create(createDto.FormId, answers);
        await db.Submission.AddAsync(submission);
        await db.SaveChangesAsync();
        return submission;
    }
}
