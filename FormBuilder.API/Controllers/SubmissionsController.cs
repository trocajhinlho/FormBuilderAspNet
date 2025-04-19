using FormBuilder.API.Models.Dto.SubmissionDtos.Create;
using FormBuilder.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.API.Controllers;

[Route("api/submissions")]
[ApiController]
public class SubmissionsController(ISubmissionService submissionService) : ControllerBase
{
    [HttpGet("{id}")]

    public async Task<IActionResult> Get(Guid id)
    {
        var submission = await submissionService.Get(id);
        if(submission == null) 
            return NotFound();
        return Ok(submission);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitForm([FromBody] CreateSubmissionDto createDto)
    {
        var result = await submissionService.Create(createDto);
        return Ok();
    }
}
