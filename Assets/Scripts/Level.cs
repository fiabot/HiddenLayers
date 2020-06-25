using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
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
