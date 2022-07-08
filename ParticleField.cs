using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpi_Particles
{
    internal class ParticleField
    {
        int _width;
        int _height;

        public bool BounceWithBoundary = false;

        public ParticleField(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public List<ParticleSourceBase> Sources = new List<ParticleSourceBase>();
        public List<ParticleForceBase> Forces = new List<ParticleForceBase>();
        public List<ParticleColliderBase> Colliders = new List<ParticleColliderBase>();

        public void Update(float deltaT)
        {
            foreach (var source in Sources)
            {
                source.Create(deltaT);

                foreach (var particle in source.Particles.ToArray())
                {
                    particle.Update(deltaT);

                    if (particle.LifeTime > particle.Longevity)
                    {
                        source.Particles.Remove(particle);
                        continue;
                    }
                    
                    foreach (var force in Forces)
                    {
                        force.Apply(particle);
                    }

                    foreach (var collider in Colliders)
                    {
                        collider.Apply(particle);
                    }

                    if (BounceWithBoundary) BoundaryBounce(particle);
                }                
            }
        }

        private void BoundaryBounce(Particle particle)
        {
            if (particle.Position.X > _width)
            {
                particle.Position.X = _width;
                particle.Speed.X *= (particle.ElasticLoss - 1.0f);
                particle.Speed.Y *= (1.0f - particle.ViscoseLoss);
            }
            if (particle.Position.X < 0)
            {
                particle.Position.X = 0;
                particle.Speed.X *= (particle.ElasticLoss - 1.0f);
                particle.Speed.Y *= (1.0f - particle.ViscoseLoss);
            }

            if (particle.Position.Y > _height)
            {
                particle.Position.Y = _height;
                particle.Speed.X *= (1.0f - particle.ViscoseLoss);
                particle.Speed.Y *= (particle.ElasticLoss - 1.0f);

            }
            if (particle.Position.Y < 0)
            {
                particle.Position.Y = 0;
                particle.Speed.X *= (1.0f - particle.ViscoseLoss);
                particle.Speed.Y *= (particle.ElasticLoss - 1.0f);
            }
        }
    };
}
