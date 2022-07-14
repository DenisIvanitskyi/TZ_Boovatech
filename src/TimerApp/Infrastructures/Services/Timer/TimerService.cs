using System;
using System.Timers;
using SystemTimer = System.Timers.Timer;

namespace TimerApp.Infrastructures.Services.Timer
{
    public class TimerService : ITimerService, IDisposable
    {
        private SystemTimer _timer;
        private bool _isTimerRunning;
        private double _counter;

        public TimerService() : this(null)
        {

        }

        public TimerService(SystemTimer defaultTimer = null)
        {
            if (defaultTimer != null)
                _timer = defaultTimer;
            else
            {
                _timer = new SystemTimer();
                _timer.AutoReset = true;
                _timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            }

            _timer.Elapsed += Elapsed;
        }

        public double SecondToFinish { get; private set; }

        public event EventHandler<double> OnEverySecondChanged;

        public event EventHandler OnTimerStop;

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            _counter--;
            if (_counter <= 0)
                Stop();

            SecondToFinish = _counter;
            OnEverySecondChanged?.Invoke(this, _counter);
        }

        public bool IsTimerRunning => _isTimerRunning;

        public void Dispose()
        {
            if (_timer == null) return;

            Stop();
            _timer.Elapsed -= Elapsed;
            _timer.Dispose();
            _timer = null;
        }

        public void Start(TimeSpan timerValue)
        {
            if (!_isTimerRunning)
            {
                _counter = timerValue.TotalSeconds;
                _isTimerRunning = true;
                _timer?.Start();
            }
        }

        public void Stop()
        {
            if(_isTimerRunning)
            {
                _isTimerRunning = false;
                _timer?.Stop();
                OnTimerStop?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
