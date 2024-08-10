using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Components.Countdown;

public partial class Countdown : IDisposable
{
    [Parameter] public int TotalTimeInSeconds { get; set; }
    [Parameter] public EventCallback<bool> Finished { get; set; }
    private int ElapsedTime { get; set; }
    private int TimeRemaining => TotalTimeInSeconds - ElapsedTime;
    private TimeSpan TimeSpan => TimeSpan.FromSeconds(TimeRemaining);
    private string FormattedTimeRemaining => string.Format("{0:D2}:{1:D2}", (int)TimeSpan.TotalMinutes, TimeSpan.Seconds);
    private Timer? Timer { get; set; }
    protected override Task OnInitializedAsync()
    {
        Timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        return base.OnInitializedAsync();
    }

    public void Dispose()
    {
        Timer?.Dispose();
    }
    
    private void OnTimerElapsed(object? state)
    {
        if (TimeRemaining <= 0)
        {
            Finished.InvokeAsync(true);
            Timer?.Dispose();
        }
        else
        {
            ElapsedTime++;
            InvokeAsync(StateHasChanged);
        }
    }
}