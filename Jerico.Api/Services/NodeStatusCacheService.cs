using Jerico.Api.Models;

namespace Jerico.Api.Services;

public class NodeStatusCacheService
    {

    private readonly Dictionary<string, NodeStatus> _cache = new();

    public void Update(string id, NodeStatus status)
        {
        _cache[id] = status;
        }

    public IReadOnlyDictionary<string, NodeStatus> GetAll()
        {
        return _cache;
        }

    public NodeStatus? Get(string id)
        {
        _cache.TryGetValue(id, out var status);
        return status;
        }
    }

