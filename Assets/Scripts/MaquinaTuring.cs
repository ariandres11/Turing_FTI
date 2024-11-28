using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Maquina Turing", menuName = "Maquinas/Maquina")]
public class MaquinaTuring : ScriptableObject
{
    [SerializeField] private string nombre;
    [SerializeField] private List<Estado> estados = new List<Estado>();
    [SerializeField] private List<Transicion> transiciones= new List<Transicion>();
    private Estado estadoActual;
    private ControladorCinta controladorCinta;

    public void ArmarMaquina()
    {
        
        /*string prefix = nombre + "_";
        estados = new List<Estado>(Resources.LoadAll<Estado>("Estados").Where(e => e.name.StartsWith(prefix)).ToList());
        List<Transicion> transiciones = new List<Transicion>(Resources.LoadAll<Transicion>("Transiciones").Where(t => t.name.StartsWith(prefix)).ToList());
        */
        // Asignar transiciones a los estados correspondientes
        // Todo: fijarse si se puede evitar el orden cuadratico
        foreach (var estado in estados)
        {
            foreach (var transicion in transiciones)
            {
                if (transicion.GetEstadoOrigen() == estado)
                {
                    estado.AgregarTransicionOrigen(transicion);
                }
                if(transicion.GetEstadoDestino() == estado){
                    estado.AgregarTransicionDestino(transicion);
                }
            }
        }

        Debug.Log("Maquina armada con " + estados.Count + " estados y " + transiciones.Count + " transiciones.");
    }

    public void Inicializar(ControladorCinta controlador)
    {
        ArmarMaquina();

        controladorCinta = controlador;
        estadoActual = estados.Find(e => e.EsInicial());
        if (estadoActual == null)
        {
            Debug.LogError("Estado inicial no encontrado.");
        }
    }

    public void IniciarMaquinaDesde(Casillero casillero)
    {
        if (estadoActual == null)
        {
            Debug.LogError("Estado inicial no encontrado.");
            return;
        }

        char simboloLeido = casillero.GetSimbolo();

        foreach (var transicion in estadoActual.GetTransicionesOrigen())
        {
            if (transicion.GetSimboloLeido() == simboloLeido)
            {
                casillero.SetSimbolo(transicion.GetSimboloAEscribir());

                if (transicion.GetNombreAccion() == "L")
                {
                    controladorCinta.MoverIzquierda();
                }
                else if (transicion.GetNombreAccion() == "R")
                {
                    controladorCinta.MoverDerecha();
                }

                estadoActual = transicion.GetEstadoDestino();
                break;
            }
        }
    }

    public bool Termino()
    {
        return estadoActual != null && estadoActual.EsAceptador();
    }
}
