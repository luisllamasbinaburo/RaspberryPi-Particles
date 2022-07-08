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
        public float LifeSpeedMax = 0.3f;
        public float LifeSpeedMin = 0.02f;
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

        public void Create()
        {
            if (!IsActive) return;

            int num_particles = (int)EmissionRate;
            float dice = (float)(new Random().NextDouble());
            if (dice < EmissionRate - num_particles) num_particles++;

            Emit(num_particles);
        }
    };
}
