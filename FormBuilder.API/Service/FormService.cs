using FormBuilder.API.Models.Dto.FormDtos;
using FormBuilder.Domain.Context;
using FormBuilder.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Service;

public interface IFormService
{
    Task<List<FormDto>> GetForms();
    Task<FormDetailsDto?> GetForm(Guid id);
    Task<Form> SaveForm(Form form);
    Task<Form> UpdateForm(Form id);
}

public class FormService(ApplicationDbContext db) : IFormService
{
    public Task<FormDetailsDto?> GetForm(Guid id)
    {
        return db.Form
            .Include(f => f.Questions)
            .ThenInclude(q => q.Options)
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
                    IsDeleted = q.IsDeleted,
                    Options = q.Options != null ? q.Options.Select(o => new QuestionOptionDto()
                    {
                        Id = o.id,
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

    public Task<Form> SaveForm(Form form)
    {
        throw new NotImplementedException();
    }

    public Task<Form> UpdateForm(Form id)
    {
        throw new NotImplementedException();
    }
}
