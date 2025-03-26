namespace FormBuilder.Domain;

public class QuestionConstraint
{
    public bool Required { get; set; }
    public int MinLength { get; set; }
    public int MaxLength { get; set; }

    private QuestionConstraint() { }
    
    private QuestionConstraint(bool required, int minLength, int maxLength)
    {
        ValidateLength(minLength, maxLength);
        Required = required; 
        MinLength = minLength; 
        MaxLength = maxLength;
    }
    public static QuestionConstraint CreateRequired(bool required, int minLength, int maxLength) 
    {
        return new QuestionConstraint(required, minLength, maxLength); ;       
    }

    private void ValidateLength(int minLength, int maxLength)
    {
        if (minLength > maxLength)
            throw new ArgumentException("Question Minimun length must be less than or equal to its max lenght");
    }
}
