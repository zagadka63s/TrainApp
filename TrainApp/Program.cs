using TrainApp.Data;
using TrainApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITrainService, TrainService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// === CORS ��������� ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // ��� https ���� ����� �� https
              .AllowAnyHeader()
              .AllowAnyMethod();
        // .AllowCredentials(); // ���� ����� (��� cookie-�����������, �� ����������� ��� JWT)
    });
});

// JWT Auth config
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// === ���������� CORS ===
app.UseCors("AllowLocalhostFrontend"); // �� �������������� � �����������!

app.UseHttpsRedirection();

app.UseAuthentication();   // ������ ����� Authorization!
app.UseAuthorization();

app.MapControllers();

app.Run();
