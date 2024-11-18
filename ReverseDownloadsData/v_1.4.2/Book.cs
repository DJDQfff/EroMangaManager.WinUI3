namespace ReserseDownloadsData;

public partial class Book
{
    public string Id { get; set; } = null!;

    public int? EpsCount { get; set; }

    public int? Pages { get; set; }

    public int? Finished { get; set; }

    public int? LikesCount { get; set; }

    public int? TotalLikes { get; set; }

    public int? TotalViews { get; set; }

    public string? Path { get; set; }

    public string? FileServer { get; set; }

    public string? CreatedAt { get; set; }

    public string? UpdatedAt { get; set; }

    public string? Creator { get; set; }

    public string? Title { get; set; }

    public string? Title2 { get; set; }

    public string? Author { get; set; }

    public string? ChineseTeam { get; set; }

    public string? Categories { get; set; }

    public string? Tags { get; set; }

    public string? Description { get; set; }
}
