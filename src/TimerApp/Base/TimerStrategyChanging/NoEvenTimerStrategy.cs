
using System;
using TimerApp.DisplayTimer;
using TimerApp.Services.Timer;

namespace TimerApp.Base.TimerStrategyChanging
{
    public class NoEvenTimerStrategy : ITimerStrategyChanging
    {
        public bool IsShouldToUpdateTime(ITimerService timerService)
        {
            var timeSpan = TimeSpan.FromSeconds(timerService.SecondToFinish);
            return timeSpan.Seconds % 2 != 0;
        }
    }
}
