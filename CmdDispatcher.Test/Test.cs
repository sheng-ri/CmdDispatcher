namespace CmdDispatcher.Test;

public class Test
{
    private static CommandExecutor wrongExecutor(string message) => (_, _, _) =>
    {
        Assert.Fail(message);
        return true;
    };

    [Fact]
    public void BaseTest()
    {
        var dispatcher = new CommandDispatcher();
        var inCmd = "parent sub arg";
        dispatcher.register("parent sub", (cmd, cmds, args) =>
        {
            Assert.Equal("sub", cmd.name);
            Assert.Equal(inCmd, string.Join(' ', cmds));
            Assert.Equal(["arg"], args);
            return true;
        });
        var executed = dispatcher.dispatcher(inCmd);
        Assert.True(executed, "sub is not execute.");
    }

    [Fact]
    public void FailPathTest()
    {
        var dispatcher = new CommandDispatcher();
        var label = false;
        dispatcher.register("parent", (cmd, cmds, args) =>
        {
            label = true;
            return true;
        });
        dispatcher.register("parent sub", wrongExecutor("sub cmd execute."));
        dispatcher.dispatcher("parent child");
        Assert.True(label, "parent cmd not executed..");
    }

    [Fact]
    public void SucPathTest()
    {
        var dispatcher = new CommandDispatcher();
        var label = false;
        dispatcher.register("parent", wrongExecutor("parent cmd execute."));
        dispatcher.register("parent sub", (cmd, cmds, args) =>
        {
            label = true;
            return true;
        });
        dispatcher.dispatcher("parent sub");
        Assert.True(label, "sub cmd is not execute.");
    }
}