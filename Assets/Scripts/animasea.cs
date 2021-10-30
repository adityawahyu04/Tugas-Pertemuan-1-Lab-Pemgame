using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animasea : MonoBehaviour
{
    //variable
    // private float kecepatan_player;
    private float nilai_x;
    private float nilai_z;
    private bool status_ground;

    //referensi
    private Animator anim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>(); //mau mengambil animator yang sudah dipasang object 
        player = GameObject.Find("Player"); //mau mengambil game object player yaitu mengambil pada script lain
    }

    // Update is called once per frame
    void Update()
    {
        // kecepatan_player = player.GetComponent<movement_player>().kecepatan;
        nilai_x = player.GetComponent<movement_player>().x;
        nilai_z = player.GetComponent<movement_player>().z;
        status_ground = player.GetComponent<movement_player>().isGrounded;
        anim.SetFloat("x", nilai_x);
        anim.SetFloat("z", nilai_z);
        anim.SetBool("isGrounded", status_ground);
    }
}
