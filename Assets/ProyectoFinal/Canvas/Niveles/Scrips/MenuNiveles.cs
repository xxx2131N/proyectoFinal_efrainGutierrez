using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNiveles : MonoBehaviour
{
    public int numeroAtras;



    public void Atras()
    {
        SceneManager.LoadScene(numeroAtras);
    }
}
