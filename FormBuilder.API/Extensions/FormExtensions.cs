using FormBuilder.API.Models.Dto.FormDtos;
using FormBuilder.Domain.Forms;

namespace FormBuilder.API.Extensions;

public static class FormExtensions
{
    public static FormDetailsDto ToDetailsDto(this Form form)
    {
        return new FormDetailsDto
        {
            Id = form.Id,
            PublicId = form.PublicId,
            Title = form.Title,
            Description = form.Description,
            CreatedAt = form.CreatedAt,
            Questions = form.Questions.Select(q => new QuestionDto()
            {
                Id = q.Id,
                Label = q.Label,
                Type = q.Type,
                IsRequired = q.IsRequired,
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
        };
    }
}
