using WebCors1.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvc(options => options.Filters.Add(new AllowCorsAttribute()));
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("Policy_1", builder => builder.WithOrigins("https://localhost:7143").WithMethods("GET"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("Policy_1");


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
