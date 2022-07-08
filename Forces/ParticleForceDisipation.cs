using System.Numerics;

namespace Rpi_Particles
{
    public class ParticleForceDisipation : ParticleForceBase
    {
        public Vector2 Direction { get; set; }

        public ParticleForceDisipation(float force) : base(force)
        {
        }

        public override void Apply(Particle particle)
        {
            if (!IsActive) return;

            particle.Acceleration += Force * particle.Speed;
        }
    }
}
