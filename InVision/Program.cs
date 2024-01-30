using Blazored.LocalStorage;
using InVision.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Plk.Blazor.DragDrop;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<KBoardService>();
builder.Services.AddBlazorDragDrop();
builder.Services.AddSingleton<UserService>();
builder.Services.AddScoped<UserStateService>();
builder.Services.AddScoped<MatIconService>();
builder.Services.AddBlazoredLocalStorage();

// Add session-related services
// builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor(); // Needed to access the HttpContext in services

var app = builder.Build();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
