using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private static NodeDisplay firstClicked = null;
    private static List<ConnectionDisplay> connections;

    public GameObject connectionPrefab;
    public static GameObject staticPrefab;
    // Start is called before the first frame update
    void Start()
    {
        staticPrefab = connectionPrefab; 
        connections = new List<ConnectionDisplay>();
    }

    public static void nodeClicked(NodeDisplay display, bool isPositive)
    {
        if (firstClicked == null)
        {
            firstClicked = display;
            GameObject cObj = Instantiate(staticPrefab);
            ConnectionDisplay line = cObj.GetComponent<ConnectionDisplay>();
            line.startingNode = display;
            connections.Add(line);
            

        }else 
        {
            
            ConnectionDisplay line = connections[connections.Count - 1];
            
            if (firstClicked.node.isSame(display.node))
            {
                removeConnection(line);
                Destroy(line.gameObject);
                firstClicked.unconnect();
                display.unconnect();
                
            }else if (firstClicked.layerInt != (display.layerInt - 1))
            {
                removeConnection(line);
                Destroy(line.gameObject);
                firstClicked.unconnect();
                display.unconnect();
            }
            else
            {
                line.isPositive = isPositive;
                line.endingNode = display;
                display.updateNode();
                //firstClicked.showConnected();
                //display.showConnected();
            }
            firstClicked = null;
        }
    }

    public static void clearConnections()
    {
        foreach(ConnectionDisplay connection in connections)
        {
            connection.destroyDisplay();
        }

        connections.Clear();
    }

    public static void removeConnection(ConnectionDisplay connection)
    {
        connection.startingNode.unconnect();
        if(connection.endingNode != null)
        {
            connection.endingNode.unconnect();
        }
        
        connections.Remove(connection); 
    }

    public static bool canConnect(NodeDisplay display)
    {
        if(firstClicked == null)
        {
            return true;
        }
        else if (firstClicked.node.isSame(display.node)){ //click on the same node twice to cancel connection
            return true;
        }
        else if(firstClicked.layerInt != (display.layerInt - 1)){
            return false;
        }
        else
        {
            return true;
        }
        
    }

}
