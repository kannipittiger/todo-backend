using Microsoft.EntityFrameworkCore;
using Data.DataContext;
using Services.ProductInterface;
using Services.Product;
using Services.TodoList;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Add DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ AddScoped service
// builder.Services.AddScoped<IProduct,ProductService>();
builder.Services.AddScoped<ITodo,TodoService>();

// 3️⃣ Add Controllers
builder.Services.AddControllers();

// 4️⃣ Add Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 5️⃣ Map Controllers
app.MapControllers();

app.Run();
