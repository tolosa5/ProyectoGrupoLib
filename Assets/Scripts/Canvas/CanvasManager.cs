using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject messages;
    [SerializeField] TextsWriter textsWriterScr;
    
    GameObject playerGO;
    Player playerScr;
    
    int contador = 0;

    bool activated;

    public static CanvasManager canvasM;

    void Start()
    {
        if (canvasM == null)
        {
            canvasM = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScr = playerGO.GetComponent<Player>();

        messages.SetActive(false);
        //StartCoroutine(MessagesSpawnDespawn());
        //StartCoroutine(textsWriterScr.TextBuilder("WASD para moverse"));

    }

    // Update is called once per frame
    void Update()
    {
        /*
        no tocar si no hace falta, esto esta mal
        
        if (contador == 1 && !activated)
        {
            activated = true;
            
            StartCoroutine(MessagesSpawnDespawn());
            StartCoroutine(textsWriterScr.TextBuilder("Espacio para disparar"));
        }
        */
    }

    public void TextPicker(string text)
    {
        //hago que aparezcan x tiempo y luego desaparezcan
        MessagesSpawnDespawn();
        //llamo al txtbuilder para que se active la construccion
        StartCoroutine(textsWriterScr.TextBuilder(text));
    }

    public IEnumerator MessagesSpawnDespawn()
    {
        messages.SetActive(true);
        yield return new WaitForSeconds(3);
        messages.SetActive(false);
        contador++;
    }
}
