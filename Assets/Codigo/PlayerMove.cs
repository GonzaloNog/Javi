using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float velocidadRotacion = 100.0f;
    public float fuerzaGravedad = 9.81f;
    private bool enSuelo = false;

    private float actualSpeed;
    public Animator animator;
    private bool clim = false;
    private GameObject targetClim;
    void Start()
    {
        actualSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!clim)
        {
            float movimientoVertical = Input.GetAxis("Vertical");
            float movimientoHorizontal = Input.GetAxis("Horizontal");
            // Verificar si el personaje está en el suelo usando un Raycast
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Run");
                actualSpeed = runSpeed;
            }
            else
            {
                actualSpeed = speed;
            }

            if (movimientoVertical != 0)
            {
                //Debug.Log("move");
                animator.SetFloat("speed", actualSpeed);
            }
            else
            {
                //Debug.Log("stopMove");
                animator.SetFloat("speed", 0);
            }


            Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical) * actualSpeed * Time.deltaTime;

            // Mover al personaje en la dirección calculada
            transform.Translate(movimiento);

            // Rotar al personaje si hay movimiento lateral
            if (movimientoHorizontal != 0)
            {
                float rotationAmount = movimientoHorizontal * velocidadRotacion * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
            }

            if (movimientoVertical != 0 || movimientoHorizontal != 0)
            {
                // Si hay movimiento, verificar si el personaje está en el suelo
                enSuelo = false;
            }
        }
        else
        {
            Debug.Log("escalera");
            float moveInput = Input.GetAxis("Vertical");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + ((speed * Time.deltaTime) * moveInput), this.transform.position.z);
            this.transform.rotation = targetClim.transform.rotation;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "clim")
        {
            if (!clim)
            {
                Debug.Log("ClimON");
                clim = true;
                targetClim = collision.gameObject;
                animator.SetBool("Climbing", true);
                //Component comp = rb;
               //comp.
            }
            else
            {
                Debug.Log("ClimOFF");
                clim = false;
                animator.SetBool("Climbing", false);
            }

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Verificar si el personaje ya no está en contacto con el objeto etiquetado como "Grounded"
        if (collision.gameObject.CompareTag("Grounded"))
        {
            enSuelo = false;
        }
    }

    void FixedUpdate()
    {
        // Verificar si el personaje está en el suelo
        if (!enSuelo && !clim)
        {
            // Aplicar una fuerza hacia abajo simulando la gravedad
            ApplyGravity();
        }
    }
    void ApplyGravity()
    {
        // Aplicar una fuerza hacia abajo simulando la gravedad
        Vector3 fuerzaGravitacional = Vector3.down * fuerzaGravedad * Time.deltaTime;
        transform.position += fuerzaGravitacional;
    }
}
