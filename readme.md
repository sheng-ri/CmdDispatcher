### CmdDispatcher
tree hierarchy command dispatcher.

### Usage
[TestMain.cs](CmdDispatcher/Test/TestMain.cs)
```csharp
var dispatcher = new CommandDispatcher();
dispatcher.register("kill water",(cmd, cmds, args) =>
{
    Console.WriteLine(cmd.name);
    return true;
});
dispatcher.dispatcher("kill water you");
```

### Summary
```text
kill (map)
|
water (cmd)
```
Dispatcher always found cmd then execute.  
If `kill` is a `cmd`, it will execute and always ignore `sub cmd`.