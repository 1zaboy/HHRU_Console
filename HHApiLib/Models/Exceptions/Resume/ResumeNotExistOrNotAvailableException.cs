namespace HHApiLib.Models.Exceptions.Resume;

public class ResumeNotExistOrNotAvailableException : Exception, IHHRUException
{
    public string RequestId { get; set; }
    public List<object> Errors { get; set; }
}
