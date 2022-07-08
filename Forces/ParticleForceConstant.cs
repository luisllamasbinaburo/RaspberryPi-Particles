using System.Numerics;

namespace Rpi_Particles
{
    public class ParticleForceConstant : ParticleForceBase
    {
        public Vector2 Direction { get; set; }

        public ParticleForceConstant(float force) : base(force)
        {
        }

        public ParticleForceConstant(float force, float dir_X, float dir_Y) : base(force)
        {
            Direction = new Vector2(dir_X, dir_Y);
        }


        public override void Apply(Particle particle)
        {
            if (!IsActive) return;

            particle.Acceleration += Direction * Force;
        }
    }
}
