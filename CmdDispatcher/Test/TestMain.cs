namespace CmdDispatcher.Test;

public class TestMain
{
    public static void Main()
    {
        var dispatcher = new CommandDispatcher();
        dispatcher.register("kill water",(cmd, cmds, args) =>
        {
            Console.WriteLine(cmd.name);
            return true;
        });
        dispatcher.dispatcher("kill water you");
    }
}