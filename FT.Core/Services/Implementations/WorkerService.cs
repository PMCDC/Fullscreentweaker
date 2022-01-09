using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FT.Core.Services
{
    public class WorkerService : IWorkerService
    {
        private int _interval;
        private Timer _timer;
        private TimerCallback _timerCallback;

        public int Interval { get => _interval; }

        public Timer Timer { get => _timer; }
        public TimerCallback TimerCallback { get => _timerCallback; }

        public void Init(int interval, TimerCallback timerCallback, bool startAutomaticaly = false)
        {
            _interval = interval;
            _timerCallback = timerCallback;

            if (startAutomaticaly)
            {
                Start();
            }
        }

        public void Start()
        {
            if (_timerCallback == null)
            {
                throw new ArgumentNullException(nameof(_timerCallback));
            }


            if (_timer == null)
            {
                //Create Timer if first time through
                _timer = new Timer(new TimerCallback(_timerCallback), this, 0, _interval);
            }
            else
            {
                _timer.Change(0, _interval);
            }
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }
    }
}
