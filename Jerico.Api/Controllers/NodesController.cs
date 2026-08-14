using Jerico.Api.Models;
using Jerico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jerico.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NodesController : ControllerBase
    {
    private readonly NodeRepositoryService _repo;
    private readonly NodeCheckerService _checker;

    public NodesController(NodeRepositoryService repo, NodeCheckerService checker)
        {
        _repo = repo;
        _checker = checker;
        }

    // GET: api/nodes
    [HttpGet]
    public IActionResult GetNodes()
        {
        var nodes = _repo.GetNodes();
        return Ok(nodes);
        }

    // GET: api/nodes/status
    [HttpGet("status")]
    public async Task<IActionResult> GetAllStatus()
        {
        var nodes = _repo.GetNodes();
        var results = new Dictionary<string, NodeStatus>();

        foreach (var node in nodes)
            {
            var status = await _checker.CheckAsync(node);
            results[node.Id!] = status;
            }

        return Ok(results);
        }

    // GET: api/nodes/{id}/status
    [HttpGet("{id}/status")]
    public async Task<IActionResult> GetStatus(string id)
        {
        var nodes = _repo.GetNodes();
        var node = nodes.FirstOrDefault(n => n.Id == id);

        if (node == null)
            return NotFound(new { message = $"Node '{id}' not found" });

        var status = await _checker.CheckAsync(node);
        return Ok(status);
        }
    }
