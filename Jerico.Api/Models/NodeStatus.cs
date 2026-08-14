namespace Jerico.Api.Models;

public class NodeStatus
    {
    public string? Id { get; set; }
    public bool Reachable { get; set; }
    public long LatencyMs { get; set; }
    public string? Error { get; set; }
    public DateTime LastChecked { get; set; }
    }

