using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health = 100;
    private PhotonView pw;
    // Start is called before the first frame update
    private void Start()
    {
        pw = GetComponent<PhotonView>();

        if (pw.IsMine )
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
            Fire();
        } 
    }

    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("Damage", RpcTarget.All, 20);
            }
        }
    }

    [PunRPC]
    private void Damage(int damageAmount)
    {
        health = Mathf.Clamp(health - damageAmount, 0, 100);
        Debug.Log("Can: " + health);
        if(health == 0)
         PhotonNetwork.Destroy(gameObject);
        
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 20;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 20;

        transform.Translate(x, 0, z);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody>().velocity = Vector3.up * 5;
    }
}
