using UnityEngine;
using System.Collections;


public class Movimiento : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public float fuerzaGolpe;
    public int saltosMaximos;
    public LayerMask capaSuelo;
    public AudioClip sonidoSalto;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private int saltosRestantes;
    private Animator animator;
    private bool puedeMoverse = true;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GestionarMovimiento();
        GestionarSalto();
    }

    bool EstaEnSuelo()
    {
        Vector2 origen = new Vector2(transform.position.x, transform.position.y - boxCollider.bounds.extents.y - 0.05f); // Punto de origen ligeramente por debajo del centro del personaje      
        float distancia = 0.1f; // Raycast corto hacia abajo      
        RaycastHit2D hit = Physics2D.Raycast(origen, Vector2.down, distancia, capaSuelo); // Solo colisiona con capaSuelo
        Debug.DrawRay(origen, Vector2.down * distancia, Color.red); // Para depuración visual

        return hit.collider != null;
    }


    void GestionarSalto()
    {
        if (EstaEnSuelo())
        {
            saltosRestantes = saltosMaximos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0f);
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);
        }
    }

    void GestionarMovimiento()
    {
        // Si no puede moverse, salimos de la funcion
        if (puedeMoverse == false)
        {
            return;
        }

        //Logica de movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rigidBody.linearVelocity = new Vector2(inputMovimiento * velocidad, rigidBody.linearVelocity.y);
        GestionarOrientacion(inputMovimiento);
    }


    void GestionarOrientacion(float inputMovimiento)
    {
        // Si se cumple la condición, ejecutamos el código para voltear al personaje
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void AplicarGolpe()
    {
        puedeMoverse = false;
        Vector2 direccionGolpe;
        if (rigidBody.linearVelocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }
        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(EsperarYActivarMovimiento());
    }

    // Creamos una corrutina para poder ejecutar codigo a lo largo de varios frames, de esta manera podremos comprobar si el personaje vuelve a tocar el suelo en 
    // los frames posteriores al golpe del enemigo.
    IEnumerator EsperarYActivarMovimiento()
    {
        // Esperamos una décima de segundo para evitar que el movimiento se reactive al recibir el golpe, ya que en ese momento el personaje aún está en el suelo.
        yield return new WaitForSeconds(0.1f);
        while (!EstaEnSuelo())
        {
            yield return null;
        }
        puedeMoverse = true;
    }
}
