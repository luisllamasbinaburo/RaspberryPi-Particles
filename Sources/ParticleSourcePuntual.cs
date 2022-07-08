using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.CameraProjection;
using static Raylib_cs.CameraMode;
using static Raylib_cs.Color;
using static Raylib_cs.ShaderUniformDataType;
using Rpi_Faces.Utils;
using System.Numerics;

namespace Rpi_Particles
{
    class ParticleSourcePuntual : ParticleSourceBase
    {
        public Vector2 Origin { get; set; }

        public ParticleSourcePuntual(float x, float y, float rate) : base(rate)
        {
            Origin = new Vector2(x, y);
        }

        public override void CreateParticle()
        {
            float speed = (float)(new Random().NextDouble()) * 0.5f * 8.0f;
            float angle = (float)(new Random().NextDouble()) * 2 * MathF.PI;

            Particles.Add(new Particle()
            {
                Position = Origin,
                Speed = new Vector2(speed * MathF.Cos(angle), speed * MathF.Sin(angle)),

                Size = 1,
                ElasticLoss = this.ElasticLoss,
                ViscoseLoss = this.ViscoseLoss,

                Longevity = 20f,
                LifeSpeed = 0.1f,
            });


        }
    }
}
