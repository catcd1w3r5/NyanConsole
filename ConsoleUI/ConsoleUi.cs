using Nyan;
using  ConsoleLibrary;

namespace ConsoleUI;

public class ConsoleUi : NyanPlugin, IRequirePlugin<ConsoleLib>
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
        var commands = NyanBotInstance.Instance.commands;
        commands.Response += Console.WriteLine;
        consoleLib.OnMessageReceived += message => commands.CallCommand(message);
        return Task.CompletedTask;
    }
}