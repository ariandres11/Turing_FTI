using UnityEngine;
using System.Collections;
using TMPro;
public class EjecutarMaquina : MonoBehaviour 
{
    SimulationManager simulationManager = SimulationManager.instance;
    [SerializeField] GameObject panelTextoPruebaHashtables;

    void Start(){
        //Ir escribiendo todos los estados y transiciones en el panel
        TextMeshProUGUI textMeshPro = panelTextoPruebaHashtables.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = "\n Transiciones";
        // Recorrer las transiciones en la hashtable 
        foreach (DictionaryEntry entry in simulationManager.GetTransiciones()) { 
            Transicion t = (Transicion)entry.Value; 
            textMeshPro.text += t.ToString() + "\n"; 
        } 
        textMeshPro.text += "\nEstados\n"; 
        // Recorrer los estados en la hashtable 
        foreach (DictionaryEntry entry in simulationManager.GetEstados()) { 
            Estado e = (Estado)entry.Value; 
            textMeshPro.text += e.ToString() + "\n"; 
        }
    }
}
