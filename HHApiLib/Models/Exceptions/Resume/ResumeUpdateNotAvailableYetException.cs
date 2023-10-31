namespace HHApiLib.Models.Exceptions.Resume;

public class ResumeUpdateNotAvailableYetException : Exception, IHHRUException
{
    public string RequestId { get; set; }
    public List<object> Errors { get; set; }
}
