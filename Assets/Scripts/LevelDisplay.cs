using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    public GameObject targetDisplayPrefab;
    public GameObject LayerDisplayPrefab;
    public Level level;

    LayerDisplay inputDisplay;
    LayerDisplay[] hiddenDisplays;
    LayerDisplay outputDisplay;
    NodeDisplay[] targets;
    public void showLevel()
    {
        //display input layer 
        GameObject inputObject = (GameObject)Instantiate(LayerDisplayPrefab);
        inputDisplay = inputObject.GetComponent<LayerDisplay>();
        inputDisplay.thisLayer = level.getInputLayer();
        inputDisplay.ShowLayer();

        hiddenDisplays = new LayerDisplay[level.getNumHiddenLayer()];
        //display hidden layers 
        for (int i = 0; i < level.getNumHiddenLayer(); i++)
        {
            GameObject hiddenObject = (GameObject)Instantiate(LayerDisplayPrefab);
            hiddenDisplays[i] = hiddenObject.GetComponent<LayerDisplay>();
            hiddenDisplays[i].thisLayer = level.get_ith_hidden_layer(i);
            hiddenDisplays[i].ShowLayer();
        }

        //display output later 
        GameObject outputObject = (GameObject)Instantiate(LayerDisplayPrefab);
        outputDisplay = outputObject.GetComponent<LayerDisplay>();
        outputDisplay.thisLayer = level.getOutputLayer();
        outputDisplay.ShowLayer();
       

        //display target
        targets = new NodeDisplay[level.getGoal().Length];
        Vector2 targetPosition = level.getOutputLayer().get_ith_position(0) + Vector2.right * LevelCreator.layerSpace;
        for (int i = 0; i < level.getGoal().Length; i++)
        {
            Node node = level.getGoal()[i];
            GameObject targetInstance = Instantiate(targetDisplayPrefab, targetPosition, Quaternion.identity);
            NodeSetUp targetSetUp = targetInstance.GetComponent<NodeSetUp>();
            targetSetUp.startingNode = node;
            targetSetUp.startFromNode = true;
            targetSetUp.enableNode();
            targetPosition.y -= Layer.SPACE;
            targets[i] = targetSetUp.display;
            
        }
    }

    public void lightUpTargets()
    {
        
        for (int i = 0; i < level.getGoal().Length; i++)
        {
            
            if (level.getGoal()[i].Equals(level.getOutputLayer().get_ith_node(i)))
            {
                if (!(targets[i].numConnections == 0))
                {
                    targets[i].showConnected();
                }
                
            }
            else
            {
                if (targets[i].numConnections == 0)
                {
                    targets[i].unconnect();
                }
                
            }
        }
    }

   
}