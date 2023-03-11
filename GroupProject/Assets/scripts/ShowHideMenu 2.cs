using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMenu : MonoBehaviour
{
    public GameObject Menu;
    
    public void hide()
    {
        Menu.SetActive(false);
    }

    public void show()
    {
        Menu.SetActive(true);
    }
}
