using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Winner:MonoBehaviour{
    private void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            Camera camera=other.GetComponent<Player_controller>().main_camera;
            Ui ui=camera.GetComponent<Ui>();

            ui.status_game.text="Ganaste";
            ui.time_game.text=ui.time.text;
            ui.menu.AddToClassList("menu_active");
            other.GetComponent<Player_controller>().enabled=false;
            UnityEngine.Cursor.lockState=CursorLockMode.None;
            Time.timeScale=1;
            ui.game=false;
        }
    }
}
