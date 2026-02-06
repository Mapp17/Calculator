using System.Text;
using CalculatorDomain.Logic;
using CalculatorDomain.Persistence;
using CalculatorDomainDemo.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

var dataDirectory = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data"
);

builder.Services.AddSingleton<ICalculationStore>(
    new FileCalculationStore(dataDirectory)
);

// Add services to the container.
builder.Services.AddControllers(); //tells ASP.NET that this application will use controllers as entry points
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICalculationStore>(
    new FileCalculationStore(dataDirectory)
);
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddDbContext<AppDbContext>(Options => 
Options.UseSqlite("data source = Calculator.db"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection("Jwt");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])
        )

    };
})
;

builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Calling the seed data!!
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeeder.SeedAsync(userManager, roleManager);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();


