using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Vamos a hacer que el GameManager sea un singleton, de esta manera nos aseguramos de que solo hay 1 instancia de una clase y nos permite acceder a ella  desde
    // cualquier lugar del codigo.
    public static GameManager Instance {get; private set;}
    // Necesitamos que la variable puntosTotales sea publica para poder acceder a ella desde el canvas, 
    //pero si la hacemos publica aparecera en el inspector. Por lo tanto vamos a crear una propiedad.
    public int PuntosTotales {get {return puntosTotales;}}
    public int VidasActuales { get { return vidas; } }
    public HUD hud;
    private int puntosTotales;
    private int vidas = 3;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto hace que el objeto se conserve entre escenas
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Eliminamos el duplicado al volver a entrar a la escena
        }
    }
    
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVida()
    {
        vidas -= 1;
        if(vidas == 0) // Si nos quedamos sin vidas, reiniciamos el nivel.
        {
            SceneManager.LoadScene("PantallaGameOver");
        }
        hud.DesactivarVida(vidas);
    }

    public bool RecuperarVida()
    {
        if (vidas == 3)
        {
            return false;
        }

        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hud = FindFirstObjectByType<HUD>(); // Busca el HUD en la nueva escena
        if (hud != null)
        {
            hud.ActualizarPuntos(PuntosTotales);

            for (int i = 0; i < hud.vidas.Length; i++)
            {
                hud.vidas[i].SetActive(i < vidas);
            }
        }
    }
}
