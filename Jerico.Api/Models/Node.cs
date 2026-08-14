namespace Jerico.Api.Models;

public class Node
    {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Endpoint { get; set; }
    public string? Type { get; set; } // dns, http, icmp, etc
    }

