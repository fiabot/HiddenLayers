using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeDisplay : MonoBehaviour
{
    [Header ("Set Up Node")]
    public bool startWithNode;
    public Texture2D startingTexture;
    public string startingColor;

    [Header ("Unity Set Up")]
    public SpriteRenderer rend;
    public Transform connectToPoint;
    public Transform connectFromPoint;

    [Header ("Node Type")]
    public bool flipHor;
    public bool flipvert;
    public bool rotateClockWise;
    public bool rotateCounterClockWise;

    

    Sprite sprite;
    Texture2D tex;
    public Node node;

    /// <summary>
    /// construct and display node on a sprite. 
    /// If given a starting texture and/or color create node with that texture and color 
    /// otherwise create a blank node
    /// Begin calling updateNode every 0.2 seconds 
    /// </summary>
    void OnEnable()
    {
        if (!startWithNode)
        {
            setUpNode();
        }  
        updateNode();

        InvokeRepeating("updateNode", 0f, 0.2f);
    }

    public void setUpNode()
    {
        if (startingTexture != null)
        {
            if (startingColor != null)
            {
                Texture2D modifiedTex = ShapeModifer.changeColor(startingTexture, new GameColor(startingColor));
                node = new Node(modifiedTex);
            }
            else
            {
                node = new Node(startingTexture);
            }
        }
        else
        {
            node = new Node();
        }

        setUpNodeType();
    }

    void setUpNodeType()
    {
        if (flipHor)
        {
            node.flipHorizontally();
        }
        if (flipvert)
        {
            node.flipVertically();
        }

        if (rotateClockWise)
        {
            node.turnClockWise();
        }
        else if (rotateCounterClockWise)
        {
            node.turnCounterClockWise();
        }
    }
    public void changeNode(Node newNode)
    {
        node = newNode;
        updateNode();
    }

    /// <summary>
    /// get current texture from node and display it on sprite 
    /// </summary>
    public void updateNode()
    {
        tex = node.GetTexture2D();
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        rend.sprite = sprite;
    }

    /// <summary>
    /// If nodeDisplay is left or right clicked update connection mananger
    /// </summary>
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConnectionManager.nodeClicked(this, true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ConnectionManager.nodeClicked(this, false);
        }
    }

    /// <summary>
    /// set position of display
    /// </summary>
    /// <param name="pos"></param> position to set transfrom to 
    public void setPosition(Vector2 pos)
    {
        transform.position = pos; 
    }
}
