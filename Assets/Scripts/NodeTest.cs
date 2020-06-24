using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTest : MonoBehaviour
{
    public Texture2D startingTexture;
    public string startingColor; 
    public Texture2D[] addLayers;
    public string[] addColors;
    public Texture2D[] subLayers;
    public string[] subColors; 
    public SpriteRenderer rend; 
    // Start is called before the first frame update
    void Start()
    {
        //GameTexture firstLayer = ShapeModifer.changeColor(new GameTexture(startingTexture), new GameColor(startingColor));
        Texture2D firstLayer = ShapeModifer.changeColor(startingTexture, new GameColor(startingColor));
        //Debug.Log(firstLayer);
        Node thisNode = new Node(firstLayer);
        //thisNode.addTex(new GameTexture(addLayers[0]));
        
        
        for (int i = 0; i < addLayers.Length; i++)
        {
            Texture2D thisTex = addLayers[i];
            GameColor thisColor = new GameColor(addColors[i]);
            GameTexture newTex = new GameTexture(ShapeModifer.changeColor(thisTex, thisColor));
            Node newNode = new Node(newTex);

            thisNode.addNode(newNode); 
        }

        for (int i = 0; i < subLayers.Length; i++)
        {
            GameTexture thisTex = new GameTexture(subLayers[i]);
            GameColor thisColor = new GameColor(subColors[i]);
            GameTexture newTex = ShapeModifer.changeColor(thisTex, thisColor);
            Node newNode = new Node(newTex);
            thisNode.subNode(newNode);
        }
        

        Texture2D tex = thisNode.GetTexture2D();
        //Texture2D tex = firstLayer.getTex();

        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        rend.sprite = newSprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
