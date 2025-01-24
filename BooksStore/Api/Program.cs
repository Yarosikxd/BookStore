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

var builder = WebApplication.CreateBuilder(args);

// 🔹 Очистка логування та додавання нового логера
builder.Logging.ClearProviders();
builder.Services.AddLogging();

// 🔹 Реєстрація AutoMapper
builder.Services.AddAutoMapper(typeof(DataBaseMapping));

// 🔹 Додавання Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Реєстрація сервісів та репозиторіїв у DI-контейнері
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// 🔹 Реєстрація конфігурації JWT
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// 🔹 Підключення бази даних через DbContext
builder.Services.AddDbContext<DataBaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Додаємо підтримку контролерів
builder.Services.AddControllers();

var app = builder.Build();

// 🔹 Вбудована обробка помилок замість Middleware
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

// 🔹 Включаємо Swagger тільки в режимі розробки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Налаштування Cookie Policy
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

// 🔹 Включаємо аутентифікацію та авторизацію
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Включаємо HTTPS
app.UseHttpsRedirection();

// 🔹 Додаємо підтримку маршрутів
app.MapControllers();

app.Run();
