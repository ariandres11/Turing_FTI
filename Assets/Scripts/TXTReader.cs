using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;
public class TXTReader : MonoBehaviour{
    [SerializeField] private SimulationManager simulationManager;
    public void Inicializar() {
        OpenFileBrowser("estados"); 
        OpenFileBrowser("transiciones");
        ImprimirTablas();
    }
    void OpenFileBrowser(string elementoAPedir) { 
        // Permitir seleccionar solo un archivo 
        var paths = StandaloneFileBrowser.OpenFilePanel("Selecciona un archivo de " + elementoAPedir, "", "txt", false); 
        if (paths.Length > 0) { 
           ReadFile(paths[0], elementoAPedir); 
        } else { 
            Debug.LogError("No se seleccionó ningún archivo."); 
        } 
    }
    void ReadFile(string filePath, string elemento) { 
        if (File.Exists(filePath)) { 
            var lines = File.ReadAllLines(filePath); 
            switch (elemento) { 
                case "estados": 
                    // Leer estados 
                    foreach (var line in lines) { 
                        var values = line.Split(';'); 
                        // Asumiendo que el TXT tiene tres columnas 
                        string nombre = values[0]; 
                        bool aceptador = bool.Parse(values[1]); 
                        bool inicial = bool.Parse(values[2]);
                        Debug.Log($"Archivo de estados: {Path.GetFileName(filePath)} - Nombre: {nombre}, Aceptador: {aceptador}, Inicial: {inicial}"); 
                        //Crear instancia de estado y agregarla a la list
                        simulationManager.AddEstado(new Estado(nombre, aceptador, inicial));
                        }
                    break; 
                case "transiciones": // Leer transiciones 
                    Hashtable tablaEstados = simulationManager.GetEstados();
                    foreach (var line in lines) { 
                        var values = line.Split(';'); 
                        // Asumiendo que el TXT tiene cinco columnas 
                        char leo = char.Parse(values[0]); 
                        char escribo = char.Parse(values[1]);
                        string nombreAccion = values[2]; 
                        string nombreEstadoOrigen = values[3]; 
                        string nombreEstadoDestino = values[4]; 
                        Estado estadoOrigen = (Estado) tablaEstados[nombreEstadoOrigen];
                        Estado estadoDestino = (Estado) tablaEstados[nombreEstadoDestino];
                        simulationManager.AddTransicion(new Transicion(leo,escribo,nombreAccion, estadoOrigen, estadoDestino));

                        //tablaTransiciones.Add("t" + i, new Transicion(leo,escribo,nombreAccion, estadoOrigen, estadoDestino));
                        Debug.Log($"Archivo de transiciones: {Path.GetFileName(filePath)} - Leo: {leo}, Escribo: {escribo}, Nombre Accion: {nombreAccion}, Nombre Estado Origen: {nombreEstadoOrigen}, Nombre Estado Destino: {nombreEstadoDestino}"); 
                    } 
                    break; 
                default: 
                    Debug.LogError("Error en TXTReader: elemento no reconocido."); 
                    break; 
            } 
        } else { 
            Debug.LogError($"El archivo {filePath} no existe."); 
        } 
    }

    void ImprimirTablas(){

        PrintHashtable(simulationManager.GetEstados());
        PrintHashtable(simulationManager.GetTransiciones());
    }

    public static void PrintHashtable(Hashtable hashtable) { 
        foreach (DictionaryEntry entry in hashtable) { 
        Debug.Log($"Clave: {entry.Key}, Valor: {entry.Value}"); 
        } 
    }
}