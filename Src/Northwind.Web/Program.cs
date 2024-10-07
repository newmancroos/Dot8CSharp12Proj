using Northwind.DataContext.Sqlite;


#region Configure the Web server host and services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddNorthwindContext();
var app = builder.Build();
#endregion

#region Configure the HTTP Pipeline and routes

if (!app.Environment.IsDevelopment())
{ 
    app.UseHsts();
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapRazorPages();

//app.MapGet("/", () => "Hello World!");
#endregion


//Start the webserver, Host the website, and wait for requests
app.Run(); // This is a thread-blocking call.

WriteLine("This executes after the webserver has stopped");
