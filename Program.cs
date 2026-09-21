var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapHub<LoginConVerificacionHub>("/login");
app.MapGet("/verificar/usuario/{userId}", async (
    string userId,
    ILogger<LoginConVerificacionHub> logger,
    IHubContext<LoginConVerificacionHub> hubContext) =>
{
    logger.LogInformation($"Se notificara al cliente con id {userId}");
    await hubContext.Clients.Client(userId).SendAsync("VerificacionOk", userId);
});

app.Run();
