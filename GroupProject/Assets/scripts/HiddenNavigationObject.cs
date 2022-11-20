using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenNavigationObject : MonoBehaviour
{
    // For objects to be considered for the NavMesh bake they need to have mesh renderer
    // I'd like to see them in the editor but not in the game
    
   private void Awake() 
   {
        gameObject.SetActive(false);
   }
}
