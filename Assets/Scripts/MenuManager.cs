using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public TMP_InputField inputNombre;
    public List<SaveData> jugadoresList = new List<SaveData>();

    public string jugadorPlay;

    // public ArrayJugadores jugadores;
    //public SaveData jugadorsv;
    public string nombreJugadorActual;
    public int puntuacionJugadorActual;
    public string nombreBestJugador;
    public int puntuacionBestJugador;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Debug.Log(Application.persistentDataPath + "/savefile.json");
    }

    private void Start()
    {
    }


    [System.Serializable]
    public class SaveData
    {
        public string nombreJugador;
        public int puntuacionJugador;
    }

    [System.Serializable]
    public class Jugadores
    {
        public List<SaveData> jugadores;
    }

    /*   [System.Serializable]
      public class ArrayJugadores
      {
         public List<SaveData> players;
      }
  
     public void leerJugadores()
      {
          string path = Application.persistentDataPath + "/savefile.json";
          if (File.Exists(path))
          {
              string json = File.ReadAllText(path);
              SaveData[] data = JsonUtility.FromJson<SaveData[]>(json);
  
              Debug.Log(data);
          }
      }*/

    /*public void salvarJugadores()
    {
        this.jugadores = new ArrayJugadores();
        jugadorsv = new SaveData();
        jugadorsv.nombreJugador = "Juan";
        jugadorsv.puntuacionJugador = 10;
        
        this.jugadores.players.Add(jugadorsv);

        string json = JsonUtility.ToJson(this.jugadores);
        //File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
        Debug.Log(json);
    }*/

    public void SaveJugador()
    {
        // List<SaveData> jugadores = new List<SaveData>();
        LoadJugadores();
        SaveData data = new SaveData();
        data.nombreJugador = nombreJugadorActual;
        data.puntuacionJugador = puntuacionJugadorActual;

        jugadoresList.Insert(jugadoresList.Count, data);
        //jugadoresList.Add(data);
        string json = JsonConvert.SerializeObject(jugadoresList.ToArray());
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public List<SaveData> LoadJugadores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            
            if (json.Length > 0)
            {
                var jugadores = JsonConvert.DeserializeObject<List<SaveData>>(json);
                jugadoresList.Clear(); 
                foreach (var jugador in jugadores)
                {
                    
                    jugadoresList.Add(jugador);
                }
            }
        }

        return jugadoresList;
       
    }
    
    public bool compararScore(int score1,int score2)
    {
        if (score1 > score2)
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }

    public string BestScore()
    {
        bool best = false;
        string bestScore = "";
        int mayorPuntuacion = 0;
        LoadJugadores();
        for (int i = 0; i < jugadoresList.Count; i++)
        {
            if (jugadoresList[i].puntuacionJugador > mayorPuntuacion)
            {
                bestScore = "Best Score : "+ jugadoresList[i].nombreJugador + " : " + jugadoresList[i].puntuacionJugador ;
                mayorPuntuacion = jugadoresList[i].puntuacionJugador;
            }
 
        }

        return bestScore;
    }
    
    
    
}