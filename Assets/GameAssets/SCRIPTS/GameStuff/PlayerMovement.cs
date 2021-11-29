using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player controller units
    public CharacterController controller;
    public float speed = 8f;
    Vector3 velocity;
    public float gravity = -29.4f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;

    //Random foot step stuff
    float timeSinceStep = 0;
    AudioSource audioSource;
    Vector3 move;
    [SerializeField] AudioClip footstep1;
    [SerializeField] AudioClip footstep2;
    [SerializeField] AudioClip footstep3;
    [SerializeField] AudioClip footstep4;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for player touching floor then allow jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (move.x != 0 && isGrounded)
        {
            ProcessFootsteps();
        }
    }
    //foot step random range of audio
    void ProcessFootsteps()
    {

        timeSinceStep += Time.deltaTime;
        if (timeSinceStep > 0.6)
        {
            int randomSound = Random.Range(1, 4);
            switch (randomSound)
            {
                case 1:
                    // print("playedsound1");
                    audioSource.PlayOneShot(footstep1);
                    break;
                case 2:
                    //print("playedsound2");
                    audioSource.PlayOneShot(footstep2);
                    break;
                case 3:
                    // print("playedsound3");
                    audioSource.PlayOneShot(footstep3);
                    break;
                case 4:
                    //  print("playedsound4");
                    audioSource.PlayOneShot(footstep4);
                    break;
            }
            timeSinceStep = 0;
        }

    }
}
