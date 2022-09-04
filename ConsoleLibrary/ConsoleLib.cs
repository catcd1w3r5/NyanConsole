using Nyan;

namespace ConsoleLibrary;

public class ConsoleLib : NyanPlugin
{
    public ConsoleLib() : base("com.Catcd.ConsoleLibrary")
    {
        Name = "ConsoleLibrary";
        Description = "A library for accessing the console.";
        Version = "0.0.1 dev";
        Author = "Catcd";
        Website = "";
    }

    CancellationTokenSource cts = new();

    private async Task ConsoleListener()
    {
        while (!cts.Token.IsCancellationRequested)
        {
            var input = Console.ReadLine();
            if (input != null) await SendMessage(input).ConfigureAwait(false);
        }
    }
    
    private void StartConsoleListener()
    {
        Task.Run(ConsoleListener,cts.Token);
    }

    protected override Task OnFinalise(NyanBot nyanBot)
    {
        //run the console listener with the cancellation token
        StartConsoleListener();
        return Task.CompletedTask;
    }

    protected override Task OnUnregister(NyanBot nyanBot)
    {
        //cancel the console listener
        cts.Cancel();
        return Task.CompletedTask;
    }

    public delegate Task MessageReceived(string message);

    public event MessageReceived OnMessageReceived = delegate { return Task.CompletedTask; };


    private async Task SendMessage(string message)
    {
        await OnMessageReceived.Invoke(message);
    }
}