using Nyan;
using DSharpPlus.Entities;
using Nyan.Plugins;

namespace DiscordConsole;

public sealed class DiscordConsole : NyanPlugin
{
    private DiscordChannel? _channel;

    public DiscordConsole() : base("com.Catcd.DiscordConsole")
    {
        Name = "Linking Discord and Console";
        Description = "A simple discord console UI for Nyan";
        Version = "0.0.1 dev";
        Author = "Catcd";
    }

    protected override async Task OnFinalise(NyanBot nyanBot)
    {
        var commands = BotInstance.Instance.commands;
        _channel = await nyanBot.Client.GetChannelAsync(1016047930853040230);

        await commands.RegisterOutput(message =>
        {
            try
            {
                _channel.SendMessageAsync(message.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });

        nyanBot.Client.MessageCreated += (_, e) =>
        {
            if (e.Author.Id == nyanBot.Client.CurrentUser.Id || e.Message.Channel.Id != _channel.Id)
                return Task.CompletedTask;
            var message = e.Message.Content;
            var command = message.Split(' ')[0].AsSpan();
            var args = message.Substring(command.Length + 1).AsSpan();
            commands.CallCommand(command, args);

            return Task.CompletedTask;
        };
    }
}