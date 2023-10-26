using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;

    [SerializeField] Canvas canvas;

    public Vector3 newPlayerPosition;

    Animator canvasAnim;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        if (gM == null)
        {
            gM = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        //para hacer una cortinilla(?
        canvasAnim = canvas.gameObject.GetComponent<Animator>();
    }

    void OnSceneLoaded(Scene escenaCargada, LoadSceneMode modoDeCarga)
    {
        //esta nuevaPosicion se rellena con la que el GameManager pilla de la entrada
        //al entrar en ella
        Player.player.transform.position = newPlayerPosition;

        //se tira la cortinilla si es que la metemos
        canvasAnim = GameObject.FindGameObjectWithTag("Cortinilla").GetComponent<Animator>();
        Player.player.gameObject.SetActive(true);
    }
}
