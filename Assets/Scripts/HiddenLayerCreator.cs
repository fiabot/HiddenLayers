using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenLayerCreator : MonoBehaviour
{
    public int numNormalNodes;
    public int numHorizontallyFlippedNodes;
    public int numVerticallyFlippedNodes;
    public int numClockwiseNodes;
    public int numCounterClockNodes;


    public LayerDisplay display;

    Layer thisLayer; 

     void Start()
    {
        thisLayer = new Layer(numNormalNodes, numHorizontallyFlippedNodes, numVerticallyFlippedNodes, 
            numClockwiseNodes, numCounterClockNodes, transform.position.x, transform.position.y);

        display.thisLayer = thisLayer;
        display.ShowLayer();

    }
}
