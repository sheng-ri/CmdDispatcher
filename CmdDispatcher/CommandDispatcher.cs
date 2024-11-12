using System.Text.RegularExpressions;

namespace CmdDispatcher;

public partial class CommandDispatcher
{
    private readonly CommandMap rootMap = new();
    private Regex space = SpaceRegex();
    
    [GeneratedRegex(@"\s+", RegexOptions.Compiled)]
    private static partial Regex SpaceRegex();
    
    public bool? dispatcher(string cmdStr)
    {
        var cmds = space.Split(cmdStr);
        var cmd = rootMap.resolve(0,cmds,out var args);
        return cmd?.executor?.Invoke(cmd, cmds,args!);
    }
    
    public void register(string cmdStr, CommandExecutor executor)
    {
        var cmds = space.Split(cmdStr);
        rootMap.register(0,cmds,executor);
    }

}