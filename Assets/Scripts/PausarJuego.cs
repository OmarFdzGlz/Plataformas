using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
	public GameObject menuPausa;
	public bool juegoPausado = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (juegoPausado)
			{
				Reanudar();
			}
			else
			{
				Pausar();
			}
		}
	}

	public void Reanudar()
	{
		menuPausa.SetActive(false);
		Time.timeScale = 1; // El timeScale es la velocidad a la que el juego se ejecuta.
		juegoPausado = false;
	}

	public void Pausar()
	{
		menuPausa.SetActive(true);
		Time.timeScale = 0; // El timeScale es la velocidad a la que el juego se ejecuta.
		juegoPausado = true;
	}

	public void SalirAlMenu()
	{
		menuPausa.SetActive(false);
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu Principal");
	}
}
