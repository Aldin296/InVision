using InVision_API.Models;
using InVision_API.Services;

namespace InVision_API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Verbindung zur Datenbank
            builder.Services.Configure<InVisionDatabaseSettings>
            (builder.Configuration.GetSection("InVisionDatabase"));
            builder.Services.AddSingleton<UserService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<NoteService>();
            builder.Services.AddScoped<KBoardService>();
			builder.Services.AddScoped<CalendarService>();
            builder.Services.AddScoped<ToDoItemService>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}