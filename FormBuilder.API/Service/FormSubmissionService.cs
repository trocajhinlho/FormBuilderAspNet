using FormBuilder.API.Models.Dto.FormDtos.FormSubmission;
using FormBuilder.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Service;

public interface IFormSubmissionService
{
    Task<FormSubmissionDto?> GetSubmissionWithFormDetails(Guid formId, Guid submissionId);
}

public class FormSubmissionService(ApplicationDbContext db) : IFormSubmissionService
{
    public async Task<FormSubmissionDto?> GetSubmissionWithFormDetails(Guid formId, Guid submissionId)
    {
        var submission = await db.Submission
            .Include(s => s.Answers)
            .ThenInclude(a => a.Question)
            .FirstOrDefaultAsync(s => s.Id == submissionId && s.FormId == formId);

        if (submission == null) return null;

        var formSubmission = new FormSubmissionDto(
            submission.CreatedAt,
            submission.Answers.Select(a => new QuestionAnswerDto(
                a.Id,
                a.Question.Label,
                a.Value,
                a.Question.IsRequired,
                a.Question.IsDeleted)
            ));
        return formSubmission;
    }
}