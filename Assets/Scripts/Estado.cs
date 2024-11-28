using UnityEngine;
using System.Collections.Generic;

public class Estado{
    [SerializeField] private string nombre;
    [SerializeField] private bool aceptador;
    [SerializeField] private bool inicial;
    [SerializeField] private List<Transicion> transicionesOrigen = new List<Transicion>{};
    [SerializeField] private List<Transicion> transicionesDestino = new List<Transicion>{};

    public Estado(string nombre, bool aceptador, bool inicial){
        this.nombre = nombre;
        this.aceptador = aceptador;
        this.inicial = inicial;
        this.transicionesDestino = new List<Transicion>{};
        this.transicionesOrigen = new List<Transicion>{};
    }

    public bool EsAceptador(){
        return aceptador;
    }

    public bool EsInicial(){
        return inicial;
    }

    public Transicion AgregarTransicionOrigen(Transicion transicion){
        if(this.transicionesOrigen.Contains(transicion)){
            return null;
        }
        this.transicionesOrigen.Add(transicion);
        return transicion;
    }

    public Transicion AgregarTransicionDestino(Transicion transicion){
        if(this.transicionesDestino.Contains(transicion)){
            return null;
        }
        this.transicionesDestino.Add(transicion);
        return transicion;
    }

    public List<Transicion> GetTransicionesOrigen(){
        return this.transicionesOrigen;
    }

    public List<Transicion> GetTransicionesDestino(){
        return this.transicionesDestino;
    }
    public string GetNombre(){
        return nombre;
    }
    
    public override string ToString() { return $"Estado: {nombre}, Aceptador: {aceptador}, Inicial: {inicial}"; }
}