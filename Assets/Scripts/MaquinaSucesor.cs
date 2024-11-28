using System;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaSucesor : MonoBehaviour
{
    List<Estado> estados;

    void Start()
    {
        Debug.Log("--------------------SUCESOR--------------");
        Debug.Log("Agregando estados");
        Estado q0 = new Estado("q0", false, true);
        Estado qf = new Estado("qf", true, false);

        Debug.Log("Agregando transiciones");
        Transicion t1 = new Transicion('0', '1', "R", q0, qf);
        Transicion t2 = new Transicion('1', '0', "L", q0, q0);
        Transicion t3 = new Transicion('-', '1', "R", q0, qf);

        Debug.Log("Vinculando estados con sus transiciones");

        estados = new List<Estado> { q0, qf };

        // Prueba con una cinta inicial de '10-'
        List<char> cinta = new List<char> { '1', '1', '0','-' };

        int punteroIndice = cinta.Count - 2;

        Estado estadoActual = q0;

        int maxPasos = 1000;
        int pasos = 0;

        while (!estadoActual.EsAceptador() && pasos < maxPasos)
        {
            pasos++;
            Debug.Log("Entró");
            char simboloLeido = cinta[punteroIndice];
            foreach (var transicion in estadoActual.GetTransicionesOrigen())
            {
                if (transicion.GetSimboloLeido() == simboloLeido)
                {
                    cinta[punteroIndice] = transicion.GetSimboloAEscribir();
                    if (transicion.GetNombreAccion() == "L")
                    {
                        Debug.Log("Moviendo a izquierda");
                        punteroIndice--;
                    }
                    else if (transicion.GetNombreAccion() == "R")
                    {
                        Debug.Log("Moviendo a derecha");
                        punteroIndice++;
                    }
                    estadoActual = transicion.GetEstadoDestino();
                    break;
                }
            }

            if (punteroIndice < 0)
            {
                cinta.Insert(0, '1');
                estadoActual = qf;
                break;
            }

            if (punteroIndice >= cinta.Count)
            {
                cinta.Add('-');
            }
        }

        // Mostrar el resultado
        Debug.Log("Resultado: ");
        string resultado = "";
        foreach (char c in cinta)
        {
            resultado += c;
        }
        Debug.Log(resultado);
        Debug.Log("-------------------- FIN SUCESOR--------------");
    }
}
