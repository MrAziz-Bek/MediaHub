using System.Linq;
namespace media.hub.Services;
public class MediaService : IMediaService
{
    private readonly MediaHubDbContext _context;
    private readonly ILogger<MediaService> _logger;

    public MediaService(MediaHubDbContext context, ILogger<MediaService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception)> CreateAsync(Ad ad)
    {
        try
        {
            await _context.Ads.AddAsync(ad);
            await _context.Medias.AddRangeAsync(ad.Medias);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Image created in DB. ID: {ad.Id}");

            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Creating image in DB failed.");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
    {
        var ad = await GetAsync(id);
        ad.Medias = _context.Medias.ToList();
        if (ad == default(Ad))
        {
            return (false, new Exception("Not found."));
        }
        try
        {
            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Image deleted in DB. ID: {ad.Id}");

            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Deleting image in DB failed.");
            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(Guid id)
        => _context.Ads.AnyAsync(a => a.Id == id);

    public Task<Ad> GetAdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ad>> GetAllAsync()
        => _context.Ads.Include(m => m.Medias).ToListAsync();

    public Task<Ad> GetAsync(Guid id)
        => _context.Ads.FirstOrDefaultAsync(ad => ad.Id == id);

    public async Task<Media> GetImageAsync(IFormFile file)
    {
        using var stream = new MemoryStream();

        file.CopyTo(stream);

        return new Media(
            contentType: file.ContentType,
            data: stream.ToArray()
        );
    }

    public Task<Media> GetMediaAsync(Guid id)
        => _context.Medias.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
}