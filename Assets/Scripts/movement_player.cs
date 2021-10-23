using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_player : MonoBehaviour
{
    //variable
    private float kecepatan = 7f; //menampung nilai kecepatan player
    public float x; //menampung nilai input arah x
    public float z; //menampung nilai input arah y

    [SerializeField] private float gravitasi = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    public bool isGrounded;
    Vector3 velocity;

    //referensi
    private CharacterController controller;

    // Start is called before the first frame update
    //function kode pada void start hanya dipanggil pada game dimulai
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    //frame akan dipanggil secara terus menerus
    void Update()
    {
        gravity();
        bergerak();
    }

    private void bergerak()
    {
        x = Input.GetAxis("Horizontal"); //isi variabel x dengan get axis
        z = Input.GetAxis("Vertical"); //isi variabel z dengan get axis
        Vector3 gerakan = transform.right * x + transform.forward * z;
        controller.Move(gerakan * kecepatan * Time.deltaTime);
    }

    private void gravity(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        velocity.y += gravitasi * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
