namespace CmdDispatcher;

public delegate bool CommandExecutor (Command cmd, string[] cmds, string[] args);