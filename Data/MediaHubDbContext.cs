namespace media.hub.Data;
public class MediaHubDbContext : DbContext
{
    public MediaHubDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Ad> Ads { get; set; }

    public DbSet<Media> Medias { get; set; }
}