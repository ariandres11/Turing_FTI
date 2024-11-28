using System;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaAntecesor : MonoBehaviour
{
    List<Estado> estados;

    void Start()
    {
        Debug.Log("--------------------ANTECESOR--------------");
        Debug.Log("Agregando estados");
        Estado q0 = new Estado("q0", false, true); // Estado inicial
        Estado q1 = new Estado("q1", false, false); // Estado para manejar el acarreo a la izquierda
        Estado q2 = new Estado("q2", true, false);  // Estado aceptador

        Debug.Log("Agregando transiciones");

        // Transiciones para recorrer el número hacia la derecha
        Transicion t1 = new Transicion('1', '1', "R", q0, q0);
        Transicion t2 = new Transicion('0', '0', "R", q0, q0);
        Transicion t3 = new Transicion('-', '-', "L", q0, q1);

        // Transiciones para decrementar el número binario (al regresar)
        Transicion t4 = new Transicion('1', '0', "S", q1, q2);
        Transicion t5 = new Transicion('0', '1', "L", q1, q1);
        Transicion t6 = new Transicion('-', '-', "S", q1, q2);

        Debug.Log("Vinculando estados con transiciones");

        estados = new List<Estado> { q0, q1, q2 };

        // Crear la cinta para el caso específico
        List<char> cinta = new List<char> { '1', '1', '0', '-' }; // Cambia la cinta aquí para probar otros casos

        EjecutarMaquina(cinta);
    }

    void EjecutarMaquina(List<char> cinta)
    {
        int punteroIndice = 1; // Posición inicial del puntero antes del último '-'
        Estado estadoActual = estados.Find(e => e.EsInicial());

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
                    else if (transicion.GetNombreAccion() == "S")
                    {
                        Debug.Log("Puntero permanece en su posición");
                    }
                    estadoActual = transicion.GetEstadoDestino();
                    break;
                }
            }

            // Si el puntero se mueve más allá del inicio de la cinta
            if (punteroIndice < 0)
            {
                punteroIndice = 0; // Ajustar puntero a la primera posición válida
                estadoActual = estados.Find(e => e.EsAceptador()); // Cambiar al estado final
            }
        }

        // Remover ceros iniciales innecesarios
        while (cinta.Count > 1 && cinta[0] == '0')
        {
            cinta.RemoveAt(0);
        }

        // Mostrar el resultado
        Debug.Log("Resultado: ");
        string resultado = "";
        foreach (char c in cinta)
        {
            resultado += c;
        }
        Debug.Log(resultado);
        Debug.Log("-------------------- FIN ANTECESOR--------------");
    }
}
