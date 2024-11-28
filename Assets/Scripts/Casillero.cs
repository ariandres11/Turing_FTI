using System;
using UnityEngine;
using TMPro;

public class Casillero : MonoBehaviour 
{
    [SerializeField] private char contenido;
    [SerializeField] private TextMeshProUGUI textoHijo;
    [SerializeField] private GameObject punteroGO;

    public void Start(){
        //textoHijo = GetComponentInChildren<TextMeshProUGUI>();
        //El puntero empieza siendo invisible para el usuario
        punteroGO.SetActive(false);
    }
    public void SetSimbolo(char simbolo){
        if(simbolo == 'V'){
            this.contenido = '^';
        }else{
            this.contenido = simbolo;
        }
        textoHijo.text = contenido.ToString();
    }

    public char GetSimbolo(){
        return this.contenido;
    }

    public void ActivarPuntero(){
        punteroGO.SetActive(true);
    }

    public void DesactivarPuntero(){
        punteroGO.SetActive(false);
    }

    public void TogglePuntero(){
        punteroGO.SetActive(!punteroGO.activeSelf);
    }
}