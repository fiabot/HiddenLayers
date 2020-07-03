using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveNodeToPNG : MonoBehaviour
{
    public NodeDisplay NodeToSave;
    public string fileName; 
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("Saving node");
            Node node = NodeToSave.node;
            Texture2D tex = node.GetTexture2D();
            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();

            // For testing purposes, also write to a file in the project folder
            File.WriteAllBytes(Application.dataPath + "/../Assets/Image Saves/" + fileName+ ".png", bytes);
            Debug.Log("Node saved");

        }
        
    }

    public static void save(Node node, string newFileName)
    {
        Texture2D tex = node.GetTexture2D();
        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/../Assets/Image Saves/" + newFileName + ".png", bytes);
        Debug.Log("Node saved");
    }
}
