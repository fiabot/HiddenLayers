using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public float layerSpace;
    public Texture2D[] inputTextures;
    public string[] inputColorStrings;

    public HiddenTemplate[] hiddenLayerTemplates;
    public Texture2D targetTexture;

    public LevelDisplay display;
    public Storage levelStorage; 

    GameColor[] inputColors;
    Level level;

    int currentLayerNumber;


    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        currentLayerNumber = 0;
        Layer inputLayer = CreateInputLayer();

        Layer[] hiddenLayers = new Layer[hiddenLayerTemplates.Length];

        float currentX = transform.position.x + layerSpace;
        float y = transform.position.y;

        for (int i = 0; i < hiddenLayers.Length; i++)
        {
            currentLayerNumber++;
            hiddenLayers[i] = createHiddenLayer(hiddenLayerTemplates[i], currentX, y);
            currentX += layerSpace;

        }

        currentLayerNumber++;
        Layer outputLayer = new Layer(currentX, y, currentLayerNumber);

        Node target;
        if (targetTexture != null)
        {
            target = new Node(targetTexture);
        }
        else
        {
            target = outputLayer.get_ith_node(0);
        }


        level = new Level(inputLayer, hiddenLayers, outputLayer, target);


        display.level = level;
        display.showLevel();
    }
    void saveLevel()
    {
        Debug.Log("Saving Level");
        //levelStorage = Storage.CreateInstance<Storage>();
        targetTexture = level.getGoal().GetTexture2D();
        ConnectionManager.clearConnections();
        CreateLevel();
        levelStorage.EncodeLevel(level);

        /*AssetDatabase.Refresh();
        EditorUtility.SetDirty(levelStorage);
        AssetDatabase.SaveAssets();*/


    }
    Layer CreateInputLayer()
    {
        if(inputColorStrings.Length > 0)
        {
            inputColors = new GameColor[inputColorStrings.Length];
            for (int i = 0; i < inputColorStrings.Length; i++)
            {
                inputColors[i] = new GameColor(inputColorStrings[i]);
            }

            return new Layer(inputTextures, inputColors, transform.position.x, transform.position.y, currentLayerNumber);
        }
        else
        {
            return new Layer(inputTextures, transform.position.x, transform.position.y, currentLayerNumber);
        }
        
    }

    Layer createHiddenLayer(HiddenTemplate template, float x, float y)
    {
        Layer thisLayer = new Layer(template.numNormalNodes, template.numHorizontallyFlippedNodes, template.numVerticallyFlippedNodes,
            template.numClockwiseNodes, template.numCounterClockNodes, x,y, currentLayerNumber);

        return thisLayer;
    }

    public void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            saveLevel();
        }
    }
}
