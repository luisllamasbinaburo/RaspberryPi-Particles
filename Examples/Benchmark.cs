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
    public class Benchmark
    {
        public static void Run()
        {
            InitWindow(480, 480, "Particles");
            //SetTargetFPS(60);

            var field = new ParticleField(480, 480);
            field.BounceWithBoundary = false;

            var source = new ParticleSourcePuntual(180, 240, 500);
            var gravity = new ParticleForceConstant(100.0f, 0.0f, -1.0f);
            var force = new ParticleForcePuntual(25000f, 350, 150);

            gravity.IsActive = false;
            force.IsActive = false;


            field.Sources.Add(source);
            field.Forces.Add(gravity);
            field.Forces.Add(force);
            field.Forces.Add(new ParticleForceDisipation(0.1f));

            var texture = LoadRenderTexture(15, 15);
            BeginTextureMode(texture);
            DrawCircle(8, 8, 5, new Color(255, 255, 255, 100));
            DrawCircle(8, 8, 3, new Color(255, 255, 255, 120));
            DrawCircle(8, 8, 1, WHITE);
            EndTextureMode();

            var sprite = LoadRenderTexture(480, 480);
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.BLACK);

                //DrawTexture(texture.texture, 240, 240, WHITE);

                BeginBlendMode(BlendMode.BLEND_ADDITIVE);

                field.Update((float)GetFrameTime());
                //for (int x = 50; x < 50; x++)
                //{
                //    for (int y = 50; y < 50; y++)
                //    {
                //        DrawTexture(texture.texture, (int)part.Position.X, (int)part.Position.Y, WHITE);
                //    }
                //}

                foreach (var particle in field.Sources.SelectMany(x => x.Particles).Reverse())
                {
                    DrawTexture(texture.texture, (int)particle.Position.X, (int)particle.Position.Y, GetColor(particle));
                }

                DrawFPS(10, 10);
                DrawText($"{field.Sources.SelectMany(x => x.Particles).Count()}", 100, 10, 20, Color.DARKGREEN);
                EndDrawing();

                if (IsKeyPressed(KeyboardKey.KEY_G)) gravity.IsActive = !gravity.IsActive;
                if (IsKeyPressed(KeyboardKey.KEY_F)) force.IsActive = !force.IsActive;
                if (IsKeyPressed(KeyboardKey.KEY_S)) source.IsActive = !source.IsActive;
            }

            CloseWindow();
        }

        public static Color GetColor(Particle particle)
        {
            //float t = particle.LifeTime / particle.Longevity;
            float t = particle.Speed.Length() / 250;

            t = 1.0f - Raymath.Clamp(t, 0, 1.0f);

            var color = ColorPalette.GetColorInt(index: t, palette: Palette.MAGMA, reversed: false);
            return new Color(color.r, color.g, color.b, 255);
        }
    }
}