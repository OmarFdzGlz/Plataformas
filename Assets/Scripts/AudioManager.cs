using UnityEngine;

// Ademas de tener el script AudioManager tambien vamos a necesitar un AudioSource para reproducir los sonidos, por lo tanto vamos a usar un atributo para
// indicarle a unity que cada vez que añadamos este script a un GameObject se cree un componente AudioSource automaticamente.
[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.Log("Atencion! Más de un AudioManager en escena.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
