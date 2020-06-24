using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSetUp : MonoBehaviour
{
    public GameObject nodeObject;
    NodeDisplay display;

    public bool startFromNode;
    public Node startingNode;

    public Texture2D startingTexture;
    public string color; 
    
    // Start is called before the first frame update
    void Start()
    {
        //nodeObject.SetActive(false);
        display = nodeObject.GetComponent<NodeDisplay>(); 
    }

   public void enableNode()
    {
        display = nodeObject.GetComponent<NodeDisplay>();
        if (startFromNode)
        {
            display.node = startingNode;
            display.startWithNode = true;
        }
        else
        {
            display.startingTexture = startingTexture;
            display.startingColor = color;
            display.startWithNode = false; 
        }
        nodeObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            enableNode();
        }
    }
}
