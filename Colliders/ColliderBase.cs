using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpi_Particles
{
    public abstract class ParticleColliderBase
    {
        public bool IsActive = true;

        internal float ElasticLoss = 0.7f;
        internal float ViscoseLoss = 0.1f;

        public ParticleColliderBase() { }

        public abstract void Apply(Particle particle);
    };


    public class ParticleColliderHorizontal : ParticleColliderBase
    {
        float X0;
        float X1;
        float Y;


        public ParticleColliderHorizontal(float x1, float x2, float y)
        {
            X0 = x1;
            X1 = x2;
            Y = y;
        }

        public override void Apply(Particle particle)
        {
            if (!IsActive) return;

            if (particle.LastPosition.X > X0 && particle.LastPosition.X < X1 || particle.Position.X > X0 && particle.Position.X < X1)
            {
                if (particle.Position.Y > Y && particle.LastPosition.Y < Y)
                {
                    particle.Position.Y = Y - particle.Speed.Y - particle.Size - 1;

                    particle.Speed.Y *= (ElasticLoss - 1.0f);
                    particle.Speed.X *= (1.0f - ViscoseLoss);
                }
                else if (particle.Position.Y < Y && particle.LastPosition.Y > Y)
                {
                    particle.Position.Y = Y - particle.Speed.Y + particle.Size + 1;

                    particle.Speed.Y *= (ElasticLoss - 1.0f);
                    particle.Speed.X *= (1.0f - ViscoseLoss);
                }

            }
        }
    }

    public class ParticleColliderVertical : ParticleColliderBase
    {
        float Y0;
        float Y1;
        float X;

        public ParticleColliderVertical(float y1, float y2, float x)
        {
            Y0 = y1;
            Y1 = y2;
            X = x;
        }

        public override void Apply(Particle particle)
        {
            if (!IsActive) return;

            if (particle.Position.Y >= Y0 && particle.Position.Y <= Y1)
            {
                if (
                    particle.Position.X > X && particle.LastPosition.X < X
                    )
                {
                    particle.Position.X = X - particle.Speed.X - particle.Size - 1;

                    particle.Speed.X *= (ElasticLoss - 1.0f);
                    particle.Speed.Y *= (1.0f - ViscoseLoss);
                }

                else if (
                    particle.Position.X < X && particle.LastPosition.X > X)
                {
                    particle.Position.X = X - particle.Speed.X + particle.Size + 1;

                    particle.Speed.X *= (ElasticLoss - 1.0f);
                    particle.Speed.Y *= (1.0f - ViscoseLoss);
                }

            }
        }
    }

    public class ParticleColliderCircular : ParticleColliderBase
    {
        float X;
        float Y;
        float Radius;


        public ParticleColliderCircular(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public override void Apply(Particle particle)
        {
            if (!IsActive) return;

            if (
             (particle.Position.X - X) * (particle.Position.X - X) +
             (particle.Position.Y - Y) * (particle.Position.Y - Y) > Radius * Radius
             &&
             (particle.LastPosition.X - X) * (particle.LastPosition.X - X) +
             (particle.LastPosition.Y - Y) * (particle.LastPosition.Y - Y) < Radius * Radius
             )
            {
                particle.Speed.X *= (ElasticLoss - 1.0f);
                particle.Speed.Y *= (1.0f - ViscoseLoss);
            }

            else if (
                (particle.Position.X - X) * (particle.Position.X - X) +
                (particle.Position.Y - Y) * (particle.Position.Y - Y) < Radius * Radius
                &&
                (particle.LastPosition.X - X) * (particle.LastPosition.X - X) +
                (particle.LastPosition.Y - Y) * (particle.LastPosition.Y - Y) > Radius * Radius
                )
            {
                particle.Speed.Y *= -0.6f;
            }
        }
    }
}
