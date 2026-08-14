using System.Text.Json;
using Jerico.Api.Models;

namespace Jerico.Api.Services;

public class NodeRepositoryService
    {
    private readonly List<Node> _nodes;

    public NodeRepositoryService(IWebHostEnvironment env)
        {
        // Build absolute path to nodes.json
        var path = Path.Combine(env.ContentRootPath, "nodes.json");

        if (!File.Exists(path))
            throw new FileNotFoundException($"nodes.json not found at: {path}");

        var json = File.ReadAllText(path);

        // Deserialize with case-insensitive matching
        var options = new JsonSerializerOptions
            {
            PropertyNameCaseInsensitive = true
            };

        _nodes = JsonSerializer.Deserialize<List<Node>>(json, options)
                 ?? throw new Exception("Failed to deserialize nodes.json");

        // Validate nodes to prevent null crashes
        foreach (var node in _nodes)
            {
            if (node == null)
                throw new Exception("nodes.json contains a null node entry");

            if (string.IsNullOrWhiteSpace(node.Id))
                throw new Exception($"Node missing ID: {JsonSerializer.Serialize(node)}");
            }
        }

    public List<Node> GetNodes() => _nodes;
    }
