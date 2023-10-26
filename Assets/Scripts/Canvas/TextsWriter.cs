using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextsWriter : MonoBehaviour
{
    //pillo el go, si intento el textMesh no lo coge ni idea por que
    [SerializeField] GameObject textGO;
    TextMesh text;
    float charsPerSecond = 60f;

    void Start()
    {
        //el texto = el texto que hay dentro del go que pille
        text = textGO.GetComponent<TextMesh>();
    }

    public IEnumerator TextBuilder(string message)
    {
        //para que aparezcan las letras ahi como escribiendose rollo pokemon
        text.text = "";
        foreach (char character in message)
        {
            text.text += character;
            yield return new WaitForSeconds(1/charsPerSecond);
        }
    }
}
