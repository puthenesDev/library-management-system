namespace LibraryManagement.Domain.Entities;
public class Member
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime RegisteredAt { get; private set; }

    private Member() { }

    public Member(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        RegisteredAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");

        Name = name;
        Email = email;
    }
}
