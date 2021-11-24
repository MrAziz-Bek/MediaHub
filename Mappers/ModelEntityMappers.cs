namespace media.hub.Mappers;

public static class ModelEntityMappers
{
    public static Ad ToEntity(this AdModel ad)
    {
        var mFiles = ad.Files.Select(GetImageEntity).ToList();

        return new Ad(
            title: ad.Title,
            description: ad.Description,
            tags: string.Join(',', ad.Tags),
            medias: mFiles
        );
    }

    private static Media GetImageEntity(IFormFile file)
    {
        using var stream = new MemoryStream();

        file.CopyTo(stream);

        return new Media(
            contentType: file.ContentType,
            data: stream.ToArray()
        );
    }
}