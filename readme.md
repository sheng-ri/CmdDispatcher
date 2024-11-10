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

### CmdDispatcher
简单树形命令Dispatcher

### Summary
```text
kill (map)
|
water (cmd)
```
Dispatcher找到命令就直接进行执行，  
如果`kill`也是`命令`，那么`kill`的`子命令`会被忽略。