using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ManejadorEscenas: MonoBehaviour {
    public void ChangeScene(string sceneName) { 
        // Verificar si la escena está en el Build Settings 
        if (Application.CanStreamedLevelBeLoaded(sceneName)) { 
            // Cambiar a la escena especificada 
            SceneManager.LoadScene(sceneName); 
        } else { 
            Debug.LogError($"La escena '{sceneName}' no está en el Build Settings."); 
            } 
        }
}
