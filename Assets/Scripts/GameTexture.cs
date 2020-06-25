using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTexture
{
    public int width;
    public int height;
    private GameColor[,] totalColors;
    private int[,] blackLayers; 
    private Texture2D tex; 

    /// <summary> default gameTexture with clear pixels
    /// 
    /// </summary>
    /// <param name="_width"></param> number of pixels for the width
    /// <param name="_height"></param> number of pixels for the height
    public GameTexture(int _width, int _height)
    {
        width = _width;
        height = _height;

        totalColors = new GameColor[width, height];
        blackLayers = new int[width, height];
        tex = new Texture2D(width, height);

        clear();
    }

    /// <summary>
    /// GameTexture from existing Texture 2D 
    /// </summary>
    /// <param name="orginalTexture"></param> starting Texture 2D 
    public GameTexture(Texture2D orginalTexture)
    {
        width = orginalTexture.width;
        height = orginalTexture.height;

        totalColors = new GameColor[width, height];
        blackLayers = new int[width, height];
        tex = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameColor color = GameColor.getGameColorFromColor(orginalTexture.GetPixel(x, y));
                if (color.getColorName().Equals("black"))
                {
                    blackLayers[x, y] = 1;
                }
                else
                {
                    blackLayers[x, y] = 0; 
                }

                totalColors[x, y] = color;
            }
        }
    }

    /// <summary>
    /// Contruct game texture from 2d array of color strings 
    /// </summary>
    /// <param name="colorStrings"></param>
    public GameTexture(string[,] colorStrings)
    {
        width = colorStrings.GetLength(0);
        height = colorStrings.GetLength(0);
        totalColors = new GameColor[width, height];

        blackLayers = new int[width, height];
        tex = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                GameColor color = new GameColor(colorStrings[x, y]);
                if (color.getColorName().Equals("black"))
                {
                    blackLayers[x, y] = 1;
                }
                else
                {
                    blackLayers[x, y] = 0;
                }

                totalColors[x, y] = color;
            }
        }
    }

    /// <summary>
    /// GameTexture from existing GameTexture
    /// </summary>
    /// <param name="orginalTexture"></param> starting Texture 2D 
    public GameTexture(GameTexture orginalTexture)
    {
        width = orginalTexture.width;
        height = orginalTexture.height;

        totalColors = new GameColor[width, height];
        blackLayers = new int[width, height];
        tex = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameColor color = orginalTexture.GetPixel(x, y);
                if (color.getColorName().Equals("black"))
                {
                    blackLayers[x, y] = 1;
                }
                else
                {
                    blackLayers[x, y] = 0;
                }
                totalColors[x, y] = color;
            }
        }
    }

    /// <summary>
    /// set all pixels to clear 
    /// </summary>
    public void clear()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                blackLayers[x, y] = 0;
                totalColors[x, y] = new GameColor("clear");
            }
        }
    }

    /// <summary>
    /// get Texture 2D from GameTexture
    /// </summary>
    /// <returns></returns> Texture2D form of GameTexture
    public Texture2D getTex()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, totalColors[x, y].getColor());
            }
        }
        tex.Apply();
        return tex; 
    }

    /// <summary>
    /// Set pixel at x,y to color
    /// </summary>
    /// <param name="x"></param> x coordinate
    /// <param name="y"></param> y coordinate
    /// <param name="color"></param> gameColor to change to
    public void SetPixel(int x, int y, GameColor color)
    {
        totalColors[x, y] = color;
    }

    /// <summary>
    /// get GameColor at x ,y 
    /// </summary>
    /// <param name="x"></param> x coordinate
    /// <param name="y"></param> y coordinate
    /// <returns></returns> GameColor at coordinate 
    public GameColor GetPixel(int x, int y)
    {
        return totalColors[x, y]; 
    }

    /// <summary>
    /// add all colors from texToAdd to this
    /// </summary>
    /// <param name="texToAdd"></param> GameTexture we want to add
    public void addTexture(GameTexture texToAdd)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameColor color = texToAdd.GetPixel(x, y);
                if (color.getColorName().Equals("black"))
                {
                    blackLayers[x, y]++;
                }

                if (blackLayers[x, y] > 0)
                {
                    totalColors[x, y] = new GameColor("black");
                }
                else if (blackLayers[x, y] < 0)
                {
                    totalColors[x, y] = new GameColor("clear");
                }
                else
                {
                    totalColors[x, y] = GetPixel(x, y).add(color);
                }
                
            }
        }
    }

    /// <summary>
    /// subtract all colors of this texture from textToSub
    /// </summary>
    /// <param name="texToSub"></param> GameTexture to subrtact
    public void subTexture(GameTexture texToSub)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameColor color = texToSub.GetPixel(x, y);
                if (color.Equals(new GameColor("black")))
                 {
                    blackLayers[x, y]--;
                }
                else if (!color.Equals(new GameColor("clear")))
                {
                    blackLayers[x, y] = 0;
                }

                if (blackLayers[x, y] > 0)
                {
                    totalColors[x, y] = new GameColor("black");
                }
                else if (blackLayers[x, y] < 0)
                {
                    totalColors[x, y] = new GameColor("clear");
                }
                else
                {
                    totalColors[x, y] = GetPixel(x, y).subtract(color);
                }
                
            }
        }
    }

    public string[,] GetColorStrings()
    {
        string[,] returnString = new string[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameColor color = GetPixel(x, y);
                
                returnString[x, y] = color.getColorName();
            }
        }
        return returnString;
    }

    /// <summary>
    /// return true if all pixels match with another gameTexture 
    /// </summary>
    /// <param name="texToCompare"></param> Gametexture to test equality
    /// <returns></returns> true if textures are equal 
    public bool Equals(GameTexture texToCompare)
    {
        if (width != texToCompare.width || height != texToCompare.height)
        {
            return false;
        }
        else
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!GetPixel(x, y).Equals(texToCompare.GetPixel(x, y)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
        
}
