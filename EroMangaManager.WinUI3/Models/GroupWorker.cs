using System.Threading;

namespace EroMangaManager.WinUI3.Models;

public class GroupWorker
{
    public static GroupWorker Instance { get; private set; } = new();
    private Queue<MangasGroup> filepaths = new();

    private readonly Dictionary<MangasGroup, CancellationTokenSource> tokenDic = new();

    public void StartGroup(MangasGroup group)
    {
        if (tokenDic.ContainsKey(group))
        {
            CancellationTokenSource cancellationTokenSource = new();

            tokenDic.Add(group, cancellationTokenSource);
        }
    }

    public void RemoveWork(MangasGroup group)
    {
        if (tokenDic.TryGetValue(group, out var value))
        {
            tokenDic.Remove(group);

            value.Cancel();
        }
    }
}
