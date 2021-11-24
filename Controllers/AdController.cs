using media.hub.Mappers;

namespace media.hub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdController : ControllerBase
{
    private readonly IMediaService _mediaService;
    public AdController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] AdModel ad)
    {
        var adEntity = ad.ToEntity();

        var result = await _mediaService.CreateAsync(adEntity);

        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var file = await _mediaService.GetMediaAsync(id);

        var stream = new MemoryStream(file.Data);

        return File(stream, file.ContentType);
    }

    [HttpGet]
    public async Task<IActionResult> GetAds()
    {
        var ads = await _mediaService.GetAllAsync();
        return Ok(ads);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid Id)
    {
        var result = await _mediaService.DeleteAsync(Id);

        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
}