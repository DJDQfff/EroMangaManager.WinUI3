using System;
using System.Collections.Generic;

namespace TonquerPicacgDatabaseReverse.Download;

public partial class Download
{
    public string BookId { get; set; } = null!;

    public string? DownloadEpsIds { get; set; }

    public int? CurDownloadEpsId { get; set; }

    public int? CurConvertEpsId { get; set; }

    public int? Tick { get; set; }

    public string? Title { get; set; }

    public string? SavePath { get; set; }

    public string? ConvertPath { get; set; }

    public string? Status { get; set; }

    public string? ConvertStatus { get; set; }
}