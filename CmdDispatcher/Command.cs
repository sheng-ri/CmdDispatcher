namespace CmdDispatcher;

public class Command(
    string name,
    CommandExecutor? executor = null
) {
    public readonly string name = name;
    public CommandExecutor? executor = executor;
}