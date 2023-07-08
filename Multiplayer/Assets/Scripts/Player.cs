using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private int health = 100;
    private PhotonView pw;
    // Start is called before the first frame update
    private void Start()
    {
        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (pw.IsMine)
        {
            Move();
            Jump();
            Rotation();
            Fire();
        }
    }

    private void Rotation()
    {
        float x = Input.GetAxis("Horizontal");
        if (x < 0)
        {
            transform.Rotate(Vector3.up * -.5f);
        }

        if (x > 0)
        {
            transform.Rotate(Vector3.up * .5f);
        }
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("Damage", RpcTarget.All, 20);
            }
        }
    }

    [PunRPC]
    private void Damage(int damageAmount)
    {
        health = Mathf.Clamp(health - damageAmount, 0, 100);
        Debug.Log("Can: " + health);
        if (health == 0)
            GetComponent<Player>().enabled = false;


    }

    private void Move()
    {

        float z = Input.GetAxis("Vertical") * Time.deltaTime * 5;

        transform.Translate(0, 0, z);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody>().velocity = Vector3.up * 5;
    }
}
