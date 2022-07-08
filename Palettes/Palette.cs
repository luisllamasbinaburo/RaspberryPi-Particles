public class ColorPalette
{
    public static (float r, float g, float b) GetColorFloat(float index, (int r, int g, int b)[] palette, bool reversed = false)
    {
        if (reversed) index = 1.0f - index;
        var i = (int)(index * 255);
        
        float r = (float)palette[i].r / 255f;
        float g = (float)palette[i].g / 255f;
        float b = (float)palette[i].b / 255f;

        return (r, g, b);
    }

    public static (int r, int g, int b) GetColorInt(float index, (int r, int g, int b)[] palette, bool reversed = false)
    {
        if (reversed) index = 1.0f - index;

        var i = (int)(index * 255);
        return palette[i];
    }
};
