using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerDamn : MonoBehaviour
{
    public float mouseSensitivity = 100;
    public float speed;
    public int damages;

    public AnimationCurve JumpCurve;
    private float JumpTime = 1;

    private CharacterController charaCTRL;
    private Transform mCamera;
    private float xRotation = 0f;

    void Start()
    {
        charaCTRL = GetComponent<CharacterController>();
        mCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Move();
        Jump();
        Shoot();
    }
    public void Move()
    {
        float RuningBonus = (Input.GetKey(KeyCode.LeftShift)) ? 2 : 1;
        float x = (Input.GetAxis("Horizontal") * speed * Time.deltaTime * RuningBonus);
        float z = (Input.GetAxis("Vertical") * speed * Time.deltaTime * RuningBonus);

        Vector3 direction = transform.right * x + transform.forward * z;

        charaCTRL.Move(direction);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);
        mCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }
    public void Jump()
    {
        #region ground Check
        bool OnGround = Physics.CheckBox(new Vector3(transform.position.x, transform.position.y - 1.15f, transform.position.z), new Vector3(1f, 0.3f, 1f) / 2);
        #endregion

        #region Simple Jump
        JumpTime = (Input.GetKeyDown("space") && OnGround == true) ? 0 : JumpTime;
        JumpTime = Mathf.Clamp(JumpTime + Time.deltaTime, 0, 1);
        charaCTRL.Move(new Vector3(0, JumpCurve.Evaluate(JumpTime) * Time.deltaTime, 0));
        #endregion

    }
    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(mCamera.transform.position, mCamera.transform.forward, out hit) && Input.GetButtonDown("Fire1"))
        {
            
            IADamnWraith w = hit.transform.GetComponent<IADamnWraith>();
            IADamnDemon d = hit.transform.GetComponent<IADamnDemon>();
                w.takeDamages(damages);

            if (w != null)
            {

            }
            else if (d != null)
            {
                //d.takeDamages(damages);
            }
            
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - 1.15f, transform.position.z), new Vector3(1f, 0.3f, 1f));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
    }


}
