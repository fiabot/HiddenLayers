using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private bool flippedVertically;
    private bool flippedHorizontally;
    private int rotation; 
    private List<Node> positiveNodes = new List<Node>();
    private List<Node> negativeNodes = new List<Node>();
    private List<Node> children = new List<Node>();
    private GameTexture mainTexture;
    private GameTexture baseTex;
    private static int nextNodeId;
    private int id;
    

    public static int WIDTH = 150;
    public static int HEIGHT = 150;
    
    /// <summary> create new node with starting texture
    /// precondition: texture must be WIDTH x HEIGHT pixels large
    /// </summary>
    /// <param name="__startingTexture"></param> texture to default to and build up from
    public Node (Texture2D startingTexture)
    {
        flippedVertically = false;
        flippedHorizontally = false;
        rotation = 0;

        mainTexture = new GameTexture(startingTexture);
        baseTex = new GameTexture(startingTexture);
        id = nextNodeId;
        nextNodeId++;
    }

    /// <summary> create new node with starting texture
    /// precondition: texture must be WIDTH x HEIGHT pixels large
    /// </summary>
    /// <param name="__startingTexture"></param> texture to default to and build up from
    public Node(GameTexture startingTexture)
    {
        flippedVertically = false;
        flippedHorizontally = false;
        rotation = 0; 

        mainTexture = new GameTexture(startingTexture);
        baseTex = startingTexture;

        id = nextNodeId;
        nextNodeId++;
    }

    /// <summary>
    /// default constructor
    /// </summary>
    public Node()
    {
        flippedVertically = false;
        flippedHorizontally = false;
        rotation = 0;

        mainTexture = new GameTexture(WIDTH, HEIGHT);
        baseTex = mainTexture;

        id = nextNodeId;
        nextNodeId++;
    }

    /// <summary>
    /// set all pixels to correct color
    /// </summary>
    public void updateTex()
    {
        GameTexture newTexture = new GameTexture(WIDTH, HEIGHT);
        newTexture.addTexture(baseTex);

        
        foreach(Node n in positiveNodes)
        {
            newTexture.addTexture(n.GetGameTexture());
        }

        foreach (Node n in negativeNodes)
        {
            newTexture.subTexture(n.GetGameTexture());
        }

        if (flippedHorizontally)
        {
            newTexture = ShapeModifer.flipImageHorizontally(newTexture);
        }

        if (flippedVertically)
        {
            newTexture = ShapeModifer.flipImageVertically(newTexture);
        }
        
        if(rotation == 1)
        {
            Texture2D rotated = ShapeModifer.rotate(newTexture.getTex(), true);
            newTexture = new GameTexture(rotated);
        }
        else if (rotation == 2)
        {
            Texture2D rotated = ShapeModifer.rotate(newTexture.getTex(), true);
            rotated = ShapeModifer.rotate(rotated, true);
            newTexture = new GameTexture(rotated);
        }
        else if (rotation == 3)
        {
            Texture2D rotated = ShapeModifer.rotate(newTexture.getTex(), false);
            newTexture = new GameTexture(rotated);
        }

        mainTexture = newTexture;

        foreach(Node child in children)
        {
            child.updateTex();
        }
    }

    /// <summary>
    /// return if node has child 
    /// </summary>
    /// <param name="childToFind"></param> potenial child to locate 
    /// <returns></returns>
    private bool hasChild(Node childToFind)
    {
        foreach (Node child in children)
        {
            if (child.id == childToFind.id)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// flip node along the vertical axis
    /// </summary>
    public void flipHorizontally()
    {
        flippedHorizontally = !flippedHorizontally;
        updateTex();
    }

    /// <summary>
    /// flip node along the horizontal axis
    /// </summary>
    public void flipVertically()
    {
        flippedVertically = !flippedVertically;
        updateTex();
    }

    /// <summary>
    /// turn node 90 degrees clock wise 
    /// </summary>
    public void turnClockWise()
    {
        if (rotation >= 3)
        {
            rotation = 0;
        }
        else
        {
            rotation++; 
        }

        updateTex();
    }

    /// <summary>
    /// turn node 90 degrees counter clockwise 
    /// </summary>
    public void turnCounterClockWise()
    {
        if (rotation <= 0)
        {
            rotation = 3;
        }
        else
        {
            rotation--;
        }
    }

    /// <summary>
    /// return if node is flipped along the vertical axis
    /// </summary>
    /// <returns></returns> true if flipped 
    public bool isFLippedHorizontally()
    {
        return flippedHorizontally; 
    }

    /// <summary>
    /// return if node is flipped along the horizontal axis
    /// </summary>
    /// <returns></returns> true if flipped 
    public bool isFLippedVertically()
    {
        return flippedVertically;
    }

    /// <summary>
    /// return the current rotation of node
    /// 0 - 0 degree rotation, 1 - 90, 2-180, 3 -270 -- clock wise rotation 
    /// </summary>
    /// <returns></returns> what rotation the node is set to
    public int getRotation()
    {
        return rotation;
    }
    /// <summary>
    /// return texture 2D for node
    /// </summary>
    /// <returns></returns> main texture of Node
    public Texture2D GetTexture2D()
    {
        //updateTex();
        return mainTexture.getTex();
    }

    /// <summary>
    /// get game texture for node
    /// </summary>
    /// <returns></returns> main GameTexture of node
    public GameTexture GetGameTexture()
    {
        return mainTexture; 
    }

    /// <summary>
    /// postively add new node to this node 
    /// </summary>
    /// <param name="nodeToAdd"></param>
    public void addNode(Node nodeToAdd)
    {
        positiveNodes.Add(nodeToAdd);
        updateTex();

    }

    /// <summary>
    /// negatively subtract node from this node 
    /// </summary>
    /// <param name="nodeToAdd"></param>
    public void subNode(Node nodeToAdd)
    {
        negativeNodes.Add(nodeToAdd);
        updateTex();

    }

    /// <summary>
    /// remove postive parent
    /// </summary>
    /// <param name="nodToRemove"></param> parent to remove 
    public void removePostiveNode(Node nodToRemove)
    { 
        positiveNodes.Remove(nodToRemove);
        updateTex();

    }

    /// <summary>
    /// remove negative parent 
    /// </summary>
    /// <param name="nodToRemove"></param> parent to remove 
    public void removeNegativeNode(Node nodToRemove)
    {
        negativeNodes.Remove(nodToRemove);
        updateTex();
    }

    /// <summary>
    /// return if pixels of this node are indentical to another 
    /// </summary>
    /// <param name="nodeToCompare"></param> node to test eqaulity 
    /// <returns></returns>
    public bool Equals(Node nodeToCompare)
    {
        return mainTexture.Equals(nodeToCompare.GetGameTexture());
    }

    /// <summary>
    /// return true if nodeToCompare is current node 
    /// </summary>
    /// <param name="nodeToCompare"></param> node to test againsnt 
    /// <returns></returns>
    public bool isSame(Node nodeToCompare)
    {
        return id == nodeToCompare.id;
    }

    /// <summary>
    /// make this node a parent of another node 
    /// </summary>
    /// <param name="child"></param> node to add to children 
    public void addChild(Node child)
    {
        if (!hasChild(child))
        {
            children.Add(child);
        } 
    }

    /// <summary>
    /// remove parental connections with current child 
    /// </summary>
    /// <param name="child"></param> current child to remove 
    public void removeChild (Node child)
    {
        if (hasChild(child))
        {
            children.Remove(child);
        }
    }
}



