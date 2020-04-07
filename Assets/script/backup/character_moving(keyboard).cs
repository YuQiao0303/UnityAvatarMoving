using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 键盘控制版
public class character_moving : MonoBehaviour
{
    protected Animator anim;
    public float angularSpeed = 0.25f;
    public bool ApplyGravity = true;

	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    //rotate
    public float turnspeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // move position

        CharacterController controller = GetComponent<CharacterController>();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(h, 0, v);
            // Transforms direction from local space to world space.
            //moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        //controller.Move(moveDirection * Time.deltaTime);
        // rotate
        //if (h!=0 && v!= 0)
        if (true)
        {



            //transform.Rotate = new Vector3(0, h *turnspeed, 0);


            Vector3 rotateDir = new Vector3(0, h, 0);
            transform.Rotate(rotateDir * turnspeed);

            ////获取方向
            //Vector3 dir = new Vector3(h, 0, v);
            ////将方向转换为四元数
            //Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
            ////缓慢转动到目标点
            //transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.fixedDeltaTime * turnspeed);
        }

        // show animation
        if (anim)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            anim.SetFloat("Speed", h * h + v * v);
            //anim.SetFloat("Direction", h, angularSpeed, Time.deltaTime);
        }
    }
}
