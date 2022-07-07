using System;

namespace TimerApp.Services.Timer
{
    public interface ITimerService : IDisposable
    {
        bool IsTimerRunning { get; }

        double SecondToFinish { get; }

        event EventHandler<double> OnEverySecondChanged;

        void Start(TimeSpan timerSpan);

        void Stop();
    }
}
