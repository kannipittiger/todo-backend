using Microsoft.EntityFrameworkCore;
using Data.DataContext;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Services.TodoList;
using Services.Product;
using Services.ProductInterface;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Add DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ AddScoped service
// builder.Services.AddScoped<IProduct,ProductService>();
builder.Services.AddScoped<IAuth,AuthService>();
builder.Services.AddScoped<ITodo,TodoService>();

// 3️⃣ Add Controllers
builder.Services.AddControllers();

// 4️⃣ Add Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("sliding", opt =>
{
    opt.PermitLimit = 10;
    opt.Window = TimeSpan.FromMinutes(1); 
    opt.SegmentsPerWindow = 6;
    opt.QueueLimit = 0;
});

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// 5️⃣ Map Controllers
app.MapControllers();

app.Run();
