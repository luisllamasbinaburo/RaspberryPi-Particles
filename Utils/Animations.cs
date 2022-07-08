using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpi_Faces.Utils
{

    public interface IAnimation
    {
        double Calculate(double now);        
        double GetValue();
        double GetElapsed();
    };


    public class AnimationBase : IAnimation
    {

        public AnimationBase(double interval)
        {
            Interval = interval;
        }

        public double StartTime { get; private set; }

        public double Interval { get; set; }

        internal double _value;
        internal double _now;

        public void Restart(double now)
        {
            StartTime = now;
        }

        public double GetValue()
        {
            return _value;
        }

        public bool IsExpired => _now >= StartTime + Interval;   
        public double GetElapsed()
        {
            return (_now - StartTime) % Interval;
        }

        public virtual double Calculate(double now)
        {
            _now = now;
            _value = Calculate();
            return _value;
        }

        internal virtual double Calculate()
        {
            return 0.0;
        }
    };

    public class DeltaAnimation : AnimationBase
    {
        public DeltaAnimation(double interval) : base(interval)
        { 
        }

        internal override double Calculate()
        {

            if (_now < Interval)
            {
                return 0.0;
            }
            else
            {
                return 1.0;
            }
        }
    };


    public class StepAnimation : AnimationBase
    {       

        public StepAnimation(double interval) : base(interval)
        {
        }

        internal override double Calculate()
        {
            var elapsed = _now - StartTime;
            if (elapsed < Interval)
            {
                return 0.0;
            }
            return 1.0;
        }
    };

    public class RampAnimation : AnimationBase
    {

        public RampAnimation(double interval) : base(interval)
        {
        }

        internal override double Calculate()
        {
            var elapsed = _now - StartTime;
            if (elapsed < Interval)
            {
                return elapsed / Interval;
            }
            return 1.0;
        }
    };


    public class TriangleAnimation : AnimationBase
    {
        private double _t0;
        private double _t1;

        public TriangleAnimation(double interval) : base(interval)
        {
            _t0 = interval / 2;
            _t1 = interval - _t0;
        }

        public TriangleAnimation(double t0, double t1) : base(t0 + t1)
        {
            _t0 = t0;
            _t1 = t1;
        }

        internal override double Calculate()
        {
            var elapsed = GetElapsed();

            if (elapsed < _t0)
            {
                return elapsed / _t0;
            }
            return 1.0 - (elapsed  - _t0) / _t1;
        }
    };

    public class TrapeziumAnimation : AnimationBase
    {
        private double _t0;
        private double _t1;
        private double _t2;

        public TrapeziumAnimation(double t) : base(t)
        {
            _t0 = t / 3;
            _t1 = _t0;
            _t2 = 1 - _t0 - _t1;
        }

        public TrapeziumAnimation(double t0, double t1, double t2) : base(t0 + t1 + t2)
        {
            _t0 = t0;
            _t1 = t1;
            _t2 = t2;
        }

        internal override double Calculate()
        {
            if (IsExpired) return 0.0;

            var elapsed = GetElapsed();


            if (elapsed < _t0)
            {
                return elapsed / _t0;
            }
            else if (elapsed < _t0 + _t1)
            {
                return 1.0;
            }
            else
            {
                return 1.0 - (elapsed - _t1 - _t0) / _t2;
            }
        }
    };

    public class TrapeziumPulseAnimation : AnimationBase
    {
        private double _t0;
        private double _t1;
        private double _t2;
        private double _t3;
        private double _t4;

        public TrapeziumPulseAnimation(double t) : base(t)
        {
            _t0 = 0;
            _t1 = t / 3;
            _t2 = t - _t0 - _t0;
            _t3 = _t1;
            _t4 = 0;
        }

        public TrapeziumPulseAnimation(double t1, double t2, double t3) : base(t1 + t2 + t3)
        {
            _t0 = 0;
            _t1 = t1;
            _t2 = t2;
            _t3 = t3;
            _t4 = 0;
        }

        public TrapeziumPulseAnimation(double t0, double t1, double t2, double t3, double t4) : base(t0 + t1 + t2 + t3 + t4)
        {
            _t0 = t0;
            _t1 = t1;
            _t2 = t2;
            _t3 = t3;
            _t4 = t4;
        }

        internal override double Calculate()
        {
            double elapsed = GetElapsed();

            if (elapsed < _t0)
            {
                return 0.0;
            }
            if (elapsed < _t0 + _t1)
            {
                return (elapsed - _t0) / _t1;
            }
            else if (elapsed < _t0 + _t1 + _t2)
            {
                return 1.0f;
            }
            else if (elapsed < _t0 + _t1 + _t2 + _t3)
            {
                return 1.0f - (elapsed - _t2 - _t1 - _t0) / _t3;
            }

            return 0.0;

        }
    };
}

