using FormBuilder.API.Models.Dto.SubmissionDtos.Create;
using FormBuilder.Domain.Forms;

namespace FormBuilder.API.Service;

public interface ISubmissionService
{
    Task<Submission> Create(CreateSubmissionDto createDto);
}

public class SubmissionService : ISubmissionService
{
    public Task<Submission> Create(CreateSubmissionDto createDto)
    {
        throw new NotImplementedException();
    }
}
