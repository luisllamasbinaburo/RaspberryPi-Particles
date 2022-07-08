using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpi_Particles
{
    public abstract class ParticleForceBase
    {
        public float Force;
        public bool IsActive = true;

        public ParticleForceBase(float force) { Force = force; }

        public abstract void Apply(Particle particle);
    };
}
