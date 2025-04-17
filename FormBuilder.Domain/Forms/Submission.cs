using FormBuilder.Domain.Shared;

namespace FormBuilder.Domain.Forms;

public class Submission : IAuditable
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private init; } = DateTime.Now;
    public DateTime? ModifiedAt { get; set; }
    public Guid FormId { get; private init; }
    public Form Form { get; private init; }
    public List<Answer> Answers { get; private set; } = [];

    private Submission() { }

    private Submission(Guid formId, List<Answer> answers)
    {
        FormId = formId;
        Answers = answers;
    }

    public static Submission Create(Guid formId, List<Answer> answers)
    {
        return new Submission(formId, answers);
    }
}
