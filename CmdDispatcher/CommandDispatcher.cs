using System.Text.RegularExpressions;

namespace CmdDispatcher;

public class CommandDispatcher
{
    private readonly CommandMap map = new();
    private Regex space = new Regex(@"\s+", RegexOptions.Compiled);
    
    public bool? dispatcher(string cmdStr)
    {
        var cmds = space.Split(cmdStr);
        var cmd = resolve(cmds,out var args);
        return cmd?.executor?.Invoke(cmd, cmds,args!);
    }
    
    public void register(string cmdStr, CommandExecutor executor)
    {
        var cmds = space.Split(cmdStr);
        register(cmds,executor);
    }

    /// <summary>
    /// 首先看这一层命令是否被注册
    /// 其次再解析深层。
    /// </summary>
    /// <param name="cmds"></param>
    /// <returns></returns>
    private Command? resolve(string[] cmds,out string[]? args)
    {
        var cmdMap = map;
        for (var i = 0;i < cmds.Length ;i ++)
        {
            var cmdStr = cmds[i];
            cmdMap.CurCmds.TryGetValue(cmdStr, out var cmd);

            if (cmd != null)
            {
                var len = cmds.Length - i - 1;
                if (len == 0) args = [];
                else
                {
                    args = new string[len];
                    Array.Copy(cmds, i + 1, args,0, len);
                }
                return cmd;
            }

            if (!cmdMap.InnerMap.TryGetValue(cmdStr,out cmdMap))
            {
                break;
            }
        }
        args = null;
        return null;
    }
    
    private void register(string[] cmds,CommandExecutor executor)
    {
        var cmdMap = map;
        for (var i = 0;i < cmds.Length ;i ++)
        {
            var cmdStr = cmds[i];
            cmdMap.CurCmds.TryGetValue(cmdStr, out var cmd);
            if (cmd != null)
            {
                cmd.executor = executor;
                return;
            }

            if (i + 1 == cmds.Length)
            {
                cmdMap.CurCmds[cmdStr] = new Command(cmdStr,executor);
                return;
            }

            if (cmdMap.InnerMap.TryGetValue(cmdStr, out var newCmd))
            {
                cmdMap = newCmd;
            }
            else
            {
                var newMap = new CommandMap();
                cmdMap.InnerMap[cmdStr] = newMap;
                cmdMap = newMap; 
            }
        }
    }
}