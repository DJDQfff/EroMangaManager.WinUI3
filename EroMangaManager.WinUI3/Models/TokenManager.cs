using System.Threading;

namespace EroMangaManager.WinUI3.Models;

internal class TokenManager
{
    public static TokenManager Instance { get; private set; } = new();

    public Dictionary<MangasGroup, CancellationTokenSource> TokenDictionary = new();

    public void AddToken(MangasGroup group) { }
}
