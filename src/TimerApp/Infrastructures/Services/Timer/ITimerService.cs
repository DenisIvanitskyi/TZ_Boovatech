using System;

namespace TimerApp.Infrastructures.Services.Timer
{
    public interface ITimerService : IDisposable
    {
        bool IsTimerRunning { get; }

        double SecondToFinish { get; }

        event EventHandler<double> OnEverySecondChanged;

        event EventHandler OnTimerStop;

        void Start(TimeSpan timerSpan);

        void Stop();
    }
}
