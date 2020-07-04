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
    public int numTargets;

    public bool developerMode;
    public Texture2D[] targetTexture;

    public LevelDisplay display;
    //public Storage levelStorage;
    public string LevelName;

    GameColor[] inputColors;
    Level level;

    int currentLayerNumber;
    bool hasWon = false;


    // Start is called before the first frame update
    void Awake()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        currentLayerNumber = 0;

        //create input layer
        Layer inputLayer = CreateInputLayer();

        //create hidden layers
        Layer[] hiddenLayers = new Layer[hiddenLayerTemplates.Length];

        float currentX = transform.position.x + layerSpace;
        float y = transform.position.y;

        for (int i = 0; i < hiddenLayers.Length; i++)
        {
            currentLayerNumber++;
            hiddenLayers[i] = createHiddenLayer(hiddenLayerTemplates[i], currentX, y);
            currentX += layerSpace;

        }

        //create output layer
        currentLayerNumber++;
        Layer outputLayer = new Layer(numTargets, currentX, y, currentLayerNumber);

        //create targets
        Node[] target = new Node[numTargets];
        if (targetTexture != null && !developerMode)
        {
            for (int i = 0; i < targetTexture.Length; i++)
            {
                target[i] = new Node(targetTexture[i]);
            }

        }
        else
        {
            for (int i = 0; i < numTargets; i++)
            {
                target[i] = outputLayer.get_ith_node(i);
            }

        }


        level = new Level(inputLayer, hiddenLayers, outputLayer, target);


        display.level = level;
        display.showLevel();
    }
    void saveLevel()
    {
        if (developerMode)
        {
            Debug.Log("Saving Level");
            targetTexture = new Texture2D[numTargets];
            //levelStorage = Storage.CreateInstance<Storage>();
            for (int i = 0; i < numTargets; i++)
            {
                targetTexture[i] = level.getGoal()[i].GetTexture2D();
                SaveNodeToPNG.save(level.getGoal()[i], LevelName + "_targetNode_" + i);
            }
            ConnectionManager.clearConnections();
            CreateLevel();

        }


        //levelStorage.EncodeLevel(level);

        /*AssetDatabase.Refresh();
        EditorUtility.SetDirty(levelStorage);
        AssetDatabase.SaveAssets();*/


    }
    Layer CreateInputLayer()
    {
        if (inputColorStrings.Length > 0)
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
            template.numClockwiseNodes, template.numCounterClockNodes, x, y, currentLayerNumber);

        return thisLayer;
    }

    public void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            saveLevel();
        }

        if (!developerMode && level.hasWon() && !hasWon)
        {
            Debug.Log("Won");

            if (LevelCompleteMenu.instance != null)
            {
                LevelCompleteMenu.instance.showMenu();
            }

            hasWon = true;
        }
    }
}