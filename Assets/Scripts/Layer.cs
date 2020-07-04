using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Layer
{
    public static int MAXNODES = 7;
    public static float SPACE = 3f;

    bool isInputLayer;
    bool isOutPutLayer;

    Node[] nodes;
    Vector3[] positions;
    float x;

    public int number;

    /// <summary>
    /// constructor given starting nodes, x value, and starting y value 
    /// </summary>
    /// <param name="_nodes"></param> nodes in layer
    /// <param name="xPosition"></param> x position for layer
    /// <param name="startingY"></param> y positiong of first node (at the top) 
    public Layer(Node[] _nodes, float xPosition, float startingY, int Layernumber)
    {
        nodes = _nodes;
        x = xPosition;

        setUpPostions(startingY);

        isInputLayer = false;
        isOutPutLayer = false;
    }

    /// <summary>
    /// constructor given starting nodes, x value, and starting y value 
    /// </summary>
    /// <param name="_nodes"></param> nodes in layer
    /// <param name="xPosition"></param> x position for layer
    /// <param name="startingY"></param> y postion for top layer
    public Layer(Node[] _nodes, float xPosition, float startingY, bool isInput, int Layernumber)
    {
        nodes = _nodes;
        x = xPosition;

        setUpPostions(startingY);

        isInputLayer = isInput;
        isOutPutLayer = false;
    }

    /// <summary>
    /// Constructor with given amount of blank nodes 
    /// </summary>
    /// <param name="nodes"></param> number of blank nodes to add 
    /// <param name="xPosition"></param> x postion of layer 
    /// <param name="startingY"></param> postion of top node
    public Layer(int numNodes, float xPosition, float startingY, int Layernumber)
    {
        x = xPosition;
        nodes = new Node[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            nodes[i] = new Node();
        }

        setUpPostions(startingY);

        isInputLayer = false;
        isOutPutLayer = false;

        number = Layernumber;
    }

    /// <summary>
    /// Constructor with a variety of modified blank nodes 
    /// </summary>
    /// <param name="nodes"></param> number of blank nodes in layer
    /// <param name="numHorizontalFlippedNode"></param> number of nodes flipped horizontally 
    /// <param name=" numVerticallyFlippedNodes"></param> number of nodes flippled vertically 
    /// <param name=" numClockWiseNodes"></param> number of nodes rotated clockwise 
    /// <param name=" numCounterClockNodes"></param> number of nodes rotated counter clockwise
    /// <param name="xPosition"></param> x postion of layer
    /// <param name="startingY"></param> y postion of layer 
    public Layer(int numNodes, int numHorizontalFlippedNodes, int numVerticallyFlippedNodes,
        int numClockWiseNodes, int numCounterClockNodes, float xPosition, float startingY, int Layernumber)
    {
        x = xPosition;
        int total = numNodes + numHorizontalFlippedNodes + numVerticallyFlippedNodes + numClockWiseNodes + numCounterClockNodes;
        int currentIndex = 0;
        nodes = new Node[total];
        for (int i = 0; i < numNodes; i++)
        {
            nodes[i] = new Node();
        }

        currentIndex += numNodes;
        for (int i = currentIndex; i < currentIndex + numHorizontalFlippedNodes; i++)
        {
            nodes[i] = new Node();
            nodes[i].flipHorizontally();
        }

        currentIndex += numHorizontalFlippedNodes;
        for (int i = currentIndex; i < currentIndex + numVerticallyFlippedNodes; i++)
        {
            nodes[i] = new Node();
            nodes[i].flipVertically();
        }

        currentIndex += numVerticallyFlippedNodes;
        for (int i = currentIndex; i < currentIndex + numClockWiseNodes; i++)
        {
            nodes[i] = new Node();
            nodes[i].turnClockWise();
        }

        currentIndex += numClockWiseNodes;
        for (int i = currentIndex; i < currentIndex + numCounterClockNodes; i++)
        {
            nodes[i] = new Node();
            nodes[i].turnCounterClockWise();
        }

        setUpPostions(startingY);

        isInputLayer = false;
        isOutPutLayer = false;

        number = Layernumber;
    }

    /// <summary>
    /// Create layer with given shapes and gameColors, is automatically an input layer 
    /// precondition: shapes and colors must be equal in length
    /// </summary>
    /// <param name="shapes"></param> textures of black shapes to add to nodes 
    /// <param name="colors"></param> colors for each node 
    /// <param name="xPosition"></param> x position of layer
    /// <param name="startingY"></param> y postition of top node 
    public Layer(Texture2D[] shapes, GameColor[] colors, float xPosition, float startingY, int Layernumber)
    {
        nodes = new Node[shapes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            Texture2D modifiedShape = ShapeModifer.changeColor(shapes[i], colors[i]);
            nodes[i] = new Node(modifiedShape);
        }

        x = xPosition;
        isInputLayer = true;
        isOutPutLayer = false;

        setUpPostions(startingY);

        number = Layernumber;
    }

    /// <summary>
    /// Create layer with given shapes and gameColors, is automatically an input layer 
    /// precondition: shapes must be in correct dimensions 
    /// </summary>
    /// <param name="shapes"></param> textures of black shapes to add to nodes 
    /// <param name="xPosition"></param> x position of layer
    /// <param name="startingY"></param> y postition of top node 
    public Layer(Texture2D[] shapes, float xPosition, float startingY, int Layernumber)
    {
        nodes = new Node[shapes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new Node(shapes[i]);
        }

        x = xPosition;
        isInputLayer = true;
        isOutPutLayer = false;

        setUpPostions(startingY);

        number = Layernumber;
    }

    /// <summary>
    /// default constructor, creates one blank node, is automatically an outpur layer
    /// </summary>
    /// <param name="xPostion"></param> x position of layer
    /// <param name="startingY"></param> y position of node
    public Layer(float xPostion, float startingY, int Layernumber)
    {
        nodes = new Node[1];
        nodes[0] = new Node();

        isOutPutLayer = true;
        x = xPostion;

        setUpPostions(startingY);

        number = Layernumber;
    }

    /// <summary>
    /// create postions for all the nodes 
    /// </summary>
    /// <param name="middleY"></param> y middle of layer 
    private void setUpPostions(float middleY)
    {
        positions = new Vector3[nodes.Length];
        float currentY;
        if (nodes.Length % 2 == 0)
        {
            currentY = middleY + (SPACE * (nodes.Length - 1) / 2);
        }
        else
        {
            currentY = middleY + (SPACE * Mathf.Floor(nodes.Length / 2));
        }


        for (int i = 0; i < nodes.Length; i++)
        {
            positions[i] = new Vector3(x, currentY, -20);
            currentY -= SPACE;
        }
    }
    /// <summary>
    /// return the node at the given index
    /// </summary>
    /// <param name="index"></param> index of node
    /// <returns></returns>
    public Node get_ith_node(int index)
    {
        return nodes[index];
    }

    /// <summary>
    /// get the position of the node at the given index
    /// </summary>
    /// <param name="index"></param> index of node 
    /// <returns></returns>
    public Vector2 get_ith_position(int index)
    {
        return positions[index];
    }

    /// <summary>
    /// return if layer is input layer
    /// </summary>
    /// <returns></returns> true if layer is input, otherwise false 
    public bool isInput()
    {
        return isInputLayer;
    }

    /// <summary>
    /// return if layer is output layer 
    /// </summary>
    /// <returns></returns> true if layer is output, otherwise false 
    public bool isOutput()
    {
        return isOutPutLayer;
    }

    /// <summary>
    /// change the node and position of a nodeDisplay to a node at the given index
    /// </summary>
    /// <param name="display"></param> NodeDisplay to modify
    /// <param name="index"></param> index of node to change display to 
    public void setUpNodeDisplay(NodeDisplay display, int index)
    {
        display.changeNode(get_ith_node(index));
        display.setPosition(get_ith_position(index));
    }

    public int Length()
    {
        return nodes.Length;
    }
}