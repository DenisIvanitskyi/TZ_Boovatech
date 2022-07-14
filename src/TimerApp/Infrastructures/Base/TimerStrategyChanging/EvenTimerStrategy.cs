using System;
using TimerApp.Infrastructures.DisplayTimer;
using TimerApp.Infrastructures.Services.Timer;

namespace TimerApp.Infrastructures.Base.TimerStrategyChanging
{
    public class EvenTimerStrategy : ITimerStrategyChanging
    {
        public bool IsShouldToUpdateTime(ITimerService timerService)
        {
            var timeSpan = TimeSpan.FromSeconds(timerService.SecondToFinish);
            return timeSpan.Seconds % 2 == 0;
        }
    }
}
