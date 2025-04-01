namespace FormBuilder.Domain.Forms;

public class QuestionConstraint
{
    public int MinLength { get; set; }
    public int MaxLength { get; set; }

    private QuestionConstraint() { }
    
    private QuestionConstraint(int minLength, int maxLength)
    {
        ValidateLength(minLength, maxLength);
        MinLength = minLength; 
        MaxLength = maxLength;
    }
    public static QuestionConstraint Create(int minLength, int maxLength) 
    {
        return new QuestionConstraint (minLength, maxLength);      
    }

    private void ValidateLength(int minLength, int maxLength)
    {
        if (minLength > maxLength)
            throw new ArgumentException("Question Minimun length must be less than or equal to its max lenght");
    }
}
