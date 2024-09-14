var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello!");

app.MapPost("/login", (LoginDTO LoginDTO) => {
    if (LoginDTO.Email == "adm@test.com" && LoginDTO.Senha == "123456") {
        return Results.Ok("Login efetuado com sucesso!");
    }
    else 
        return Results.Unauthorized();
});

app.Run();

public class LoginDTO 
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
}
