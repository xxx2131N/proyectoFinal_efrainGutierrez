using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cargar : MonoBehaviour
{
    public Text texto;

    private void Start()
    {
        string nivelACargar = CargarNivel.siguienteNivel;
        StartCoroutine(inciarCarga(nivelACargar));
    }    
    IEnumerator inciarCarga(string nivel)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operacion = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {

            if (operacion.progress >= 0.9f)
            {
                texto.text = "Presiona una tecla para continuar";
                if (Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}