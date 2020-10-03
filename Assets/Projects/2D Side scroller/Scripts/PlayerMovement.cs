using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerCharacter;
    public Rigidbody playerRB;
    public GameObject playerCharacterSprite;
    public Vector2 movementInput;
    public int speed;
    public int gravityForce;
    [Space]
    public PaperPlayer PaperPlayer;
    public Animator paperPlayerAnimator;
    [Space]
    private bool doOnce_R = true, doOnce_L = false;

    public void Awake ()
    {
      PaperPlayer = new PaperPlayer();

      // set up inputs
      PaperPlayer.Movement.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    public void FixedUpdate ()
    {
      move(movementInput);
    }



    public void move (Vector2 input)
    {
      if(input.x >= 0.1 || input.x <= -0.1)
      {
        //Vector3 target = new Vector3(playerCharacter.transform.position.x + input.x,0,0);
        //playerCharacter.transform.position = Vector3.MoveTowards(playerCharacter.transform.position, target, Time.deltaTime * speed);
        Vector3 move = new Vector3(input.x * speed,0,0);
        playerRB.velocity = move;

        paperPlayerAnimator.SetBool("isMoving",true);

        //playerCharacterSprite.transform.LookAt(target);
        if(input.x > 0)
        {
          // right
          if (!doOnce_R)
          {
            doOnce_R = true;
            doOnce_L = false;

            paperPlayerAnimator.SetTrigger("Flip");
          }
        }
        else if(input.x < 0)
        {
          // left
          if (!doOnce_L)
          {
            doOnce_L = true;
            doOnce_R = false;

            paperPlayerAnimator.SetTrigger("Flip");
          }
        }
      }
      else
      {
        paperPlayerAnimator.SetBool("isMoving",false);
        playerRB.velocity = new Vector3(0,0,0);
      }
    }

    private void OnEnable()
    {
      PaperPlayer.Enable();
    }

    private void OnDisable()
    {
      PaperPlayer.Disable();
    }

}
