namespace FormBuilder.Domain.Forms;

public class QuestionOption
{
    public Guid id { get; private init; } = Guid.NewGuid();
    public string Value { get; private set; }
    public string Label { get; private set; }
    public Guid QuestionId { get; private init; }
    public Question Question { get; private init; }

    private QuestionOption() { }

    private QuestionOption(string value, string label)
    {
        Value = value;
        Label = label;
    }

    public static QuestionOption Create(String value, string label)
    {
        return new QuestionOption(value, label);
    }

    public void Update(string value, string label)
    {
        Value = value;
        Label = label;
    }
}
