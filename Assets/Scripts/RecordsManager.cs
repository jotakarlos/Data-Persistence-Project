using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class RecordsManager : MonoBehaviour
{
    private List<MenuManager.SaveData> jugadores;

    public TextMeshProUGUI listajugadores;
    public TextMeshProUGUI posiciones;
    public TextMeshProUGUI puntuaciones;
    private string textoTextMesh;
    private string textoPosiciones;
    private string textoPuntuaciones;


    private void Awake()
    {
        jugadores = MenuManager.Instance.LoadJugadores();
 
        jugadores.Sort((x1, x2)=> x2.puntuacionJugador.CompareTo(x1.puntuacionJugador));
    }

    // Start is called before the first frame update
    void Start()
    {
       
        if (jugadores.Count > 0)
        {
           
            // jugadores=MenuManager.Instance.LoadJugadores();
            
            for (int i = 0; i < jugadores.Count; i++)
            {
               
                if (i < 5)
                {

                    textoPosiciones += (i + 1) + " \n";
                    textoTextMesh += jugadores[i].nombreJugador + "\n";
                    textoPuntuaciones += jugadores[i].puntuacionJugador + "\n";
                }
               
            }

            posiciones.text = textoPosiciones;
            listajugadores.text = textoTextMesh;
            puntuaciones.text = textoPuntuaciones;
            jugadores.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}