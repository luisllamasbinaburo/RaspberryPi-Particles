using System.Numerics;

namespace Rpi_Particles
{
    public class ParticleForcePuntual : ParticleForceBase
    {
        public Vector2 Origin { get; set; }

        public ParticleForcePuntual(float force) : base(force)
        {
        }

        public ParticleForcePuntual(float x, float y, float force) : base(force)
        {
            Origin = new Vector2(x, y);
        }

        public override void Apply(Particle particle)
        {
            if (!IsActive) return;

            var vector = particle.Position - Origin;
            float dist = vector.Length();
            float force_scaler = Force / MathF.Pow(dist, 2);
            particle.Acceleration += vector * force_scaler;
        }
    }
}
