namespace DataProcessingLibrary;

public class DataProcessingLibrary
{
    private readonly IHttpClient _client;
    private readonly IDbContext context;
    public DataProcessingLibrary(IHttpClientFactory factory, IDbContext context, ILogger logger)
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
