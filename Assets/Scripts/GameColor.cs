using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameColor
{
    static public string[] colorNames = new string[9] { "black", "clear", "white", "red", "blue", "yellow", "green", "purple", "orange" };
    static private Dictionary<string, Color> colorValues = new Dictionary<string, Color>();
    private Color color;
    private string colorName; 

    public GameColor(string _colorName)
    {
        if (colorValues.Keys.Count == 0)
        {
            setUpDictionary();
        }
        if(_colorName == null)
        {
            colorName = "orange";
        }
        else
        {
            colorName = _colorName;
        }
        
        color = getColorFromName(colorName);

    }

    /// <summary> return true if the colors are equal, ignoring alpha
    /// 
    /// </summary>
    /// <param name="color"></param> first color to compare
    /// <param name="color2"></param> second color to compare
    /// <returns></returns> if color and otherColor are equal
    static public bool isColor(Color color, Color color2)
    {

        //return (color.r == otherColor.r && color.g == otherColor.g && color.b == otherColor.b);
        return (Mathf.Abs(color.r - color2.r) <= 0.01 && Mathf.Abs(color.g - color2.g) <= 0.01 && Mathf.Abs(color.b - color2.b) <= 0.01 && Mathf.Abs(color.a - color2.a) <= 0.01);

    }

    /// <summary> return true if the colors are equal, ignoring alpha
    /// 
    /// </summary>
    /// <param name="color"></param> first color to compare
    /// <param name="colorName"></param> name of second color to compare
    /// <returns></returns> if color and otherColor are equal
    static public bool isColor(Color color, string colorName)
    {
        Color color2 = getColorFromName(colorName);
        //return (color.r == otherColor.r && color.g == otherColor.g && color.b == otherColor.b);
        return (Mathf.Abs(color.r - color2.r) <= 0.01 && Mathf.Abs(color.g - color2.g) <= 0.01 && Mathf.Abs(color.b - color2.b) <= 0.01 && Mathf.Abs(color.a - color2.a) <= 0.01);

    }
    /// <summary> return a color from it's name, invalid inputs return black
    /// 
    /// </summary>
    /// <param name="colorName"></param> name of color, lowercase ex:"red"
    /// <returns></returns> color belonging to that name
    static public Color getColorFromName(string _colorName)
    {
        if (colorValues.ContainsKey(_colorName))
        {
            return colorValues[_colorName];
        }
        else
        {
            return Color.black;
        }

    }

    static public GameColor getGameColorFromColor(Color color)
    {
        foreach (string colorName in colorValues.Keys)
        {
            if (isColor(color, getColorFromName(colorName)))
            {
                return new GameColor(colorName);
            } 
        }

       

        return new GameColor("clear"); 
    }

    static private void setUpDictionary()
    {
        colorValues.Add("black", Color.black);
        colorValues.Add("white", Color.clear);
        colorValues.Add("clear", Color.clear);
        colorValues.Add("red", Color.red);
        colorValues.Add("blue", Color.blue);
        colorValues.Add("yellow", Color.yellow);
        colorValues.Add("green", Color.green);
        colorValues.Add("purple", new Color(0.5f, 0, 1f, 1));
        colorValues.Add("orange", new Color(1f, 0.45f, 0, 1));
    }

    /// <summary> return if some combination of colorName1 and colorName2 is present in color inputs
    /// 
    /// </summary>
    /// <param name="otherColor"></param> second color to check agaibst name 1 or 2
    /// <param name="colorName1"></param> name of first to try and find
    /// <param name="colorName2"></param> name of second color to find
    /// <returns></returns> if color1 is colorname1, or colorname2 and otherColor is the inverse
    private bool colorsPresent(GameColor otherColor, string colorName1, string colorName2)
    {
        if (getColorName() == colorName1 && otherColor.getColorName() ==  colorName2)
        {
            return true;
        }
        else if (getColorName() == colorName2 && otherColor.getColorName() == colorName1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    /// <summary> return comination of colors apart from color
    /// 
    /// 
    /// </summary>
    /// <param name="color"></param> color to subtract from black from
    /// <returns></returns> all color combination expect selected color
    private GameColor subtractFromBlack(GameColor color)
    {
        if (color.getColorName() == "red")
        {
            return new GameColor("green");
        }
        else if (color.getColorName() == "blue")
        {
            return new GameColor("orange");
        }
        else if (color.getColorName() == "yellow")
        {
            return new GameColor("purple");
        }
        else if (color.getColorName() == "orange")
        {
            return new GameColor("blue");
        }
        else if (color.getColorName() == "purple")
        {
            return new GameColor("yellow");
        }
        else if (color.getColorName() == "green")
        {
            return new GameColor("red");
        }
        else if (color.getColorName() == "clear")
        {
            return new GameColor("black");
        }
        else
        {
            return new GameColor("white");
        }
    }

    /// <summary> return name of color
    /// 
    /// </summary>
    /// <returns></returns>
    public string getColorName()
    {
        return colorName; 
    }

    public Color getColor()
    {
        return color; 
    }

    public bool Equals(GameColor otherColor)
    {
        if (colorsPresent(otherColor, "white", "clear"))
        {
            return true;
        }
        else
        {
            return getColorName().Equals(otherColor.getColorName());
        }
         
    }

    /// <summary> return the addition of two colors 
    /// Precondition: input must be on of the basic colors 
    /// Invalid colors default to black 
    /// </summary>
    /// <param name="color2"></param> second color to be added 
    /// <returns></returns> color of the combination
    public GameColor add(GameColor otherColor)
    {
        if (this.Equals(otherColor))
        {
            return this;

        }
        else if (getColorName() == "clear" || getColorName() == "white")
        {
            return otherColor;

        }
        else if (otherColor.getColorName() == "clear" || otherColor.getColorName() == "white")
        { 
            return this;
        }
        else if (colorsPresent(otherColor, "red", "blue"))
        {
            return new GameColor("purple");
        }
        else if (colorsPresent(otherColor, "red", "yellow"))
        {
            return new GameColor("orange");
        }
        else if (colorsPresent(otherColor, "blue", "yellow"))
        {
            return new GameColor("green");
        }
        else
        {
            return new GameColor("black");
        }
    }

    /// <summary> subtract one color from the other
    /// invalid colors default to clear 
    /// 
    /// </summary>
    /// <param name="otherColor"></param> color to subtract
    /// <returns></returns> valid color for color1 - otherColor 
    public GameColor subtract(GameColor otherColor)
    {
        if (Equals(otherColor))
        {
            return new GameColor("white");
        }
        else if (getColorName() == "clear" || getColorName() == "white")
        {
            return new GameColor("clear");
        }
        else if (otherColor.getColorName() == "clear" || otherColor.getColorName() == "white")
        {
            return this;
        }
        else if (getColorName() == "purple" && otherColor.getColorName() == "red")
        {
            return new GameColor("blue");
        }
        else if (getColorName() == "purple" && otherColor.getColorName() == "blue")
        {
            return new GameColor("red");
        }
        else if (getColorName() == "green" && otherColor.getColorName() == "yellow")
        {
            return new GameColor("blue");
        }
        else if (getColorName() ==  "green" && otherColor.getColorName() == "blue")
        {
            return new GameColor("yellow");
        }
        else if (getColorName() == "orange" && otherColor.getColorName() == "red")
        {
            return new GameColor("yellow");
        }
        else if (getColorName() == "orange" && otherColor.getColorName() == "yellow")
        {
            return new GameColor("red");
        }
        else if (getColorName() == "black")
        {
            return subtractFromBlack(otherColor);
        }
        else if(otherColor.getColorName() == "black")
        {
            return new GameColor("white");
        }
        else
        {
            return this; 
        }
    }

    
}
