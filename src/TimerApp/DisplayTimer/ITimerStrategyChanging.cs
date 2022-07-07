using TimerApp.Services.Timer;

namespace TimerApp.DisplayTimer
{
    public interface ITimerStrategyChanging
    {
        bool IsShouldToUpdateTime(ITimerService timerService);
    }
}
