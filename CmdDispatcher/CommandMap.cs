namespace CmdDispatcher;

public class CommandMap
{
    public Dictionary<string, CommandMap> InnerMap { get; } = new();
    public Dictionary<string, Command> Cmds { get; } = new();

    public Command? resolve(int idx, string[] cmds, out string[]? args)
    {
        if (idx == cmds.Length)
        {
            args = null;
            return null;
        }
        
        var cmdStr = cmds[idx];
        if (InnerMap.TryGetValue(cmdStr, out var cmdMap))
        {
            var subResolve = cmdMap.resolve(idx + 1, cmds, out args);
            if (subResolve != null)
            {
                return subResolve;
            }
        }
        
        if (Cmds.TryGetValue(cmdStr, out var cmd))
        {
            var len = cmds.Length - idx - 1;
            if (len == 0) args = [];
            else
            {
                args = new string[len];
                Array.Copy(cmds, idx + 1, args, 0, len);
            }

            return cmd;
        }


        args = null;
        return null;
    }

    public void register(int idx, string[] cmds, CommandExecutor executor)
    {
        var cmdStr = cmds[idx];
        if (Cmds.TryGetValue(cmdStr, out var cmd))
        {
            cmd.executor = executor;
            return;
        }

        if (idx + 1 == cmds.Length)
        {
            Cmds[cmdStr] = new Command(cmdStr, executor);
            return;
        }

        if (InnerMap.TryGetValue(cmdStr, out var newCmd))
        {
            newCmd.register(idx + 1,cmds,executor);
        }
        else
        {
            var map = InnerMap[cmdStr] = new CommandMap();
            map.register(idx + 1,cmds,executor);
        }
    }
}