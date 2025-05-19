using UnityEngine.SceneManagement;
using UnityEngine;

public class PuertaANivel2 : MonoBehaviour
{
    private bool entrando = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!entrando && other.CompareTag("Player"))
        {
            entrando = true;
            SceneManager.LoadScene("Nivel 2");
        }
    }
}
