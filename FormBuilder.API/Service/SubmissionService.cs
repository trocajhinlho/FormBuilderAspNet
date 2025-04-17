using FormBuilder.API.Models.Dto.SubmissionDtos.Create;
using FormBuilder.Domain.Context;
using FormBuilder.Domain.Forms;

namespace FormBuilder.API.Service;

public interface ISubmissionService
{
    Task<Submission> Create(CreateSubmissionDto createDto);
}

public class SubmissionService(ApplicationDbContext db) : ISubmissionService
{
    public async Task<Submission> Create(CreateSubmissionDto createDto)
    {
        var answers = createDto.Answers.Select(a => Answer.Create(a.Value, a.QuestionId)).ToList();
        var submission = Submission.Create(createDto.FormId, answers);
        await db.Submission.AddAsync(submission);
        await db.SaveChangesAsync();
        return submission;
    }
}
