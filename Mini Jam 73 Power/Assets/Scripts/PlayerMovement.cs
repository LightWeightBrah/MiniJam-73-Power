using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform gunToSpawnTransform;

    [SerializeField] SpriteRenderer bodySr;
    public SpriteRenderer gunSr;

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Camera cam;

    Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePos;

    Animator animator;
    string currentAnimationState;

    const string playerIdle = "Player_idle";
    const string playerMove = "Player_Move";

    [SerializeField] Gun defaultGun = null;

    Gun currentGun = null;

    public bool isPaused;


    void Start()
    {
        if(currentGun == null)
        {
            EquipGun(defaultGun);
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPaused) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        if(movement != Vector2.zero)
        {
            ChangeAnimationState(playerMove);
        }
        else
        {
            ChangeAnimationState(playerIdle);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            bodySr.flipX = false;
            gunSr.flipY = false;
        }
        else
        {
            bodySr.flipX = true;
            gunSr.flipY = true;
        }
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        //rb.MovePosition => moves rigidbody to a position
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 gunPos = new Vector2(gunToSpawnTransform.position.x, gunToSpawnTransform.position.y);

        Vector2 lookDir = gunPos - mousePos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        gunToSpawnTransform.eulerAngles = new Vector3(gunToSpawnTransform.eulerAngles.x, gunToSpawnTransform.eulerAngles.y, angle);

    }

    public void EquipGun(Gun weapon)
    {
        currentGun = weapon;
        weapon.Spawn(gunToSpawnTransform);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;

        animator.Play(newState);

        currentAnimationState = newState;
    }
}
