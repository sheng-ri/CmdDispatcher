namespace CmdDispatcher;

public class CommandMap
{
    public Dictionary<string, CommandMap> InnerMap { get; } = new();
    public Dictionary<string, Command> Cmds { get; } = new();

    public Command? resolve(int idx, string[] cmds, out string[]? args)
    {
        args = null;
        if (idx == cmds.Length)
            return null;

        var cmdStr = cmds[idx];
        if (InnerMap.TryGetValue(cmdStr, out var cmdMap))
        {
            var subResolve = cmdMap.resolve(idx + 1, cmds, out args);
            if (subResolve != null)
                return subResolve;
        }

        if (!Cmds.TryGetValue(cmdStr, out var cmd))
            return null;

        var len = cmds.Length - idx - 1;
        if (len == 0) args = [];
        else
        {
            args = new string[len];
            Array.Copy(cmds, idx + 1, args, 0, len);
        }

        return cmd;
    }

    public bool register(int idx, string[] cmds, CommandExecutor executor)
    {
        var cmdStr = cmds[idx];

        if (idx + 1 == cmds.Length)
        {
            Cmds[cmdStr] = new Command(cmdStr, executor);
            return true;
        }

        if (!InnerMap.TryGetValue(cmdStr, out var map))
        {
            map = InnerMap[cmdStr] = new CommandMap();
        }
        var result =  map.register(idx + 1, cmds, executor);
        if (!result)
        {
            if (Cmds.TryGetValue(cmdStr, out var cmd))
            {
                cmd.executor = executor;
                return true;
            }
        }

        return false;
    }
}