namespace DataProcessingLibrary;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DataProcessingLibrary
{
    private readonly HttpClient _client;
    private readonly DbContext _context;
    private readonly ILogger _logger;
    
    public DataProcessingLibrary(IHttpClientFactory  factory, DbContext context, ILogger logger)
    {
        _client = factory.CreateClient();
        _context = context;
        _logger = logger;
    }

    public Task ProcessData()
    {
        return Task.FromResult(0);
    }
}
