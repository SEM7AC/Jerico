
using Jerico.Api.Services;

namespace Jerico.Api
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<NodeStatusCacheService>();
            builder.Services.AddSingleton<NodeRepositoryService>();
            builder.Services.AddSingleton<NodeCheckerService>();
           



            var app = builder.Build();
          
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            }
        }
    }
