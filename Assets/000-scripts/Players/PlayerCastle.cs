using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pStates {Alive , Pause, Dead};
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerCastle : MonoBehaviour,IDamageable
{
    public bool b;
    [Space(20)]
    public pStates curPlayerState;

    [Space(20)]
    public float health = 20;
    public float souls = 0;
    public float healthPenality = 2;
    public float soulsPenality = 1000;

    [Space(20)]
    public float Speed;
    public float Acceleration;
    public float Deceleration;

    [Space(20)]
    public AnimationCurve AccelerationCurve;

    [Space(20)]
    public AnimationCurve JumpingCurve;

    private float direction = 1;
    private float MoveTime = 0;
    private float JumpTime = 1;

    private CharacterController charaCTRL;
    private BoxCollider boxCol;
    private Animator anim;

    public void Start()
    {
        charaCTRL = GetComponent<CharacterController>();
        boxCol = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        if (curPlayerState == pStates.Alive)
        {
            Move();
            Jump();
            Attack();
            Pause();
        }
        if(curPlayerState == pStates.Dead)
        {

        }
        if(curPlayerState == pStates.Pause)
        {
            Pause();
        }
    }

    public void Move()
    {
        MoveTime = (Input.GetAxisRaw("Horizontal") != 0) ? Mathf.Clamp(MoveTime + Time.deltaTime * Acceleration, 0 , 1) : Mathf.Clamp(MoveTime - Time.deltaTime * Deceleration, 0, 1);

        direction = (Input.GetButton("Horizontal")) ? (Input.GetAxisRaw("Horizontal")) : direction;

        Vector3 horizontalSpeed = new Vector3(AccelerationCurve.Evaluate(MoveTime) * Speed * direction * Time.deltaTime, 0, 0);

        transform.rotation =  Quaternion.Euler (0, 180 + 90 * -direction, 0);

        charaCTRL.Move(horizontalSpeed);

        MoveTime = (charaCTRL.velocity.x == 0 && MoveTime > 0.2f) ? 0 : MoveTime;

    }
    public void Jump()
    {
        bool OnGround = Physics.CheckBox(new Vector3(transform.position.x, transform.position.y - 0.16f, transform.position.z), new Vector3(1f, 0.3f, 1f) / 2);
        b = OnGround;

        JumpTime = (Input.GetButtonDown("Jump") && OnGround) ? 0 : JumpTime;
        JumpTime = Mathf.Clamp(JumpTime + Time.deltaTime, 0, 1);


        Vector3 VerticalSpeed = new Vector3(0, JumpingCurve.Evaluate(JumpTime) * Time.deltaTime, 0);

        charaCTRL.Move(VerticalSpeed);
        Debug.Log(OnGround);

    }
    public void Attack()
    {
        boxCol.enabled = true;
    }
    public void EndAttack()
    {
        boxCol.enabled = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {

        }
        EndAttack();
    }

    public void Steal()
    {
        souls -= soulsPenality;

    }
    public void Damage()
    {
        health = Mathf.Clamp(health - healthPenality, 0, 20);
        if (health == 0)
        {
            Death();
        }
    }
    public void Death()
    {
            curPlayerState = pStates.Dead;
    }


    public void Pause()
    { 
        curPlayerState = (Input.GetKeyDown(KeyCode.Escape) && curPlayerState == pStates.Alive) ? pStates.Pause : pStates.Alive;
        Time.timeScale = (Input.GetKeyDown(KeyCode.Escape)) ? -Time.timeScale + 1 : Time.timeScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y -0.15f, transform.position.z), new Vector3(1f, 0.3f, 1f));
    }
}
