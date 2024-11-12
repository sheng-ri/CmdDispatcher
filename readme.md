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
root (map)
|            \
kill (map)  kill(cmd)
|
water (cmd)
```
Dispatcher always try to find the deepest command.
If cmd is 'kill water', it will found `root`-`kill(map)`-`water`
If cmd is `kill other`, it will found `root`-`kill(cmd)`
### CmdDispatcher
简单树形命令Dispatcher

### Summary
```text
root (map)
|            \
kill (map)  kill(cmd)
|
water (cmd)
```
Dispatcher总是尝试寻找最深的命令。
如果命令是 'kill water', 它会找到 `root`-`kill(map)`-`water`
如果命令是 `kill other`, 它会找到 `root`-`kill(cmd)`