using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI;

[ApiController]
[Route("/api/[controller]")]
public class DbEmployeesController : Controller
{
    private IDbEmployeesRepository _repo;
    public DbEmployeesController(IDbEmployeesRepository repo)
    {
        _repo = repo;
    }
    [HttpGet]
    public async Task<IEnumerable<DbEmployee>> GetAllEmp()
    {
        var data = await _repo.GetAllAsync();
        return data;
    }

    [HttpPost("{id}")]
    public async Task<DbEmployee> GetUser(string id)
    {
        Console.WriteLine(id);
        var result = await _repo.Search(id);
        return result;
    }
}