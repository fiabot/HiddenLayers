using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionDisplay : MonoBehaviour
{
    public float lineWidth;
    public Color nutralColor;
    public Color postiveColorStart;
    public Color postiveColorEnd;
    public Color negativeColorStart;
    public Color negativeColorEnd;

    public NodeDisplay startingNode;
    public NodeDisplay endingNode;
    public LineRenderer line;
    public PolygonCollider2D polygonCollider2D;
    public bool isPositive;
    private Vector3[] points;
    private bool complete;
    private Connection thisConnection;
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[2];
        updatePosition();
        //line.SetPositions(points);
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.startColor = nutralColor;
        line.endColor = nutralColor;
        polygonCollider2D.enabled = false; 

        complete = endingNode != null;
    }

    /// <summary>
    /// update current postions for the line renderer
    /// </summary>
    void updatePosition()
    {
        if (endingNode != null && startingNode !=null)
        {
            points[0] = startingNode.connectFromPoint.position;
            points[1] = endingNode.connectToPoint.position;
            updateCollider();
        }
        else if (startingNode != null)
        {
            points[0] = startingNode.connectFromPoint.position;
            points[1] = Camera.main.ScreenToWorldPoint( Input.mousePosition);
            
        }

        line.SetPositions(points);
    }

    /// <summary>
    /// update colors for line renderer 
    /// </summary>
    void updateColors()
    {
        
        if (isPositive)
        {
            line.startColor = postiveColorStart;
            line.endColor = postiveColorEnd;
        }
        else
        {
            line.startColor = negativeColorStart;
            line.endColor = negativeColorEnd;
        }
    }


    /// <summary>
    /// make connection between nodes and display it 
    /// </summary>
    void completeConnection()
    {
        if (startingNode.node.isSame(endingNode.node))
        {
            ConnectionManager.removeConnection(this);
            Destroy(gameObject);
        }else if (endingNode.layerInt != (startingNode.layerInt +1))
        {
            ConnectionManager.removeConnection(this);
            Debug.Log("Cannot connect nodes");
            Destroy(gameObject);
        }
        else
        {
            updateColors();   
            thisConnection = new Connection(startingNode.node, endingNode.node, isPositive);
            complete = true;
            updatePosition();
            polygonCollider2D.enabled = true;
        }
    }

    /// <summary>
    /// check if ending and starting nodes are available and update positions
    /// </summary>
    void Update()
    {
        if (endingNode != null && !complete)
        {
            completeConnection();
        }
        else
        {
            updatePosition();
        }  
    }

    /// <summary>
    /// if left click on connection inverse connection
    /// if right click on connection delete connection 
    /// </summary>
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPositive = !isPositive;
            thisConnection.Inverse();
            updateColors();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            removeConnection();
        }
    }

    /// <summary>
    /// update collider with current positions 
    /// </summary>
    public void updateCollider()
    {
        Vector2 upperLeft = new Vector2(points[0].x, points[0].y + lineWidth);
        Vector2 lowerLeft = new Vector2(points[0].x, points[0].y - lineWidth);

        Vector2 upperRight = new Vector2(points[1].x, points[1].y + lineWidth);
        Vector2 lowerRight = new Vector2(points[1].x, points[1].y - lineWidth);

        polygonCollider2D.points = new Vector2[] { upperLeft, lowerLeft,lowerRight, upperRight }; 

    }
    
    /// <summary>
    /// delete connection between nodes, 
    /// remove this from connection manager and 
    /// destroy game object
    /// </summary>
    public void removeConnection()
    {
        thisConnection.removeConnection();
        ConnectionManager.removeConnection(this);
        Destroy(gameObject);

    }

    /// <summary>
    /// destroy game object and remove connection 
    /// without removing display from connection manager
    /// </summary>
    public void destroyDisplay()
    {
        thisConnection.removeConnection();
        Destroy(gameObject);
    }
}
