using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;


    // Update is called once per frame
    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    public void DesactivarVida(int indice) 
    {
        vidas[indice].SetActive(false);
    }

    public void ActivarVida(int indice) 
    {
        vidas[indice].SetActive(true);
    }

    public void SincronizarVidas(int vidasActuales)
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(i < vidasActuales);
        }
    }

}
