using UnityEngine; 
using System.Collections.Generic; 
using System.Collections; 
public class SimulationManager : MonoBehaviour { 
    //Se encarga de persistir la info entre escenas 
    public static SimulationManager instance; 
    private Hashtable tablaEstados; 
    private Hashtable tablaTransiciones; 
    private int indice = 0; 
    void Awake() { 
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(gameObject); 
            tablaEstados = new Hashtable(); 
            tablaTransiciones = new Hashtable(); 
        } else { 
            Destroy(gameObject); 
        } 
    } 
    public void AddEstado(Estado estado) { 
        tablaEstados.Add(estado.GetNombre(), estado); 
    } 
        
    public void AddTransicion(Transicion transicion) { 
        tablaTransiciones.Add("t" + indice, transicion); 
        indice++; 
    } 
        
    public Hashtable GetEstados() { 
        return tablaEstados; 
    } 
    public Hashtable GetTransiciones() { 
        return tablaTransiciones; 
    } 
}