namespace FormBuilder.Domain.Forms;

public class QuestionOption
{
    public Guid id { get; private init; } = Guid.NewGuid();
    public string Value { get; set; }
    public string Label { get; set; }
    public Guid QuestionId { get; private init; }

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
}
