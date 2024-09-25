
using FirstApp;
using FirstApp.Data;
using FirstApp.Filters;
using FirstApp.Middleware;
using FirstApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("config.json");
builder.Services.AddLogging(config=> {
    config.AddDebug();
    });
//First Way To Register Configuration.
//var attachmentsOptions = builder.Configuration.GetSection("Attachments").Get<AttachmentsOptions>();
//builder.Services.AddSingleton(attachmentsOptions);

//Second Way To Register Configuration.
//var attachmentsOptions = new AttachmentsOptions();
//builder.Configuration.GetSection("Attachments").Bind(attachmentsOptions);
//builder.Services.AddSingleton(attachmentsOptions);

/*------------(Best Practice)Third Way To Register Configuration.--------------*/
builder.Services.Configure<AttachmentsOptions>(builder.Configuration.GetSection("Attachments"));


// Add services to the container.
builder.Services.AddControllers(options =>
{
    //Global Action Filter
    options.Filters.Add<LogActivityFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProductService,ProductService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ProfilingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
