using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Storage : ScriptableObject
{
    //public Level level;
    public LevelDisplay display;


    public Vector2 inputPostion;
    public string[][,] inputColorValues;

    Vector2[] hiddenLayerPositions;
    public int[] numNormalNodes;
    int[] numVerticalNodes;
    int[] numHorizontalNodes;
    int[] numClockWiseNodes;
    int[] numCounterClockWiseNodes;

    Vector2 outputPostion;
    string[,] targetColorValues;

    bool inInputLayers;
    int inputLayerLength;
    int inputLayerIndex;

    public void EncodeLevel(Level level)
    {
        Layer inputLayer = level.getInputLayer();
        inputPostion = inputLayer.get_ith_position(0);
        inputColorValues = new string[inputLayer.Length()][,];
        for (int i = 0; i < inputLayer.Length(); i++)
        {
            GameTexture texture = inputLayer.get_ith_node(i).GetGameTexture();
            inputColorValues[i] = texture.GetColorStrings();
        }

        int numHiddenLayers = level.getNumHiddenLayer();
        hiddenLayerPositions = new Vector2[numHiddenLayers];
        numNormalNodes = new int[numHiddenLayers];
        numHorizontalNodes = new int[numHiddenLayers];
        numVerticalNodes = new int[numHiddenLayers];
        numClockWiseNodes = new int[numHiddenLayers];
        numCounterClockWiseNodes = new int[numHiddenLayers];

        for (int layer = 0; layer < numHiddenLayers; layer++)
        {
            Layer hiddenLayer = level.get_ith_hidden_layer(layer);
            hiddenLayerPositions[layer] = hiddenLayer.get_ith_position(0);
            for (int i = 0; i < hiddenLayer.Length(); i++)
            {
                //numNormalNodes[i]++;
                Node node = hiddenLayer.get_ith_node(i);
                if (node.isFLippedHorizontally())
                {
                    numHorizontalNodes[layer]++;
                }else if (node.isFLippedVertically())
                {
                    numVerticalNodes[layer]++; 
                }else if (node.getRotation() == 1)
                {
                    numClockWiseNodes[layer]++;
                }
                else if (node.getRotation() == 3)
                {
                    numCounterClockWiseNodes[layer]++;
                }
                else
                {
                    numNormalNodes[layer]++;
                }
            }
        }
        Debug.Log(numNormalNodes[0]);
        outputPostion = level.getOutputLayer().get_ith_position(0);
        targetColorValues = level.getGoal().GetGameTexture().GetColorStrings();
        WriteToFile("SomeOtherName");

    }
    public void WriteToFile(string fileName)
    {
        string path = "Assets/" + fileName + ".txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path);
        writer.WriteLine("<INPUTVECTOR2>" + inputPostion.x + "/" + inputPostion.y);
        writer.WriteLine("<NUMINPUTLAYERS>" + inputColorValues.Length);
        writer.WriteLine("<INPUTLAYERSBEGIN>");
        for (int i = 0; i < inputColorValues.Length; i++)
        {
            writer.WriteLine(ColorArrayToString(inputColorValues[i]));
        }
        writer.WriteLine("<INPUTLAYERSEND>");

        writer.WriteLine("<HIDDENLAYERPOSITIONS>" + PositionArrayToString(hiddenLayerPositions));
        writer.WriteLine("<NUMNORMALNODES>" + IntArrayToString(numNormalNodes));
        writer.WriteLine("<NUMHORIZONTALNODES>" + IntArrayToString(numHorizontalNodes));
        writer.WriteLine("<NUMVERTICALNODES>" + IntArrayToString(numNormalNodes));
        writer.WriteLine("<NUMCLOCKWISENODES>" + IntArrayToString(numClockWiseNodes));
        writer.WriteLine("<NUMCOUNTERCLOCKWISENODES>" + IntArrayToString(numCounterClockWiseNodes));

        writer.WriteLine("<OUTPUTPOSITiON>" + outputPostion.x + "/" + outputPostion.y);
        writer.WriteLine("<TARGETTEXTURE>" + ColorArrayToString(targetColorValues));

        writer.Close();

        //AssetDatabase.Refresh();
        //AssetDatabase.SaveAssets();
    }

    string ColorArrayToString(string[,] colors)
    {
        string returnString =  colors.GetLength(0) + "/" + colors.GetLength(1) + "!";
        for (int x = 0; x < colors.GetLength(0); x++)
        {
            for (int y = 0; y < colors.GetLength(1); y++)
            {
                returnString += colors[x, y] + "/";
                //returnString += "black" + "/";
            }
            returnString += "*"; 
        }
        return returnString;
    }

    string PositionArrayToString(Vector2[] positions)
    {
        string returnString = "";
        for (int i = 0; i < positions.Length; i++)
        {
            returnString += positions[i].x + "/" + positions[i].y + "*"; 
        }
        return returnString; 
    }

    string IntArrayToString(int[] array)
    {
        string returnString = "";
        for (int i = 0; i < array.Length; i++)
        {
            returnString += array[i] + "/"; 
        }
        return returnString; 
    }

    void parseInputVector(string line)
    {
        string parsedLine = line.Replace("<INPUTVECTOR2>", "");
        string[] stringpos = parsedLine.Split('/');
        inputPostion = new Vector2(float.Parse(stringpos[0]), float.Parse(stringpos[1]));
    }

    void parseNumInputLayers(string line)
    {
        string parsedLine = line.Replace("<NUMINPUTLAYERS>", "");
        inputLayerLength = int.Parse(parsedLine);
    }

    void beginInputLayer()
    {
        inInputLayers = true;
        inputColorValues = new string[inputLayerLength][,];
        inputLayerIndex = 0; 
    }

    string[,] getStringArrayFromLine(string line)
    {
        string[,] returnArray;
        try
        {
            string[] lineBreakDown = line.Split('!');
            string[] demensions = lineBreakDown[0].Split('/');
            returnArray = new string[int.Parse(demensions[0]), int.Parse(demensions[0])];

            string[] rows = lineBreakDown[1].Split('*');
            
            //rows = rows.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            /*if (rows.Length != returnArray.GetLength(0))
            {
                Debug.Log("rows: " + rows.Length + " Org:" + returnArray.GetLength(0));
                Debug.Log(rows[0]);
            }*/
            for (int x = 0; x < int.Parse(demensions[0]); x++)
            {
                //Debug.Log("Next Columns");
                try
                {
                    string[] columns = rows[x].Split('/');
                    columns = columns.Where(i => !string.IsNullOrEmpty(i)).ToArray();
                    for (int y = 0; y < int.Parse(demensions[0]); y++)
                    {
                        //Debug.Log(returnArray[x, y]);
                        //returnArray[x, y] = "black";
                        returnArray[x, y] = columns[y];
                    }
                }
                catch
                {
                    Debug.Log(rows[x]);
                }
                
                
            }
        }catch(Exception e)
        {
            Debug.Log(line);
            Debug.Log(e);
            returnArray = new string[10, 10];
        }
        return returnArray; 
    }

    Vector2[] GetPositionArrayFromLine(string line)
    {
        string[] rows = line.Split('*');
        rows = rows.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        Vector2[] returnArray = new Vector2[rows.Length];
        try
        {
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split('/');
         
                returnArray[i].x = float.Parse(columns[0]);
                returnArray[i].y = float.Parse(columns[1]);
                
                
            }
        }catch (Exception e)
        {
            Debug.Log("Could not Get Position Array");
            Debug.Log(line);
            Debug.Log(e);
        }
        
        
        return returnArray;
    }

    int[] GetIntArrayFromLine(string line)
    {
        string[] nums = line.Split('/');
        nums = nums.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        int[] returnArray = new int[nums.Length];
        try
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != "")
                {
                    returnArray[i] = int.Parse(nums[i]);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Could not Get int array from line");
            Debug.Log(line);

            Debug.Log(e);
        }
        

        return returnArray; 
    }

    void endInputLayer()
    {
        inInputLayers = false; 
    }

    void parseInputTexture(string line)
    {
        inputColorValues[inputLayerIndex] = getStringArrayFromLine(line);
        inputLayerIndex++; 
    }

    void parseHiddenPosition(string line)
    {
        string parsedLine = line.Replace("<HIDDENLAYERPOSITIONS>", "");
        hiddenLayerPositions = GetPositionArrayFromLine(parsedLine);
    }

    void parseNormaNodes(string line)
    {
        string parsedLine = line.Replace("<NUMNORMALNODES>", "");
        numNormalNodes = GetIntArrayFromLine(parsedLine); 

    }

    void parseHorizontalNodes(string line)
    {
        string parsedLine = line.Replace("<NUMHORIZONTALNODES>", "");
        numHorizontalNodes = GetIntArrayFromLine(parsedLine);

    }

    void parseVerticalNodes(string line)
    {
        string parsedLine = line.Replace("<NUMVERTICALNODES>", "");
        numVerticalNodes = GetIntArrayFromLine(parsedLine);

    }

    void parseClockNodes(string line)
    {
        string parsedLine = line.Replace("<NUMCLOCKWISENODES>", "");
        numClockWiseNodes = GetIntArrayFromLine(parsedLine);

    }

    void parseCounterNodes(string line)
    {
        string parsedLine = line.Replace("<NUMCOUNTERCLOCKWISENODES>", "");
        numCounterClockWiseNodes = GetIntArrayFromLine(parsedLine);

    }

    void parseOutputPostion(string line)
    {
        string parsedLine = line.Replace("<OUTPUTPOSITiON>", "");
        string[] posString = parsedLine.Split('/');
        outputPostion = new Vector2(float.Parse(posString[0]), float.Parse(posString[1])); 
        
    }

    void parseTarget(string line)
    {
        string parsedLine = line.Replace("<TARGETTEXTURE>", "");
        targetColorValues = getStringArrayFromLine(parsedLine);
        
    }

    public void parseLine(string line)
    {
        line = line.Replace("\n", "");
        if (line.StartsWith("<INPUTLAYERSEND>"))
        {
            endInputLayer();
        }
         else if (inInputLayers)
        {
            parseInputTexture(line);
        }
        else if(line.StartsWith("<INPUTVECTOR2>"))
        {
            parseInputVector(line);
        }else if (line.StartsWith("<NUMINPUTLAYERS>")){
            parseNumInputLayers(line);
        }
        else if (line.StartsWith("<INPUTLAYERSBEGIN>"))
        {
            beginInputLayer();
        }else if (line.StartsWith("<HIDDENLAYERPOSITIONS>"))
        {
            parseHiddenPosition(line);
        }
        else if (line.StartsWith("<NUMNORMALNODES>"))
        {
            parseNormaNodes(line);
        }else if (line.StartsWith("<NUMHORIZONTALNODES>"))
        {
            parseHorizontalNodes(line);
        }
        else if (line.StartsWith("<NUMVERTICALNODES>"))
        {
            parseVerticalNodes(line);
        }
        else if (line.StartsWith("<NUMCLOCKWISENODES>"))
        {
            parseClockNodes(line);
        }
        else if (line.StartsWith("<OUTPUTPOSITiON>"))
        {
            parseOutputPostion(line);
        }
        else if (line.StartsWith("<TARGETTEXTURE>"))
        {
            parseTarget(line);
        }
    }
    public void ReadFromFile(string fileName)
    {
        inInputLayers = false; 
        
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("Assets/" + fileName + ".txt"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.LogError("Could not read file");
            Debug.LogError(e);
        }
    }


    public Level DecodeLevel()
    {
        Debug.Log("Showing Level");
        ReadFromFile("SomeOtherName");
        HiddenTemplate[] hiddentemplates = new HiddenTemplate[numNormalNodes.Length];
        for (int i = 0; i < numNormalNodes.Length; i++)
        {
            hiddentemplates[i] = new HiddenTemplate();
            hiddentemplates[i].numNormalNodes = numNormalNodes[i];
            hiddentemplates[i].numHorizontallyFlippedNodes = numHorizontalNodes[i];
            hiddentemplates[i].numVerticallyFlippedNodes = numVerticalNodes[i];
            hiddentemplates[i].numClockwiseNodes = numClockWiseNodes[i];
            hiddentemplates[i].numCounterClockNodes = numCounterClockWiseNodes[i];
        }

        Texture2D[] inputTextures = new Texture2D[inputColorValues.Length];
        for (int i = 0; i < inputColorValues.Length; i++)
        {
            inputTextures[i] = new GameTexture(inputColorValues[i]).getTex();
        }

        Texture2D targetTexture = new GameTexture(targetColorValues).getTex();
        Level level = new Level(inputTextures, inputPostion, hiddentemplates, hiddenLayerPositions, outputPostion, targetTexture);
        return level;

    }
    
}
