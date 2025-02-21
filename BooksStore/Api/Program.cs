using Application.Services;
using Core.Abstraction.Auth;
using Core.Abstraction.Repository;
using Core.Abstraction.Services;
using Infrastructure;
using Infrastructure.JWT;
using Infrastructure.Password;
using Infrastructure.Repositoryes;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Налаштування логування
builder.Logging.ClearProviders();
builder.Services.AddLogging();

// Додаємо AutoMapper для мапінгу моделей
builder.Services.AddAutoMapper(typeof(DataBaseMappings));

// Налаштування Swagger для документації API
builder.Services.AddSwaggerGen();

// Налаштування JWT
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// Реєстрація залежностей для роботи з репозиторіями та сервісами
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Отримуємо налаштування JWT та перевіряємо їх
var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey))
{
    throw new ArgumentNullException("JwtOptions:SecretKey cannot be null or empty.");
}

// Додаємо аутентифікацію через JWT
builder.Services.AddApiAuthentication(jwtOptions);

// Налаштування підключення до бази даних
builder.Services.AddDbContext<DataBaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додаємо контролери
builder.Services.AddControllers();

var app = builder.Build();

// Обробка глобальних винятків
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"{{\"error\":\"{error.Error.Message}\"}}");
        }
    });
});

// Додаємо Swagger лише в режимі розробки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Налаштування політики використання кукі
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

// Включення аутентифікації та авторизації
app.UseAuthentication();
app.UseAuthorization();

// Перенаправлення HTTP-запитів на HTTPS
app.UseHttpsRedirection();

// Маршрутизація контролерів
app.MapControllers();

// Запуск застосунку
app.Run();
