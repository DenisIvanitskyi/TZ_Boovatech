using Common.Mvvm;

namespace TimerApp.DisplayTimer
{
    public interface IDisplaySencondsViewModel : IBaseViewModel
    {
        double DisplayTime { get; }
    }
}
