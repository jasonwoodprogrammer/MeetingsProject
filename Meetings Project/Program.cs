using Meetings_Project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MeetingContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Meeting}/{action=Calendar}");




//add dummy data

using var db = new MeetingContext();

if (!db.Persons.Any())
{
    db.Add(new Person { Id = 1, FirstName = "Frodo", LastName = "Baggins" });
    db.Add(new Person { Id = 2, FirstName = "Samwise", LastName = "Gamgee" });
    db.Add(new Person { Id = 3, FirstName = "Merry", LastName = "Brandybuck" });
    db.Add(new Person { Id = 4, FirstName = "Peregrin", LastName = "Took" });
    db.Add(new Person { Id = 5, FirstName = "Bilbo", LastName = "Baggins" });
    db.Add(new Person { Id = 6, FirstName = "Farmer", LastName = "Maggot" });
    db.SaveChanges();
}

if (!db.Rooms.Any())
{
    db.Add(new Room { Id = 1, RoomName = "Bag End" });
    db.Add(new Room { Id = 2, RoomName = "Bywater Brook" });
    db.Add(new Room { Id = 3, RoomName = "Three Farthing Stone" });
    db.SaveChanges();
}


app.Run();
