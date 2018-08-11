using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 8f;
    public float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerMoveKeyboad();
	}

    void PlayerMoveKeyboad()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            if (vel < maxVelocity)
            {
                forceX = speed;
                anim.SetBool("isWalking", true);
                Vector3 temp = transform.localScale;
                if (temp.x < 0f)
                {
                    temp.x = temp.x * -1;
                    transform.localScale = temp;
                }
            }
        }
        else if (h < 0)
        {
            if (vel < maxVelocity)
            {
                forceX = -speed;
                anim.SetBool("isWalking", true);
                Vector3 temp = transform.localScale;
                if (temp.x > 0f)
                {
                    temp.x = temp.x * -1;
                    transform.localScale = temp;
                }
            }

        } else
        {
            anim.SetBool("isWalking", false);
        }
            myBody.AddForce(new Vector2(forceX, 0f));
        
    }




}
