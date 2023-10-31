namespace HHApiLib.Models.Exceptions;

public interface IHHRUException
{
    public string RequestId { get; set; }
    public List<object> Errors { get; set; }
}
