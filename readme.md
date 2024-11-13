### CmdDispatcher
tree hierarchy command dispatcher.

### Usage
[TestMain.cs](CmdDispatcher/Test/TestMain.cs)
```csharp
var dispatcher = new CommandDispatcher();
dispatcher.register("par sub",(cmd, cmds, args) =>
{
    Console.WriteLine(cmd.name);
    return true;
});
dispatcher.dispatcher("par sub arg");
```

### Summary
```text
root (map)
|            \
par (map)  par(cmd)
|
sub (cmd)
```
Dispatcher always try to find the deepest command.  
If cmd is `par sub`, it will found `root`-`par(map)`-`sub`  
If cmd is `par child`, it will found `root`-`par(cmd)`
### CmdDispatcher
简单树形命令Dispatcher

### Summary
```text
root (map)
|            \
par (map)  par(cmd)
|
sub (cmd)
```
Dispatcher总是尝试寻找最深的命令。  
如果命令是 `par sub` 它会找到 `root`-`par(map)`-`sub`  
如果命令是 `par child`, 它会找到 `root`-`par(cmd)`