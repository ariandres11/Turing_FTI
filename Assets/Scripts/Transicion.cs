using UnityEngine;
using System.Collections;


public class Transicion
{
    [SerializeField] private char simboloLeido;
    [SerializeField] private char simboloAEscribir;
    [SerializeField] private string nombreAccion;
    [SerializeField] private Estado estadoOrigen;
    [SerializeField] private Estado estadoDestino;

    public Transicion (char simboloLeido, char simboloAEscribir, string nombreAccion, Estado estadoOrigen, Estado estadoDestino){
        this.simboloLeido = simboloLeido;
        this.simboloAEscribir = simboloAEscribir;
        this.nombreAccion = nombreAccion;
        this.estadoOrigen = estadoOrigen;
        this.estadoOrigen.AgregarTransicionOrigen(this);
        this.estadoDestino = estadoDestino;
        this.estadoDestino.AgregarTransicionDestino(this);
    }

    //Getters 
    public char GetSimboloLeido(){
        return this.simboloLeido;
    }

    public char GetSimboloAEscribir(){
        return this.simboloAEscribir;
    }

    public string GetNombreAccion(){
        return this.nombreAccion;
    }

    public Estado GetEstadoOrigen(){
        return this.estadoOrigen;
    }

    public Estado GetEstadoDestino(){
        return this.estadoDestino;
    }

    public override string ToString() { return $"Transicion - Leo: {simboloLeido}, Escribo: {simboloAEscribir}, Nombre Accion: {nombreAccion}, Estado Origen: {estadoOrigen.GetNombre()}, Estado Destino: {estadoDestino.GetNombre()}"; }
}