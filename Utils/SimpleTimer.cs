using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpi_Faces.Utils
{
    public class SimpleTimer
    {
        public double StartTime { get; private set; }
        private double _currentTime;

        private double Interval { get; set; }
        public bool IsActive { get; private set; } = true;
        public bool IsExpired => _currentTime > StartTime + Interval;

        //public bool IsExpired { get; private set; }

        public event EventHandler? OnFinish;

        public SimpleTimer(double interval)
        {
            Interval = interval;
        }

        public void Start()
        {
            IsActive = true;
        }

        public void Reset(double now)
        {
            StartTime = now;
        }

        public void Stop()
        {
            IsActive = false;
        }

        public bool Update(double now)
        {
            _currentTime = now;

            if (IsActive == false) return false;

            var _hasExpired = IsExpired;
            if (_hasExpired)
            {
                OnFinish?.Invoke(this, EventArgs.Empty);
                Reset(now);
            }
            return _hasExpired;
        }

        public double GetElapsedTime()
        {
            return _currentTime - StartTime;
        }

        public double GetRemainingTime()
        {
            return _currentTime - StartTime - Interval;
        }
    }
}