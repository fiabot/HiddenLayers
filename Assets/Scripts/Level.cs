using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    Layer inputLayer;
    Layer[] hiddenLayers;
    Layer outputLayer;
    Node outputNode;
    Node goal; 

    public Level(Layer input, Layer[] hidden, Layer output, Node target)
    {
        inputLayer = input;
        hiddenLayers = hidden;
        outputLayer = output;
        outputNode = output.get_ith_node(0);
        goal = target; 
    }

    public Level(Texture2D[] inputTextures, Vector2 inputPostion, HiddenTemplate[] hiddenTemplates,
        Vector2[] HiddenPostions, Vector2 outputPostion, Texture2D target)
    {
        int currentLayerNumber = 0;
        inputLayer = new Layer(inputTextures, inputPostion.x, inputPostion.y, currentLayerNumber);
        hiddenLayers = new Layer[hiddenTemplates.Length];

        for (int i = 0; i < hiddenTemplates.Length; i++)
        {
            currentLayerNumber++; 
            HiddenTemplate template = hiddenTemplates[i];
            Vector2 postion = HiddenPostions[i];
            hiddenLayers[i] = new Layer(template.numNormalNodes, template.numHorizontallyFlippedNodes, template.numVerticallyFlippedNodes,
            template.numClockwiseNodes, template.numCounterClockNodes, postion.x, postion.y, currentLayerNumber);
        }

        currentLayerNumber++;
        outputLayer = new Layer(outputPostion.x, outputPostion.y, currentLayerNumber);
        outputNode = outputLayer.get_ith_node(0);

        goal = new Node(target); 

    }


    public Layer getInputLayer()
    {
        return inputLayer; 
    }

    public Layer get_ith_hidden_layer(int index)
    {
        return hiddenLayers[index]; 
    }

    public int getNumHiddenLayer()
    {
        return hiddenLayers.Length;
    }

    public Layer getOutputLayer()
    {
        return outputLayer;
    }

    public Node getGoal()
    {
        return goal; 
    }

    public bool hasWon()
    {
        return outputNode.Equals(goal);
    }
}
