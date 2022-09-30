using Nyan;
using ConsoleLibrary;
using Nyan.Plugins;

namespace ConsoleUI;

public sealed class ConsoleUi : NyanPlugin, IRequirePlugin<ConsoleLib>
{
    public ConsoleUi() : base("com.Catcd.ConsoleUI")
    {
        Name = "Console UI";
        Description = "A simple console UI for Nyan";
        Version = "0.0.1 dev";
        Author = "Catcd";
    }

    protected override Task OnFinalise(NyanBot nyanBot)
    {
        if (!nyanBot.TryGetPlugin<ConsoleLib>(out var consoleLib)) throw new Exception("ConsoleLibrary not found");
        var commands = BotInstance.Instance.commands;
        commands.RegisterOutput(msg => Console.WriteLine(msg.ToString()));
        consoleLib.OnMessageReceived += message =>
        {
            var command = message.Split(' ')[0].AsSpan();
            var args = message.Substring(command.Length + 1).AsSpan();
            commands.CallCommand(command, args);
            return Task.CompletedTask;
        };
        return Task.CompletedTask;
    }
}