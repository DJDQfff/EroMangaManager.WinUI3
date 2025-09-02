using System;
using System.Collections.Generic;

namespace TonquerPicacgDatabaseReverse.Download;

public partial class DownloadEp
{
    public string BookId { get; set; } = null!;

    public int EpsId { get; set; }

    public string? EpsTitle { get; set; }

    public int? PicCnt { get; set; }

    public int? CurPreDownloadIndex { get; set; }

    public int? CurPreConvertId { get; set; }
}