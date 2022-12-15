using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure;
using LabsAndCoursesManagement.Infrastructure.Generics;
using LabsAndCoursesManagement.Infrastructure.Generics.GenericRepositories;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5173", "http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<DatabaseContext>();

// with generic implementation
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IRepository<Teacher>, TeacherRepository>();
builder.Services.AddScoped<IRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IRepository<Grade>, GradeRepository>();
builder.Services.AddScoped<IRepository<Didactic>, DidacticRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }