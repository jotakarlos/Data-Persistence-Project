using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    
    private void Awake()
    {
        GameObject.Find("NombrePlayer").GetComponent<TMP_InputField>().text = MenuManager.Instance.jugadorPlay;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void StartNew()
    {
        MenuManager.Instance.jugadorPlay = GameObject.Find("NombrePlayer").GetComponent<TMP_InputField>().text;
        //Debug.Log("Nombre usuario: " + MenuManager.Instance.jugador);
        SceneManager.LoadScene(1);
    }

    public void Records()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
#endif
    }
}
