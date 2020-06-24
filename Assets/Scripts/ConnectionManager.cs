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
            
            firstClicked = null;
            ConnectionDisplay line = connections[connections.Count -1];
            line.isPositive = isPositive; 
            line.endingNode = display;
            display.updateNode();
        }
    }


}
