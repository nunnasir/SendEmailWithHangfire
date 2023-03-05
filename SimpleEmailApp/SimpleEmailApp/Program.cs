global using SimpleEmailApp.Services.EmailService;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/EmailTemplates/{0}.cshtml");
});



builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHangfire(x => x.UseSqlServerStorage(string.Format(@"Data Source=DESKTOP-H28I08B;Initial Catalog=hangfireTest;Integrated Security=True")));
builder.Services.AddHangfireServer(); ;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/hangfire");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
