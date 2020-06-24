using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLayerCreator : MonoBehaviour
{
    public Texture2D[] textures;
    public string[] colorStrings;

    public LayerDisplay layerDisplay; 

    Layer thisLayer;
    GameColor[] colors; 
    // Start is called before the first frame update
    void Start()
    {
        colors = new GameColor[colorStrings.Length]; 
        for (int i = 0; i < colorStrings.Length; i++)
        {
            colors[i] = new GameColor(colorStrings[i]); 
        }

        thisLayer = new Layer(textures, colors, transform.position.x, transform.position.y);
        layerDisplay.thisLayer = thisLayer;
        layerDisplay.ShowLayer();
    }

}
