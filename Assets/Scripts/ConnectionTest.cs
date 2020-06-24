using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTest : MonoBehaviour
{
    public NodeDisplay[] startingConnection;
    public NodeDisplay[] endingConnection;
    public bool[] connectionPostives; 
    private Connection[] connections;
    // Start is called before the first frame update
    void Start()
    {
        connections = new Connection[startingConnection.Length];

        for (int i = 0; i < startingConnection.Length; i++)
        {
            Connection connection = new Connection(startingConnection[i].node, endingConnection[i].node, connectionPostives[i]);
            connections[i] = connection;
            endingConnection[i].updateNode();
        } 
    }
}
