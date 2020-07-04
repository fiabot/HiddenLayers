using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDisplay : MonoBehaviour
{
    public Layer thisLayer;

    public GameObject normalNodeEnabler;
    public GameObject HorizontalFlipNodeEnabler;
    public GameObject VerticalFlipNodeEnabler;
    public GameObject ClockWiseNodeEnabler;
    public GameObject CounterClockWiseNodeEnabler;

    private NodeSetUp[] setUps;
    // Start is called before the first frame update
    public void ShowLayer()
    {
        setUps = new NodeSetUp[thisLayer.Length()];

        // create gameObjects and set node and positon 
        for (int i = 0; i < thisLayer.Length(); i++)
        {

            Node thisNode = thisLayer.get_ith_node(i);
            GameObject thisObject;

            if (thisNode.isFLippedHorizontally())
            {
                thisObject = (GameObject)Instantiate(HorizontalFlipNodeEnabler, thisLayer.get_ith_position(i), Quaternion.identity);
            }
            else if (thisNode.isFLippedVertically())
            {
                thisObject = (GameObject)Instantiate(VerticalFlipNodeEnabler, thisLayer.get_ith_position(i), Quaternion.identity);
            }
            else if (thisNode.getRotation() == 1)
            {
                thisObject = (GameObject)Instantiate(ClockWiseNodeEnabler, thisLayer.get_ith_position(i), Quaternion.identity);
            }
            else if (thisNode.getRotation() == 3)
            {
                thisObject = (GameObject)Instantiate(CounterClockWiseNodeEnabler, thisLayer.get_ith_position(i), Quaternion.identity);
            }
            else
            {
                thisObject = (GameObject)Instantiate(normalNodeEnabler, thisLayer.get_ith_position(i), Quaternion.identity);
            }

            NodeSetUp setUp = thisObject.GetComponent<NodeSetUp>();
            setUp.startFromNode = true;
            setUp.startingNode = thisNode;
            setUp.layerNum = thisLayer.number;
            setUps[i] = setUp;
        }

        //Enable all nodeDisplays 

        foreach (NodeSetUp setUp in setUps)
        {
            setUp.enableNode();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}