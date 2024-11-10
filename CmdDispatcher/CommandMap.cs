namespace CmdDispatcher;

public class CommandMap
{
    public Dictionary<string, CommandMap> InnerMap { get; } = new();
    public Dictionary<string, Command> CurCmds { get; } = new();
}
