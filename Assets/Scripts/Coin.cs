using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 1;
    public AudioClip sonidoMoneda;


    // Esto hace que solo el player pueda recoger las monedas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detectado con: " + collision.name);
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoMoneda);
        }
    }
}
