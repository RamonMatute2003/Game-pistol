using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Movement{
    public class Player_controller:MonoBehaviour {
        private float horizontal_move, vertical_move;
        public float player_speed, gravity=9.8f, fall_velocity, jump_force, slide_velocity, slope_force_down;
        private Vector3 player_input, camera_forwad, camera_right, move_player, hit_normal;
        private bool is_on_slope;
        public AudioSource audio;
        public Animator player_animator_controller;
        public Camera main_camera;
        private CharacterController player;
        public Vector3 startPosition;
        public int health = 3;
        Transform muzzleFlashTransform;

        void Start(){
            player=GetComponent<CharacterController>();
            player_animator_controller=GetComponent<Animator>();
            startPosition=transform.position;
            Cursor.lockState=CursorLockMode.Locked;

            muzzleFlashTransform = FindDeepChild(transform,"VFX_MuzzleFlash");
        }

        Transform FindDeepChild(Transform aParent,string aName) {
            foreach(Transform child in aParent) {
                if(child.name==aName)
                    return child;
                Transform result = FindDeepChild(child,aName);
                if(result!=null)
                    return result;
            }

            return null;
        }

        void Update(){
            move();
        }

        public void subtract_life() {
            main_camera.GetComponent<Ui>().visible(health);
            health--;

            if(health==0) {
                game_over();
            } else{
                ResetPlayer();
            }
        }

        void ResetPlayer() {
            player.enabled=false;
            player.transform.position=startPosition;
            player.enabled=true;
            Cursor.lockState=CursorLockMode.Locked;
        }

        public void game_over(){
            main_camera.GetComponent<Ui>().status_game.text="Perdiste";
            main_camera.GetComponent<Ui>().time_game.text=main_camera.GetComponent<Ui>().time.text;
            main_camera.GetComponent<Ui>().menu.AddToClassList("menu_active");
            Cursor.lockState=CursorLockMode.None;
            Time.timeScale=0;
        }

        private void move(){
            horizontal_move=Input.GetAxis("Horizontal");
            vertical_move=Input.GetAxis("Vertical");
            player_input=new Vector3(horizontal_move,0,vertical_move);
            player_input=Vector3.ClampMagnitude(player_input,1);

            player_animator_controller.SetFloat("speed",player_input.magnitude*player_speed);

            camera_direction();
            move_player=player_input.x*camera_right+player_input.z*camera_forwad;

            move_player=move_player*player_speed;

            player.transform.LookAt(player.transform.position+move_player);

            set_gravity();

            player_skills();

            player.Move(move_player*Time.deltaTime);
        }

        private void player_skills(){
            if(player.isGrounded && Input.GetButtonDown("Jump")){
                fall_velocity=jump_force;
                move_player.y=fall_velocity;
                player_animator_controller.SetTrigger("player_jump");
            }else{
                if(Input.GetButtonDown("Fire1")&&Time.time>GetComponent<Shoot>().nextFireTime) {
                    GetComponent<Shoot>().nextFireTime=Time.time+GetComponent<Shoot>().fireRate;
                    GetComponent<Shoot>().Fire();
                    GetComponent<AudioSource>().Play();
                    muzzleFlashTransform.gameObject.GetComponent<ParticleSystem>().Play();
                }
            }
        }

        private void set_gravity(){
            if(player.isGrounded){
                fall_velocity=-gravity*Time.deltaTime;
                move_player.y=fall_velocity;
            } else{
                fall_velocity-=gravity*Time.deltaTime;
                move_player.y=fall_velocity;
                player_animator_controller.SetFloat("player_vertical_velocity",player.velocity.y);
            }

            player_animator_controller.SetBool("is_grounded",player.isGrounded);
            slide_down();
        }

        private void camera_direction(){
            camera_forwad=main_camera.transform.forward;
            camera_right=main_camera.transform.right;

            camera_forwad.y=0;
            camera_right.y=0;

            camera_forwad=camera_forwad.normalized;
            camera_right=camera_right.normalized;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit){
            hit_normal=hit.normal;
        }

        public void slide_down(){
            is_on_slope=Vector3.Angle(Vector3.up, hit_normal)>=player.slopeLimit;

            if(is_on_slope) {
                move_player.x+=((1f-hit_normal.y)*hit_normal.x)*slide_velocity;
                move_player.z+=((1f-hit_normal.y)*hit_normal.z)*slide_velocity;

                move_player.y+=slope_force_down;
            }
        }

        private void OnAnimatorMove() {
            
        }
    }
}

