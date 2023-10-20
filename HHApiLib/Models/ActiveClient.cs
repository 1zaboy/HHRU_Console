namespace HHApiLib.Models;

public class ActiveClient
{
    public string AuthType { get; set; }
    public bool IsApplicant { get; set; }
    public bool IsEmployer { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsApplication { get; set; }
    public bool IsEmployerIntegration { get; set; }
    public string CryptedId { get; set; }
    public string Id { get; set; }
    public bool IsAnonymous { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public object MiddleName { get; set; }
    public string LastName { get; set; }
    public string ResumesUrl { get; set; }
    public string NegotiationsUrl { get; set; }
    public bool IsInSearch { get; set; }
    public object MidName { get; set; }
    public object Employer { get; set; }
    public object Manager { get; set; }
    public object Phone { get; set; }
    public Counters Counters { get; set; }
    public ProfileVideos ProfileVideos { get; set; }
    public object PersonalManager { get; set; }
}

public class ProfileVideos
{
    public List<object> Items { get; set; }
}

public class Counters
{
    public int NewResumeViews { get; set; }
    public int UnreadNegotiations { get; set; }
    public int ResumesCount { get; set; }
}
