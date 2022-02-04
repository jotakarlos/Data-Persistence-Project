using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    public Button btnVolver;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
   

    
    // Start is called before the first frame update
    void Start()
    {
        //MenuManager.Instance.LoadJugador();
        if (MenuManager.Instance.puntuacionJugadorActual > 0)
        { 
            //BestScoreText.text = "Best Score : "+ MenuManager.Instance.nombreMejorJugador + " : " + MenuManager.Instance.mejorPuntuacion ;
        }

        BestScoreText.text = MenuManager.Instance.BestScore();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        btnVolver.gameObject.SetActive(true);
       // if (m_Points > MenuManager.Instance.mejorPuntuacion)
       // {
            UpdateScore();
            MenuManager.Instance.BestScore();
       // }
       
    }

    private void UpdateScore()
    {
        bool mayor=false;
        MenuManager.Instance.nombreJugadorActual = MenuManager.Instance.jugadorPlay;
        MenuManager.Instance.puntuacionJugadorActual = m_Points;

        var jugadores = MenuManager.Instance.LoadJugadores();

        if (jugadores.Count > 0)
        {
             for (int i = 0; i < jugadores.Count; i++)
                    {
                        mayor = MenuManager.Instance.compararScore(m_Points,jugadores[i].puntuacionJugador );
                        if (mayor)
                        {
                            break;
                        }
                    }
            
                    if (mayor)
                    {
                        MenuManager.Instance.SaveJugador();
                         //BestScoreText.text = "Best Score : "+ MenuManager.Instance.nombreJugadorActual + " : " + MenuManager.Instance.puntuacionJugadorActual ;
                    }
        }
        else
        {
            MenuManager.Instance.SaveJugador();
        }
        
       

        MenuManager.Instance.BestScore();


    }

    /*private bool compararScore(int score1,int score2)
    {
        if (score2 > score1)
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }*/

}
