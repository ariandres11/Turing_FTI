using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class Cinta : MonoBehaviour
{
    [SerializeField] private GameObject cargandoCinta;
    [SerializeField] private GameObject maquinaTerminoTexto;
    [SerializeField] private MaquinaTuring maquina;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private GameObject contenedorSeleccion;
    [SerializeField] private int cantidadCasillerosInicial;
    [SerializeField] private GameObject prefabCasillero;
    private GameObject prefabVacio;
    [SerializeField] private string contenidoInicial;
    [SerializeField] private List<GameObject> casillerosGOS = new List<GameObject>();
    [SerializeField] private List<Casillero> casillerosSCS = new List<Casillero>();
    private Dictionary<string, Action> operaciones;
    [SerializeField] private float velSuperLenta;
    [SerializeField] private float velLenta;
    [SerializeField] private float velMedia;
    [SerializeField] private float velRapida;
    [SerializeField] private float velSuperRapida;
    [SerializeField] private GameObject contenedorVelocidades;
    private float velocidad;
    private int punteroIndice;
    private Casillero casilleroInicial;

    public void SetearContenidoInicial(string contenido)
    {
        this.contenidoInicial = contenido;
    }

    public void BotonSeleccionNumeroPresionado()
    {
        SetearContenidoInicial(this.input.text);
        Debug.Log("Seteado el contenido inicial: " + this.contenidoInicial);
        contenedorSeleccion.SetActive(false);
    }

    public void SetearVelocidad(int op)
    {
        switch (op)
        {
            case 1:
                velocidad = velSuperLenta;
                break;
            case 2:
                velocidad = velLenta;
                break;
            case 3:
                velocidad = velMedia;
                break;
            case 4:
                velocidad = velRapida;
                break;
            default:
                velocidad = velMedia;
                break;
        }
        Debug.Log("Seteada la velocidad" + velocidad);
        contenedorVelocidades.SetActive(false);

        InstanciarYSetearCasilleros();
        DefinirOperaciones();

        cargandoCinta.SetActive(true);
        StartCoroutine(CargarCintaInicial());
        EjecutarMaquina();
    }

    public void EjecutarMaquina()
    {
        cargandoCinta.SetActive(false);
        
        maquina.Inicializar(new ControladorCinta(this, maquina));
        StartCoroutine(ProcesoMaquina());
    }

    public IEnumerator ProcesoMaquina()
    {
        while (true)
        {
            maquina.IniciarMaquinaDesde(casilleroInicial);
            yield return new WaitForSeconds(velocidad);

            if (maquina.Termino())
            {
                Terminar();
                break;
            }
        }
    }

    public void DefinirOperaciones()
    {
        operaciones = new Dictionary<string, Action>
        {
            { "R", MoverDerecha },
            { "L", MoverIzquierda }
        };
    }

    public void MoverDerecha()
    {
        punteroIndice++;
        if (punteroIndice >= casillerosGOS.Count)
        {
            GameObject casilleroGO = Instantiate(prefabCasillero, new Vector3(punteroIndice * 2.0f, 0, 0), Quaternion.identity);
            casilleroGO.transform.SetParent(this.transform);
            casillerosGOS.Add(casilleroGO);
            Casillero casilleroSC = casilleroGO.GetComponentInChildren<Casillero>();
            casillerosSCS.Add(casilleroSC);
            casilleroSC.SetSimbolo('^');
        }
        Debug.Log("Mover a la derecha: " + punteroIndice);
    }

    public void MoverIzquierda()
    {
        punteroIndice--;
        if (punteroIndice < 0)
        {
            GameObject casilleroGO = Instantiate(prefabCasillero, new Vector3(punteroIndice * 2.0f, 0, 0), Quaternion.identity);
            casilleroGO.transform.SetParent(this.transform);
            casillerosGOS.Insert(0, casilleroGO);
            Casillero casilleroSC = casilleroGO.GetComponentInChildren<Casillero>();
            casillerosSCS.Insert(0, casilleroSC);
            casilleroSC.SetSimbolo('^');
            punteroIndice = 0;
        }
        Debug.Log("Mover a la izquierda: " + punteroIndice);
    }

    public IEnumerator CargarCintaInicial()
    {
        char[] caracteresContenidoInicial = contenidoInicial.ToCharArray();

        for (int i = 0; i < caracteresContenidoInicial.Length; i++)
        {
            Casillero casilleroSC = casillerosGOS[i].GetComponent<Casillero>();
            casilleroSC.ActivarPuntero();

            if (i < caracteresContenidoInicial.Length)
            {
                casilleroSC.SetSimbolo(caracteresContenidoInicial[i]);
                if (i == 0)
                {
                    casilleroInicial = casilleroSC;
                }
            }
            else
            {
                casilleroSC.SetSimbolo('^');
            }

            yield return new WaitForSeconds(velocidad);

            casilleroSC.DesactivarPuntero();
        }
    }

    public void InstanciarYSetearCasilleros()
    {
        for (int i = 0; i < cantidadCasillerosInicial; i++)
        {
            GameObject casilleroGO = Instantiate(prefabCasillero, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
            casilleroGO.transform.SetParent(this.transform);
            casillerosGOS.Add(casilleroGO);
            Casillero casilleroSC = casilleroGO.GetComponentInChildren<Casillero>();
            casillerosSCS.Add(casilleroSC);
        }

        punteroIndice = 0;

        if (casillerosSCS != null && casillerosSCS.Count > 0)
        {
            for (int i = 0; i < casillerosSCS.Count; i++)
            {
                this.casillerosSCS[i].SetSimbolo('^');
            }
        }
        else
        {
            Debug.LogWarning("La lista casillerosSCS está vacía o no ha sido inicializada.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoverDerecha();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoverIzquierda();
        }
    }

    public void Terminar()
    {
        this.maquinaTerminoTexto.SetActive(true);
    }
}
