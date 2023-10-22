namespace HHRU_Console.Core.Models;

public class Resume
{
    public string Title { get; set; }
    public bool IsAdvancing { get; set; }

    public Resume(string title, bool isAdvancing)
    {
        Title = title;
        IsAdvancing = isAdvancing;
    }
}
