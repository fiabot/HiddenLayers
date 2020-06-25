using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Level level;
    public LevelDisplay display;
    // Start is called before the first frame update
    void Start()
    {
        if (display != null)
        {
            display.level = level;
            display.showLevel();
        }
    }

    
}
