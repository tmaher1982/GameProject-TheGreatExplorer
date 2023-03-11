using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  The switch check could be moved to game manager and the 'level data' could be moved to ...somewhere?
public class LevelData : MonoBehaviour
{
    public float levelTimeLimit;
    public string nextLevel;
    [SerializeField]
    Switch[] switches;
    public List<bool> isActive;
    // Start is called before the first frame update
    void Start()
    {
        switches = FindObjectsOfType<Switch>();
        foreach(Switch s in switches)
        {
            isActive.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkSwitches()
    {
        int count = 0;
        foreach(Switch s in switches)
        {
            if(s.activated)
                count++;
        }

        if(count == switches.Length)
        {
            Debug.Log("All switches on");
            //
            GameManager.instance.UpdateGameState(GameState.LevelCompleted);
        }

    }
}
