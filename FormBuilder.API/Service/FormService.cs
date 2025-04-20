using FormBuilder.API.Commands.Forms;
using FormBuilder.API.Models.Dto.FormDtos;
using FormBuilder.API.Models.Dto.FormDtos.Create;
using FormBuilder.API.Models.Dto.FormDtos.Update;
using FormBuilder.Domain.Context;
using FormBuilder.Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;

namespace FormBuilder.API.Service;

public interface IFormService
{
    IIncludableQueryable<Form, List<QuestionOption>?> FormAgreggate { get; }
    Task<List<FormDto>> GetForms();
    Task<FormDetailsDto?> GetForm(Guid id);
    Task<Form> SaveForm(CreateFormDto createDto);
    Task<Form> UpdateForm (Guid formId, UpdateFormDto updateDto);
}

public class FormService(
    ApplicationDbContext db,
    IUpdateFormCommandHandler updateFormCommandHandler
    ) : IFormService
{

 public IIncludableQueryable<Form, List<QuestionOption>?> FormAgreggate    =>
        db.Form
            .Include(f => f.Questions)
            .ThenInclude(q => q.Options);

    public Task<FormDetailsDto?> GetForm(Guid id)
    {
        return FormAgreggate
            .Select(f => new FormDetailsDto()
            {
                Id = f.Id,
                PublicId = f.PublicId,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt,
                Questions = f.Questions.Select(q => new QuestionDto()
                {
                    Id = q.Id,
                    Label = q.Label,
                    Type = q.Type,
                    Order = q.Order,
                    IsDeleted = q.IsDeleted,
                    IsRequired  = q.IsRequired,
                    QuestionConstraint = q.Constraints == null ? null : new QuestionConstraintDto()
                    {
                        MinLength = q.Constraints.MinLength,
                        MaxLength = q.Constraints.MaxLength,

                    },                    
                    Options = q.Options != null ? q.Options.Select(o => new QuestionOptionDto()
                    {
                        Id = o.Id,
                        Value = o.Value,
                        Label = o.Label,
                    }).ToList() : null
                }).ToList()
            }).SingleOrDefaultAsync(f => f.Id == id);
    }

    public Task<List<FormDto>> GetForms()
    {
        return db.Form
            .Select(f => new FormDto
            {
                PublicId = f.PublicId,
                Id = f.Id,
                Title = f.Title,
                Description = f.Description,
                CreatedAt = f.CreatedAt,
            }).ToListAsync();
    }

    public async Task<Form> SaveForm(CreateFormDto createDto)
    {
        var questions = createDto.Questions.OrderBy(q => q.Order).Select(q =>
        {
            var question = Question.Create(q.Label, q.Type, q.IsRequired);

            if (q.Constraint != null)
            {
                var constraint = QuestionConstraint.Create(

                    q.Constraint.MinLength,
                    q.Constraint.MaxLength);
                question.SetConstraints(constraint);
            }

            if (!q.HasOptions)
                return question;
            foreach (var optionDto in q.Options)
            {
                question.AddOption(QuestionOption.Create(optionDto.Value, optionDto.Label));
            }
            return question;
        }).ToList();

        var form = Form.Create(createDto.Title, createDto.Description, questions);
        await db.Form.AddAsync(form);
        await db.SaveChangesAsync();
        return form;
    }
    public async Task<Form> UpdateForm(Guid formId, UpdateFormDto updateDto)
    {
        var form = await FormAgreggate
            .FirstOrDefaultAsync(f => f.Id == formId);
        if (form == null)
        {
            throw new Exception($"Form with id {formId} not found");
        }

        updateFormCommandHandler.Handle(form, updateDto);

        form.Questions.ForEach(q =>
        {
            if (q.FormId != Guid.Empty) return;
            db.Entry(q).State = EntityState.Added;
            q.Options?.ForEach(o => db.Entry(o).State = EntityState.Added);

        });


        await db.SaveChangesAsync();
        return form;
    }
}
