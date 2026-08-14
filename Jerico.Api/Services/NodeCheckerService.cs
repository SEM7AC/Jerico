using Jerico.Api.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Jerico.Api.Services;

public class NodeCheckerService
    {

    private readonly HttpClient _http = new HttpClient();

    public async Task<NodeStatus> CheckAsync(Node node)
        {
        return node.Type switch
            {
                "icmp" => await CheckIcmp(node),
                "http" => await CheckHttp(node),
                _ => new NodeStatus
                    {
                    Id = node.Id,
                    Reachable = false,
                    Error = "Unknown node type",
                    LastChecked = DateTime.UtcNow
                    }
                };
        }

    private async Task<NodeStatus> CheckIcmp(Node node)
        {
        var ping = new Ping();
        try
            {
            var sw = Stopwatch.StartNew();
            var reply = await ping.SendPingAsync(node.Endpoint, 2000);
            sw.Stop();

            return new NodeStatus
                {
                Id = node.Id,
                Reachable = reply.Status == IPStatus.Success,
                LatencyMs = sw.ElapsedMilliseconds,
                Error = reply.Status == IPStatus.Success ? null : reply.Status.ToString(),
                LastChecked = DateTime.UtcNow
                };
            }
        catch (Exception ex)
            {
            return new NodeStatus
                {
                Id = node.Id,
                Reachable = false,
                Error = ex.Message,
                LastChecked = DateTime.UtcNow
                };
            }
        }

    private async Task<NodeStatus> CheckHttp(Node node)
        {
        try
            {
            var sw = Stopwatch.StartNew();
            var response = await _http.GetAsync(node.Endpoint);
            sw.Stop();

            return new NodeStatus
                {
                Id = node.Id,
                Reachable = response.IsSuccessStatusCode,
                LatencyMs = sw.ElapsedMilliseconds,
                Error = response.IsSuccessStatusCode ? null : response.StatusCode.ToString(),
                LastChecked = DateTime.UtcNow
                };
            }
        catch (Exception ex)
            {
            return new NodeStatus
                {
                Id = node.Id,
                Reachable = false,
                Error = ex.Message,
                LastChecked = DateTime.UtcNow
                };
            }
        }

    }

