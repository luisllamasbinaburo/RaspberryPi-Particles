using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.CameraProjection;
using static Raylib_cs.CameraMode;
using static Raylib_cs.Color;
using static Raylib_cs.ShaderUniformDataType;
using Rpi_Faces.Utils;
using Rpi_Particles;


namespace Rpi_Faces
{
    public class Example1
    {
        public static void Run()
        {
            InitWindow(480, 480, "Particles");
            SetTargetFPS(60);

            var field = new ParticleField(480, 480);
            var source = new ParticleSourcePuntual(240, 240, 100.0f);
            field.Sources.Add(source);
            field.Forces.Add(new ParticleForceConstant(0.10f, 0.0f, -1.0f));

            //field.Forces.Add(new ParticleForcePuntual(240, 300, 100f));
            //field.Forces.Add(new ParticleForceDisipation(0.75f));

            field.Colliders.Add(new ParticleColliderHorizontal(100, 300, 240));
            field.Colliders.Add(new ParticleColliderHorizontal(250, 420, 360));

            var texture = LoadRenderTexture(5, 5);
            BeginTextureMode(texture);
            DrawCircle(2, 2, 1, WHITE);
            EndTextureMode();


            var animation = new TriangleAnimation(5.0);

            var sprite = LoadRenderTexture(480, 480);
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.BLACK);
                BeginBlendMode(BlendMode.BLEND_ADDITIVE);

                var x = animation.Calculate(GetTime());
                source.Origin = new System.Numerics.Vector2((int)(x * 200 - 100 + 240), 100);

                field.Update();
                foreach (var particle in field.Sources.SelectMany(x => x.Particles))
                {
                    DrawTexture(texture.texture, (int)particle.Position.X, (int)particle.Position.Y, GetColor(particle));
                }
                EndDrawing();
                DrawFPS(10, 10);
                DrawText($"{field.Sources.SelectMany(x => x.Particles).Count()}", 100, 10, 20, Color.DARKGREEN);
            }

            CloseWindow();
        }

        public static Color GetColor(Particle particle)
        {
            float t = particle.LifeTime / particle.Longevity;

            t = 1.0f - Raymath.Clamp(t, 0, 1.0f);

            var color = ColorPalette.GetColorInt(index: t, palette: Palette.MAGMA, reversed: false);
            return new Color(color.r, color.g, color.b, 180);
        }
    }
}