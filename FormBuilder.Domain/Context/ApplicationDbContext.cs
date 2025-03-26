using FormBuilder.Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.Domain.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Form> Form => Set<Form>();
    public DbSet<Question> Question => Set<Question>();
    public DbSet<QuestionOption> QuestionOption => Set<QuestionOption>();
    public DbSet<Submission> Submission => Set<Submission>();
    public DbSet<Answer> Answer => Set<Answer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .OwnsOne(q => q.Constraints);
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Form)
            .WithMany(f => f.Questions)
            .HasForeignKey(q => q.Id);

        modelBuilder.Entity<QuestionOption>()
            .HasOne(o => o.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.QuestionId);

        modelBuilder.Entity<Submission>()
            .HasOne<Form>(s => s.Form)
            .WithMany(f => f.Submissions)
            .HasForeignKey(s => s.FormId);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Submission)
            .WithMany(s => s.Answers)
            .HasForeignKey(a => a.SubmissionId);


    }
}
