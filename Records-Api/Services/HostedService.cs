using Records_Api.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Records_Api.Services
{
    public class HostedService : IHostedService
    {
        private Timer _timer;
        //private AutoResetEvent _autoResetEvent;
        private Action _action;

        public HostedService(Action action)
        {
            _action = action;
            //_autoResetEvent = new AutoResetEvent(false);
            TimerCallback tc = new TimerCallback(UpdateRecords);
            Timer timer = new Timer(tc, null, 0, 2000);
            //_timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            //TimerStarted = DateTime.Now;

            //TimerCallback tc = new TimerCallback(UpdateRecords);
            //Timer timer = new Timer(tc, null, 0, 2000);
        }

        public void UpdateRecords(object o)
        {
            _action();
            Console.WriteLine(1);
        }
    }
}
