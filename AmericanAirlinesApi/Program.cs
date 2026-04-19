using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona os serviços de Controllers
builder.Services.AddControllers();

// 2. Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Configura o Banco de Dados (Use Sqlite para o item 5 da lista)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=americanairlines.db"));

var app = builder.Build();

// 4. ATIVA O SWAGGER (O segredo para o 404 sumir)
// No ambiente de desenvolvimento, ele libera a página /swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();