using UnityEngine;

public class ControladorCinta
{
    private Cinta cinta;
    private MaquinaTuring maquinaTuring;

    public ControladorCinta(Cinta cinta, MaquinaTuring maquinaTuring)
    {
        this.cinta = cinta;
        this.maquinaTuring = maquinaTuring;
    }

    public void MoverDerecha()
    {
        cinta.MoverDerecha();
        Debug.Log("Moviendo a la derecha el puntero");
    }

    public void MoverIzquierda()
    {
        cinta.MoverIzquierda();
        Debug.Log("Moviendo a la izquierda el puntero");

    }

    public void TerminarCinta()
    {
        cinta.Terminar();
        Debug.Log("Terminó la maquina");

    }
}
