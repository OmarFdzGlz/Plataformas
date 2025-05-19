using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDerrota : MonoBehaviour
{
    public void Reintentar()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu Principal");
    }
}
