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
    public class Example2
    {
        public static void Run()
        {
            InitWindow(480, 480, "Particles");
            SetTargetFPS(60);

            var field = new ParticleField(480, 480);
            field.BounceWithBoundary = true;

            var source = new ParticleSourcePuntual(180, 240, 50.0f);
            
            var gravity = new ParticleForceConstant(0.10f, 0.0f, -1.0f);
            var force = new ParticleForcePuntual(350, 150, 25f);

            field.Sources.Add(source);
            field.Forces.Add(gravity);
            field.Forces.Add(force);

            var texture = LoadRenderTexture(5, 5);
            BeginTextureMode(texture);
            DrawCircle(2, 2, 1, WHITE);
            EndTextureMode();
           
            var sprite = LoadRenderTexture(480, 480);
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.BLACK);
                BeginBlendMode(BlendMode.BLEND_ADDITIVE);

                field.Update();
                foreach (var particle in field.Sources.SelectMany(x => x.Particles))
                {
                    DrawTexture(texture.texture, (int)particle.Position.X, (int)particle.Position.Y, GetColor(particle));
                }

                EndDrawing();
                DrawFPS(10, 10);
                DrawText($"{field.Sources.SelectMany(x => x.Particles).Count()}", 100, 10, 20, Color.DARKGREEN);

                if (IsKeyPressed(KeyboardKey.KEY_G)) gravity.IsActive = !gravity.IsActive;
                if (IsKeyPressed(KeyboardKey.KEY_F)) force.IsActive = !force.IsActive;
            }

            CloseWindow();
        }

        public static Color GetColor(Particle particle)
        {
            //float t = particle.LifeTime / particle.Longevity;
            float t = particle.Speed.Length() / 10.0f;

            t = 1.0f - Raymath.Clamp(t, 0, 1.0f);

            var color = ColorPalette.GetColorInt(index: t, palette: Palette.MAGMA, reversed: false);
            return new Color(color.r, color.g, color.b, 180);
        }
    }
}