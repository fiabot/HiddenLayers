using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTest : MonoBehaviour
{
    public Storage storage;
    public LevelDisplay display;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            display.level = storage.DecodeLevel(); 
            display.showLevel();
        }
       
    }

}
