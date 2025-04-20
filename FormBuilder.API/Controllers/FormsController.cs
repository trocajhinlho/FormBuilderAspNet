using FormBuilder.API.Extensions;
using FormBuilder.API.Models.Dto.FormDtos.Create;
using FormBuilder.API.Models.Dto.FormDtos.Update;
using FormBuilder.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormsController(IFormService formService, ISubmissionService submissionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetForms()
    {
        var forms = await formService.GetForms();
        return Ok(forms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetForm(Guid id)
    {
        var form = await formService.GetForm(id);
        return Ok(form);
    }

    [HttpPost]
    public async Task<IActionResult> CreateForm([FromBody] CreateFormDto dto)
    {
        var form = await formService.SaveForm(dto);
        return Ok(form.ToDetailsDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateForm(Guid id, [FromBody] UpdateFormDto dto)
    {
        var form = await formService.UpdateForm(id, dto);
        return Ok(form.ToDetailsDto());
    }

    [HttpGet("{id}/submissions")]
    public async Task<IActionResult> ListSubmissions(Guid id)
    {
       var submissions = await submissionService.ListByFormId(id);
        return Ok(submissions);
    }

    [HttpGet("{id}/submissions/{submissionId}")]
    public async Task<IActionResult> GetSubmission(Guid id, Guid submissionId)
    {
        var submissionDetails = await submissionService.GetWithFormDetails(id, submissionId);
        return Ok(submissionDetails);
    }

}