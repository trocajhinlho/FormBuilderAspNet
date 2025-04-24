using FormBuilder.Domain.Forms;
using FormBuilder.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.Domain.Context;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Form> Form => Set<Form>();
    public DbSet<Question> Question => Set<Question>();
    public DbSet<QuestionOption> QuestionOption => Set<QuestionOption>();
    public DbSet<Submission> Submission => Set<Submission>();
    public DbSet<Answer> Answer => Set<Answer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Question>()
            .OwnsOne(q => q.Constraints);
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Form)
            .WithMany(f => f.Questions)
            .HasForeignKey(q => q.FormId);

        modelBuilder.Entity<QuestionOption>()
            .HasOne(o => o.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Submission>()
            .HasOne<Form>(s => s.Form)
            .WithMany(f => f.Submissions)
            .HasForeignKey(s => s.FormId);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Submission)
            .WithMany(s => s.Answers)
            .HasForeignKey(a => a.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(s => s.Answers)
            .HasForeignKey( a  => a.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
            


    }
}
