using FormBuilder.Domain.Shared;

namespace FormBuilder.Domain.Forms;

public class Question : IAuditable
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public string Label { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public QuestionConstraint? Constraints { get; set; }
    public QuestionTypes Type { get; private init; }
    


    public bool IsDeleted { get; private set; }
    public Guid FormId { get; private init; }
    public Form Form { get; private init; }
    public List<QuestionOption>? Options { get; private set; } 
    public DateTime CreatedAt { get; private init; } = DateTime.Now;
    public DateTime? ModifiedAt { get; set; }
    public List<Answer> Answers { get; private init; } = [];

    private Question() { }

    private Question(string label, QuestionTypes type, bool isRequired)
    {
      Label = label;
      Type = type;
      IsRequired = isRequired;

    }

    public static Question Create (string label, QuestionTypes type, bool isRequired)
    {
        return new Question(label, type, isRequired);
    }

    public void Update(string label, bool isRequired)
    {
        Label = label;
        IsRequired = isRequired;
    }

    public void SetConstraints(QuestionConstraint? constraints)
    {
        if (constraints == null)
            return;

        if (Type != QuestionTypes.Text && Type != QuestionTypes.TextArea)
            throw new TypeAccessException("Cannot set constrints for question that is not of text or text area types");
        Constraints = constraints;    
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }

    public void AddOption(QuestionOption option)
    {
        if(Type != QuestionTypes.Radio && Type != QuestionTypes.Checkbox && Type != QuestionTypes.Select)
            throw new TypeAccessException("Cannot add options to a question that is not of checkbox, radio or select types"); 

        Options ??= [];
        Options.Add(option);
    }

    public void RemoveOption(QuestionOption option)
    {
        if (Options == null)
            throw new Exception("This question does not have any options");
        if (Options.Contains(option) == false)
            throw new Exception("This question does not have this option");
        Options.Remove(option); //TODO: test and evaluate
    }
}
