using FormBuilder.Domain.Shared;

namespace FormBuilder.Domain.Forms;

public class Question : IAuditable
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Label { get; set; }
    public QuestionConstraint Constraints { get; set; }
    public QuestionTypes Type { get; private init; }


    public bool IsDeleted { get; private set; }
    public Guid FormId { get; private init; }
    public Form Form { get; private init; }
    public List<QuestionOption> Options { get; private set; } = [];
    public DateTime CreatedAt { get; private init; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; }

    private Question() { }

    private Question(string label, QuestionConstraint constraints, QuestionTypes type)
    {
      Label = label;
      Constraints = constraints;
      Type = type;

    }

    public static Question Create (string label, QuestionConstraint constraints, QuestionTypes type)
    {
        return new Question(label, constraints, type);
    }
}
