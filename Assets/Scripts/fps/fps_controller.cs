using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_controller : MonoBehaviour
{

    public float speed = 4;
    public float sensitivity = 5;
    public GameObject Graphics;
    public GameObject Player;
    public GameObject Graphics2;
    
    public GameObject Cam;
    public Vector3 aim_offset;

    public float IkWeight;
    public GameObject aim_position;


   [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Animator Graphics2_anim;
    [HideInInspector] 
    public Vector3 rotation_of_camera;
    [HideInInspector] 
    public Vector3 rotation_of_player;
    [HideInInspector]
    public float rotSpeed;
    [HideInInspector]
    public float running_anim;
    [HideInInspector]
    public bool aim;
    [HideInInspector]
    public Transform chest;
    [HideInInspector]
    public float mouseX;
    [HideInInspector]
    public float mouseY;
    [HideInInspector]
    public float aim_angle;
    
    float xAxisClamp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Graphics2_anim = Graphics2.GetComponent<Animator>();

        running_anim = 2;
        aim = false;
        chest = Graphics2_anim.GetBoneTransform(HumanBodyBones.Chest);
        Debug.Log(chest.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        setting_variables();
        Move();
        Look();
        Aim();
        

    }

    void setting_variables()
    {

        rotation_of_camera = Cam.transform.rotation.eulerAngles;
        rotation_of_player = Player.transform.rotation.eulerAngles;
    }


    void Move()
    {
        float right_speed = 0;
        float forward_speed = 0;
        float forward_input = Input.GetAxis("Vertical");
        float right_input = Input.GetAxis("Horizontal");


       
        rotSpeed = 140;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (speed < 15)
            {
                speed = speed + 7.5f * Time.deltaTime;
               

            }
            if (running_anim <= 2.1 && running_anim >= 1)
            {
                running_anim = running_anim - 0.5f * Time.deltaTime;
            }
            
        }
        else
        {
            if (speed > 3)
            {
                speed = speed - 7.5f * Time.deltaTime;
               
            }
            if (running_anim >= 0.9 && running_anim < 2)
            {
                running_anim = running_anim + 0.5f * Time.deltaTime;
            }

        }
            
        if (speed > 9)
        {

            rotSpeed = 340;
        }
        else if (speed > 5 && speed < 10)
        {
           
            rotSpeed = 240;
        }
        
        else
        {
          
            rotSpeed = 140;
       
        }





        Graphics2_anim.SetFloat("posY", forward_input/ running_anim);
        Graphics2_anim.SetFloat("posX", right_input/ running_anim);
        Graphics2_anim.SetFloat("speed", speed);

        if (forward_input > 0 || forward_input < 0)
        {
            rotation_of_player.y = rotation_of_camera.y;

            Graphics2.transform.rotation = Quaternion.RotateTowards(Graphics2.transform.rotation, Quaternion.Euler(Player.transform.rotation.eulerAngles), rotSpeed * Time.deltaTime);
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, Quaternion.Euler(rotation_of_player), rotSpeed * Time.deltaTime);
            
            forward_speed = speed * Time.deltaTime * forward_input;

        }
      
        if (right_input > 0 || right_input < 0)
        {
            rotation_of_player.y = rotation_of_camera.y;
            
            Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, Quaternion.Euler(rotation_of_player), rotSpeed * Time.deltaTime);
            
            rotSpeed = 400;

         
            speed = Mathf.Lerp(speed, 2, 10 * Time.deltaTime);
            right_speed = speed * Time.deltaTime * right_input;
        }
        Vector3 playertransform = Player.transform.position;

        playertransform.y = 1.2f;
        Player.transform.position = playertransform;

        Player.transform.Translate(right_speed, 0, forward_speed);
    
    }

    void Look()
    {
        Cursor.lockState = CursorLockMode.Locked;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        mouseX = mouseX * sensitivity;
        mouseY = mouseY * sensitivity;

        //Vector3 rotation_of_graphics = Graphics.transform.rotation.eulerAngles;
        
        
        
        rotation_of_camera.x -= mouseY;
        rotation_of_camera.y += mouseX;
        if (aim == true) {
            rotation_of_player.y = rotation_of_camera.y;

            Graphics2.transform.rotation = Quaternion.RotateTowards(Graphics2.transform.rotation, Quaternion.Euler(Player.transform.rotation.eulerAngles), 1f * Time.deltaTime);
            
            rotation_of_player.y += mouseX;
            Player.transform.rotation = Quaternion.Euler(rotation_of_player);
        }
    
        //Cam.transform.position = new Vector3(Cam.transform.position.x, rotation_of_camera.x/10, Cam.transform.position.z);

        //rotation_of_graphics.x -= mouseY ;
        //rotation_of_graphics.y += mouseX ;

        //
        ////rotation_of_player.x -= mouseY ;

        xAxisClamp -= mouseY;
       
        if (xAxisClamp > 60)
        {
            xAxisClamp = 60;
            rotation_of_camera.x = 80;
        }
        else if (xAxisClamp < -80)
        {
            xAxisClamp = -80;
            rotation_of_camera.x = 280;
        }

        Cam.transform.rotation = Quaternion.Euler(rotation_of_camera);

        ////Graphics.transform.rotation = Quaternion.Euler(rotation_of_graphics);


        
        


    }
    
    void Aim()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (aim == false) { 
            Graphics2_anim.SetBool("aim", true);
                aim = true;
                
            }
            else
            {
                Graphics2_anim.SetBool("aim", false);
                aim = false;

            }
        }
        
        //aim_angle = aim_angle + mouseY;
        //aim_angle = aim_angle + aim_offset.x;
        //aim_offset.x = 0;

        //if (aim == true)
        //{

        //    Graphics2_anim.SetFloat("aim_angle", aim_angle);

        //}


    }

    //void OnAnimatorIK()
    //{
        
    

    //Graphics2_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, IkWeight);
    //    Graphics2_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IkWeight);

    //    Graphics2_anim.SetIKPosition(AvatarIKGoal.RightHand, aim_position.transform.position);
    //    Graphics2_anim.SetIKPosition(AvatarIKGoal.LeftHand, aim_position.transform.position);

    //    Graphics2_anim.SetIKRotationWeight(AvatarIKGoal.RightHand, IkWeight);
    //    Graphics2_anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, IkWeight);

    //    Graphics2_anim.SetIKRotation(AvatarIKGoal.RightHand, aim_position.transform.rotation);
    //    Graphics2_anim.SetIKRotation(AvatarIKGoal.LeftHand, aim_position.transform.rotation);


    //}
}
