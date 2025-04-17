using FormBuilder.Domain.Shared;
using Microsoft.Extensions.Options;

namespace FormBuilder.Domain.Forms;

public class Answer : IAuditable
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Value { get; set; }
    public DateTime CreatedAt { get; private init; } = DateTime.Now;
    public DateTime? ModifiedAt { get; set; }
    public Guid SubmissionId { get; private init; }
    public Submission Submission { get; private init; }
    public Guid QuestionId { get; private init; }
    public Question Question { get; private init; }

    private Answer() { }

    private Answer(string value, Guid questionID)
    {
        Value = value;
        QuestionId = questionID;
    }

    public static Answer Create(String value, Question question)
    {
        ValidateQuestionTypeAnswerValue(value, question);
        return new Answer( value, question.Id);
    }

    private static void ValidateQuestionTypeAnswerValue(string value, Question question)
    {
        switch (question.Type)
        {
            case QuestionTypes.Integer:
                var isInt = int.TryParse(value, out _);
                if (isInt)
                    throw new Exception($"\"{question.Label}\" Answer value must be integer");
                break;

            case QuestionTypes.Float:
                var isFloat = float.TryParse(value, out _);
                if (isFloat)
                    throw new Exception($"\"{question.Label}\" Answer value must be float");
                break;

            case QuestionTypes.Radio:
            case QuestionTypes.Checkbox:
            case QuestionTypes.Select:
                if (question.GetOptionsValues().All(o => o != value))
                    throw new Exception($"\"{question.Label}\"does not accept a value of {value}");
                break;

            case QuestionTypes.Date:
                var isDate = DateTime.TryParse(value, out _); 
                if (!isDate)
                    throw new Exception($"\"{question.Label}\" Answer value must be a date");
                break;
        }
    }
}

