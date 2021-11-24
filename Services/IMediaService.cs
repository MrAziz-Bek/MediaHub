namespace media.hub.Services;
public interface IMediaService
{
    Task<bool> ExistsAsync(Guid id);
    Task<Media> GetImageAsync(IFormFile file);
    Task<Ad> GetAdAsync(Guid id);
    Task<List<Ad>> GetAllAsync();
    Task<Media> GetMediaAsync(Guid id);
    Task<(bool IsSuccess, Exception Exception)> CreateAsync(Ad ad);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
}