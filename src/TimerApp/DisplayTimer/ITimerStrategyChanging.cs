
using TimerApp.Infrastructures.Services.Timer;

namespace TimerApp.Infrastructures.DisplayTimer
{
    public interface ITimerStrategyChanging
    {
        bool IsShouldToUpdateTime(ITimerService timerService);
    }
}
