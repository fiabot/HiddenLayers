using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualTest : MonoBehaviour
{
    public NodeDisplay node1;
    public NodeDisplay node2; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(node1.node.Equals(node2.node));
    }

    // Update is called once per frame
    void Update()
    {
        if (node1.node.Equals(node2.node))
        {
            Debug.Log("They are Equal");
        }
    }
}
