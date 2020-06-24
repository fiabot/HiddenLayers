using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection 
{
    public Node startingNode;
    public Node endingNode;
    public bool isPostive;

    private bool isValid;

    /// <summary>
    /// constructor, changes node2
    /// </summary>
    /// <param name="node1"></param> node to connect to node2
    /// <param name="node2"></param> node to be connected to node1
    /// <param name="postive"></param>
    public Connection(Node node1, Node node2, bool postive)
    {
        startingNode = node1;
        endingNode = node2;
        isPostive = postive;

        isValid = true;

        connectNode();
    }

    /// <summary>
    /// add or subtract startingNode to/from endingNode
    /// if connection is still valid, based on connection type
    /// </summary>
    private void connectNode()
    {
        if (isValid)
        {
            startingNode.addChild(endingNode);
            if (isPostive)
            {
                endingNode.addNode(startingNode);
            }
            else
            {
                endingNode.subNode(startingNode);
            }
        }
    }

    /// <summary>
    /// remove starting node from ending node if connection is valid
    /// </summary>
    private void removeNode()
    {
        if (isValid)
        {
            startingNode.removeChild(endingNode);
            if (isPostive)
            {
                endingNode.removePostiveNode(startingNode);
            }
            else
            {
                endingNode.removeNegativeNode(startingNode);
            }
        }
        
    }
        

    /// <summary>
    /// change the connection to postive and modify endingNode
    /// </summary>
    public void MakePostive()
    {
        removeNode();

        isPostive = true;
        connectNode(); 
    }

    /// <summary>
    /// Change connection to negative and modify the endingNode
    /// </summary>
    public void MakeNegative()
    {
        removeNode();

        isPostive = false;
        connectNode();
    }

    /// <summary>
    /// Inverse the connection and modify endingNode 
    /// </summary>
    public void Inverse()
    {
        if (isPostive)
        {
            MakeNegative();
        }
        else
        {
            MakePostive();
        }
    }

    /// <summary>
    /// remove starting node from ending nod and make connection invalid
    /// </summary>
    public void removeConnection()
    {
        removeNode();
        isValid = false; 
    }
}
