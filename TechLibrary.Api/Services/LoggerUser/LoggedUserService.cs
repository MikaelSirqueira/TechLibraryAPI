using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAcess;

namespace TechLibrary.Api.Services.LoggerUser;

public class LoggedUserService
{
    private readonly HttpContext _httpContext;
    public LoggedUserService(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    public User User(TechLibraryDbContext dbContext)
    {
        var authentication = _httpContext.Request.Headers.Authorization.ToString();
        var token = authentication["Bearer ".Length..].Trim();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurytiToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSecurytiToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

        var userId = Guid.Parse(identifier);
        var user = dbContext.Users.First(user => user.Id == userId);

        return user;
    }
}
