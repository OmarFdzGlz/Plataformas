using UnityEngine;

public class Enemigo : MonoBehaviour
{
	public float cooldownAtaque;
	private bool puedeAtacar = true;
	private SpriteRenderer spriteRenderer;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.CompareTag("Player")) 
		{
			if(!puedeAtacar) return; // Si no puede atacar, salimos de la función.
			puedeAtacar = false; // Desactivamos el ataque
			Color color = spriteRenderer.color; // Modificamos la opacidad del enemigo para saber cuando no ataca al personaje.
			color.a = 0.5f;
			spriteRenderer.color = color;

			GameManager.Instance.PerderVida(); // Perdemos una vida.
			other.gameObject.GetComponent<Movimiento>().AplicarGolpe(); // Aplicamos el empuje al personaje.
			Invoke("ReactivarAtaque", cooldownAtaque);
		}
	}

	void ReactivarAtaque() {
		puedeAtacar = true;

		Color c = spriteRenderer.color; // Volvemos a cambiar la opacidad del enemigo para saber cuando vuelve a poder atacar al personaje.
		c.a = 1f;
		spriteRenderer.color = c;
	}
} 