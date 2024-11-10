namespace CmdDispatcher;

public class Command(
    string name,
    CommandExecutor? executor = null
) {
    public string name = name;
    public CommandExecutor? executor = executor;
}