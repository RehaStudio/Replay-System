using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ReplayPlayer : MonoBehaviour {

    List<ReplayRecord> replay_records;
    public int max_lenght = 100;
    bool slider_controlling;
    public Slider slider;
    public float maximum_zoom = 90f;
    public float minumum_zoom = 10f;
    public float zoom_index = 5f;
    public float rotation_index = 2f;
    int camera_index = 0;
    public List<Camera> kameralar;
    public Canvas cnvs;
    public ReplayPlayer()
    {

        replay_records = new List<ReplayRecord>();
        Game.Game_Mode = Game.Game_Modes.RECORD;
        slider_controlling = false;
    }
	void Start () {

  
	}
	
	// Update is called once per frame
    public void Camera_Change()
    {
        camera_index = (camera_index + 1) % kameralar.Count;
        if (kameralar != null)
        {
            for (int i = 0; i < kameralar.Count; i++)
            {
                if (i == camera_index)
                {
                    kameralar[i].enabled = true;
                }
                else
                {
                    kameralar[i].enabled = false;
                }
            }
        }
    }
    public void Zoom()
    {
        if (kameralar[camera_index].fieldOfView > minumum_zoom)
        {
            kameralar[camera_index].fieldOfView -= zoom_index;
        }
    }
    public void UnZoom()
    {
        if (kameralar[camera_index].fieldOfView < maximum_zoom)
        {
            kameralar[camera_index].fieldOfView += zoom_index;
        }
    }
    public void Camera_rot_up()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x + rotation_index) % 360;
        kameralar[camera_index].transform.localEulerAngles = rot;
    }
    public void Camera_rot_down()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.x = (rot.x - rotation_index) % 360;
        kameralar[camera_index].transform.localEulerAngles = rot;

    }
    public void Camera_rot_left()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y= (rot.y- rotation_index) % 360;
        kameralar[camera_index].transform.localEulerAngles = rot;

    }
    public void Camera_rot_right()
    {
        Vector3 rot = kameralar[camera_index].transform.localEulerAngles;
        rot.y = (rot.y + rotation_index) % 360;
        kameralar[camera_index].transform.localEulerAngles = rot;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            Camera_Change();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Zoom();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UnZoom();
        }
        if (Input.GetKey(KeyCode.Q))
        {
                Camera_rot_up();
        }
        if (Input.GetKey(KeyCode.E))
        {

            Camera_rot_down();

        }
        if (Input.GetKey(KeyCode.R))
        {

            Camera_rot_left();

        }
        if (Input.GetKey(KeyCode.T))
        {

            Camera_rot_right();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cnvs.enabled)
            {
                cnvs.enabled = false;
                Exit();
            }
            else 
            {
                cnvs.enabled = true;
                Replay();
            }
        }
        if (Game.Game_Mode != Game.Game_Modes.RECORD)
        {
            foreach (ReplayRecord item in replay_records)
            {
                if (slider_controlling)
                {
                    item.SetFrame(Convert.ToInt32((slider.value)));
                }
                else 
                {
                    slider.value = item.GetFrame();
                    slider.maxValue = item.Lenght;
                }
                if (Game.Game_Mode == Game.Game_Modes.REPLAY)
                {
                    item.SetFrame(-1);
                }
                if (Game.Game_Mode == Game.Game_Modes.Exit)
                {
                    item.SetFrame(item.Lenght - 2);
                }
                item.Play();
               
            }
        }
        if (Game.Game_Mode == Game.Game_Modes.REPLAY)
        {
            Game.Game_Mode = Game.Game_Modes.PLAY;
        }
        if (Game.Game_Mode == Game.Game_Modes.Exit)
        {
            Game.Game_Mode = Game.Game_Modes.RECORD;
            Time.timeScale = 1;
        }
    }
    public void Ekle(ReplayRecord rec)
    {
        replay_records.Add(rec);
    }
    public void Pause()
    {
        Game.Game_Mode = Game.Game_Modes.PAUSE;
        Time.timeScale = 0;
    }
    public void Play()
    {
        Game.Game_Mode = Game.Game_Modes.PLAY;
        Time.timeScale = 1;
    }
    public void Replay()
    {
        Game.Game_Mode = Game.Game_Modes.REPLAY;
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Game.Game_Mode = Game.Game_Modes.Exit;
    }
    public void Click_Slider()
    {
        slider_controlling = true;
    }
    public void Slider_Break()
    {
        slider_controlling = false;
        Play();
    }
}
public static class Game
{
     static Game_Modes Game_mode;
    public enum Game_Modes
    { 
        PLAY,PAUSE,RECORD,REPLAY,Slider,Exit
    }
    public static Game_Modes Game_Mode
    {
        get 
        {
            return Game_mode;
        }
        set 
        {
            Game_mode = value;
        }
    }
}
