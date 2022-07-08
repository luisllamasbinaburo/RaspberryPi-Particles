using System.Numerics;

public class Particle
{
    public Particle()
    {

    }

    public Particle(float posX, float posY, float speedX, float speedY)
    {
    }

    public float LifeTime;
    public Vector2 LastPosition;
    public Vector2 Position;
    public Vector2 Speed;
    public Vector2 Acceleration;

    public float Size;

    public float Longevity;
    public float LifeSpeed;

    public float ElasticLoss;
    public float ViscoseLoss;

    public float GetAbsoluteSpeed()
    {
        return MathF.Sqrt(Speed.X * Speed.X + Speed.Y * Speed.Y);
    }

    public void Update()
    {
        LastPosition.X = Position.X;
        LastPosition.Y = Position.Y;

        Speed.X -= Acceleration.X;
        Speed.Y -= Acceleration.Y;

        Position.X += Speed.X;
        Position.Y += Speed.Y;

        Acceleration.X = 0;
        Acceleration.Y = 0;

        LifeTime += LifeSpeed;
    }
};
