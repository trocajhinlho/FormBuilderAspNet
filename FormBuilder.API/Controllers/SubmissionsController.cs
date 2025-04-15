using FormBuilder.API.Models.Dto.SubmissionDtos.Create;
using FormBuilder.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.API.Controllers;

[Route("api/submissions")]
[ApiController]
public class SubmissionsController(ISubmissionService submissionService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> SubmitForm([FromBody] CreateSubmissionDto createDto)
    {
        var result = await submissionService.Create(createDto);
        return Ok(result);
    }
}
