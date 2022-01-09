using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FT.Core.Services
{
    public interface IWorkerService
    {
        int Interval { get; }

        Timer Timer { get; }

        TimerCallback TimerCallback { get; }

        void Init(int interval, TimerCallback timerCallback, bool startAutomaticaly = false);

        void Start();

        void Stop();
    }
}
