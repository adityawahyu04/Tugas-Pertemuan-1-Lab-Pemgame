using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour
{
    //variable
    [SerializeField]private float sensitivity; //sensitifitas mouse ke arah kamera

    //referensi
    private Transform parent; //ambil transform parent kamera

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent; //karena private dipanggil di void start
        Cursor.lockState = CursorLockMode.Locked; //pada game tidak akan memperlihatkan kursor
    }

    // Update is called once per frame
    void Update()
    {
        if (!HUDManager.GameIsPaused)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            parent.Rotate(Vector3.up, mouseX); //merubah rotasi kamera nilai yang diinput mouse x
        }
        
    }

    
}
