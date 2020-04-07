using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 鼠标控制，功能完美，尚未整理参数和不必要的代码
public class character_moving : MonoBehaviour
{
    protected Animator anim;
    //主摄像机对象
    private Camera camera;
    // mouse control
    private RaycastHit hit;
    private Ray ray;

	public float speed = 1F;
    public float gravity = 200.0F;
    public float randomness = 0.0F;

    //monitor
    public Vector3 randVec;
    public Vector3 target;
    public bool isGrounded;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        CharacterController controller = GetComponent<CharacterController>();

        target = new Vector3();
        bool isOver = false;
        Vector3 moveDirection = new Vector3();
        distance = Vector3.Distance(target, transform.position);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//摄像机发射射线到屏幕点。
        if (Physics.Raycast(ray, out hit))//射线发出并碰撞到外壳，那么手臂就应该朝向碰撞点
        {
            // calculage target position
            target = hit.point;
            //target.y = 0;
            target.y = transform.position.y;

            //// add some randomness
            //randVec = new Vector3(Random.Range(0f, randomness), 0, Random.Range(0f, randomness));
            //target = target + randVec;

            // rotate
            transform.LookAt(target);


            // move position
            if (controller.isGrounded)
            {
                isGrounded = true;
                if (!isOver)
                {    
                    if (distance <= 0.3f)
                    {
                        isOver = true;
                        transform.position = target;
                        moveDirection.x = 0.0f;
                        moveDirection.y = 0.0f;
                        moveDirection.z = 0.0f;
                    }
                    else
                    {
                        moveDirection = target - transform.position;
                        moveDirection *= speed;
                    }
                }
            }
            else
                isGrounded = false;
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
            //controller.SimpleMove(moveDirection * Time.deltaTime);
        }
        

        // show animation
        if (anim)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (distance > 0.3f)
                anim.SetFloat("Speed", 1.0f);
            else
                anim.SetFloat("Speed", -0.1f);
        }
    }

    void OnGUI()
    {
        float modelHeight = 1.55f;
        string text = "36.7℃";
        //得到NPC头顶在3D世界中的坐标
        //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + modelHeight, transform.position.z);
        //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
        Vector2 position = camera.WorldToScreenPoint(worldPosition);
        //得到真实NPC头顶的2D坐标
        position = new Vector2(position.x, Screen.height - position.y);
     
        //计算NPC名称的宽高
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(text));
        //设置显示颜色为黄色
        GUI.color = Color.yellow;
        //绘制NPC名称
        GUI.Label(new Rect(position.x - (nameSize.x / 2), position.y - nameSize.y , nameSize.x, nameSize.y), text);

    }
}
