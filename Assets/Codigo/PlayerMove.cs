using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float rotateSpeed = 50f;

    private Rigidbody rb;
    private float actualSpeed;
    public Animator animator;
    private bool clim = false;
    void Start()
    {
        actualSpeed = speed;
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!clim)
        {
            float rotateInput = Input.GetAxis("Horizontal");

            transform.Rotate(0.0f, rotateInput * rotateSpeed * Time.deltaTime, 0.0f);

            float moveInput = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Run");
                actualSpeed = runSpeed;
            }
            else
            {
                actualSpeed = speed;
            }

            if (moveInput != 0)
            {
                //Debug.Log("move");
                animator.SetFloat("speed", actualSpeed);
            }
            else
            {
                //Debug.Log("stopMove");
                animator.SetFloat("speed", 0);
            }

            Vector3 movement = transform.forward * moveInput * actualSpeed;


            rb.velocity = movement;
        }  
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "clim")
        {
            Debug.Log("ClimON");
            clim = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "clim")
        {
            Debug.Log("ClimOFF");
            clim = false;
        }
    }
}
