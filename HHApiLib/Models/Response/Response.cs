namespace HHApiLib.Models.Response;

public class Response
{
    public string Id { get; set; }
    public ResponseState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ResponseResume Resume { get; set; }
    public bool ViewedByOpponent { get; set; }
    public bool HasUpdates { get; set; }
    public string MessagesUrl { get; set; }
    public string Url { get; set; }
    public ResponseCounters Counters { get; set; }
    public ResponseChatStates ChatStates { get; set; }
    public string Source { get; set; }
    public long ChatId { get; set; }
    public string MessagingStatus { get; set; }
    public bool DeclineAllowed { get; set; }
    public bool Read { get; set; }
    public bool HasNewMessages { get; set; }
    public bool Hidden { get; set; }
    public Vacancy.Vacancy Vacancy { get; set; }
}
