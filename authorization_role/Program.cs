var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddCookie("cookie");
builder.Services.AddAuthorization();

builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
