namespace FormBuilder.Domain.Shared;

internal interface IAuditable
{
    public DateTime CreatedAt { get; } 
    public DateTime ModifiedAt { get; }
}
