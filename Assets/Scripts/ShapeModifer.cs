using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeModifer 
{
    /// <summary>
    /// swap the postion of two pixel in a game texture
    /// </summary>
    /// <param name="texture"></param> GameTexture to modify
    /// <param name="x1"></param> x postion of first pixel
    /// <param name="y1"></param> y postion of first pixel
    /// <param name="x2"></param> x postion of second pixel 
    /// <param name="y2"></param> y postion of second pixel
    /// <returns></returns>
    private static void swapPixels(GameTexture texture, int x1, int y1, int x2, int y2)
    {
        GameColor firstPixel = texture.GetPixel(x1, y1);
        GameColor secondPixel = texture.GetPixel(x2, y2);

        texture.SetPixel(x1, y1, secondPixel); 
        texture.SetPixel(x2,y2, firstPixel);

    }
    /// <summary>
    /// change all black pixels to color, for gameTexture 
    /// </summary>
    /// <param name="texture"></param>GameTexture to modify
    /// <param name="color"></param> color to change shape to
    /// <returns></returns> new texture with modfied color
    public static GameTexture changeColor(GameTexture texture, GameColor color)
    {
        GameTexture returnTexture = new GameTexture(texture.width, texture.height);
        for (int x = 0; x < returnTexture.width; x++)
        {
            for (int y = 0; y < returnTexture.height; y++)
            {
                //returnTexture.SetPixel(x, y, color);
                if (texture.GetPixel(x, y).getColorName().Equals("black"))
                {
                    returnTexture.SetPixel(x, y, color);
                }

            }
        }
        return returnTexture;
    }

    /// <summary>
    /// change all black pixels to color, for texture 2D 
    /// </summary>
    /// <param name="texture"></param>GameTexture to modify
    /// <param name="color"></param> color to change shape to
    /// <returns></returns> new texture with modfied color
    public static Texture2D changeColor(Texture2D texture, GameColor color)
    {
        Texture2D returnTexture = new Texture2D(texture.width, texture.height);
        for (int x = 0; x < returnTexture.width; x++)
        {
            for (int y = 0; y < returnTexture.height; y++)
            {
            
                if (GameColor.isColor(texture.GetPixel(x, y),"black"))
                {
                    returnTexture.SetPixel(x, y, color.getColor());
                }
                else
                {
                    returnTexture.SetPixel(x, y, Color.clear);
                }

            }
        }
        return returnTexture;
    }

    /// <summary>
    /// flip GameTexture along the center horzitonal axis
    /// </summary>
    /// <param name="orginal"></param> GameTexture to flip 
    /// <returns></returns>
    public static GameTexture flipImageVertically(GameTexture orginal)
    {
        GameTexture returnTexture = new GameTexture(orginal);
        int yCutOff = Mathf.FloorToInt(returnTexture.height / 2) - 1;
        for (int x = 0; x < returnTexture.width; x++)
        {
            for (int y = 0; y < yCutOff; y++)
            {
                swapPixels(returnTexture, x, y, x, returnTexture.height - 1 - y);
            }   
        }
       return returnTexture;
    }

    /// <summary>
    /// flip GameTexture along the center vertical axis
    /// </summary>
    /// <param name="orginal"></param> GameTexture to flip 
    /// <returns></returns>
    public static GameTexture flipImageHorizontally(GameTexture orginal)
    {
        GameTexture returnTexture = new GameTexture(orginal);
        int xCutOff = Mathf.FloorToInt(returnTexture.width / 2) - 1;
        for (int y = 0; y < returnTexture.height; y++)
        {
            for (int x = 0; x < xCutOff; x++)
            {
                swapPixels(returnTexture, x, y, returnTexture.width -1 - x,  y);
            }
        }
        return returnTexture;
    }

    public static Texture2D rotate(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public static GameTexture rotate(GameTexture orignal, bool clockWise)
    {
        Texture2D orgText = orignal.getTex();
        return new GameTexture(rotate(orignal, clockWise));
    }
}
