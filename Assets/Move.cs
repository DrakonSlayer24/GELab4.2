using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class Move : MonoBehaviour
{
    private Rigidbody rb;
    private float movespeed;
    private float dirX, dirZ, dirY;
    public float volume = 0.5f;
    public AudioClip BreakSound;
    AudioSource audio;
    int health = 20;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 3f;
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * movespeed;
        dirZ = Input.GetAxis("Vertical") * movespeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
        }
        healthText.text = health.ToString();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX, rb.velocity.y, dirZ);
    }

    void OnCollisionEnter(Collision BoxCol)
    {
        Box box = BoxCol.gameObject.GetComponent<Box>();
        if (box)
        {
            Destroy(BoxCol.gameObject);
            audio.PlayOneShot(BreakSound, volume);
            health = health - 1;
        }
    }

}
