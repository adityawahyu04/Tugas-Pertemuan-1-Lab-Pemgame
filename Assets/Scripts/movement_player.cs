using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_player : MonoBehaviour
{
    //variable
    public float kecepatan; //diubah menjadi public supaya bisa diambil
    public float x; //menampung nilai input arah x
    public float z; //menampung nilai input arah z
    [SerializeField]private float speed_jump = 3f;
    [SerializeField]public float speed_jalan = 4f;
    [SerializeField]public float speed_lari = 8f;
     

    [SerializeField] private float gravitasi = -9.81f;
    [SerializeField] private Transform groundCheck; //untuk check object pd player
    [SerializeField] private float groundDistance = 0.4f; //untuk besaran nilai antara player dan ground
    [SerializeField] private LayerMask groundMask; //menentukan terrain
    public bool isGrounded; //menentukan player sudah menyentuh terrain
    Vector3 velocity; //buat perubahan transform object pd player

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
        lompat();
        jalan();
        
    }

    private void bergerak(){
        x = Input.GetAxis("Horizontal"); //isi variabel x dengan get axis
        z = Input.GetAxis("Vertical"); //isi variabel z dengan get axis
        Vector3 gerakan = transform.right * x + transform.forward * z;
        controller.Move(gerakan * kecepatan * Time.deltaTime);
    }

    private void gravity(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //bool dimasukkan function physics

        if (isGrounded && velocity.y < 0){ //jika bool true dan velocity < 0
            velocity.y = -2f;
        }
        
    }

    private void lompat (){
        if (Input.GetButtonDown("Jump") && isGrounded){ //ketika user menekan button spasi player akan jump pada saat sudah di tanah
            velocity.y = Mathf.Sqrt(speed_jump * -2f * gravitasi); //rumus player lompat
        }
        //menerapkan gravitasi
        velocity.y += gravitasi * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void jalan(){

        if (Input.GetKey(KeyCode.LeftShift)){ // ketika user menekan button left shift

            kecepatan = speed_lari;
        }
        else{ //jika tidak menekan button left shift
            kecepatan = speed_jalan;
        }
    }
}
