namespace Rpi_Particles
{
    public abstract class ParticleSourceBase
    {
        public ParticleSourceBase(float rate)
        {
            Particles = new List<Particle>();
            EmissionRate = rate;
        }   

        public List<Particle> Particles;

        public bool IsActive = true;
        public float EmissionRate = 20;

        public float Size = 1;
        public float LifeSpeedMax = 120f;
        public float LifeSpeedMin = 60.0f;
        public float ElasticLoss = 0.8f;
        public float ViscoseLoss = 0.1f;

        public void Emit(int num_particles)
        {
            if (!IsActive) return;

            for (int i = 0; i < num_particles; i++)
            {
                CreateParticle();
            }
        }

        public abstract void CreateParticle();

        public void Create(double deltaT)
        {
            if (!IsActive) return;

            var correctedEmissionRate = EmissionRate * deltaT;
            int num_particles = (int)correctedEmissionRate;

            float dice = (float)(new Random().NextDouble());
            if (dice < correctedEmissionRate - num_particles) num_particles++;

            Emit(num_particles);
        }
    };
}
