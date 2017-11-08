using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayRecord : MonoBehaviour {

    public ReplayPlayer oynatici;
    public Animator animasyon;
    List<Frame> frames;
    List<AnimationRecord> animasyon_records;
    int max_lenght;
    int lenght;
    int frame_index = -1;
	// Use this for initialization
    public ReplayRecord()
    {
       
    
    }
	void Start () {

        oynatici.Ekle(this);
        max_lenght = oynatici.max_lenght;
        frames = new List<Frame>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Game.Game_Mode == Game.Game_Modes.RECORD)
        {
            animasyon_records = new List<AnimationRecord>();
            if (animasyon != null)
            {
                foreach (AnimatorControllerParameter item in animasyon.parameters)
                {
                    string name = item.name;
                    if (item.type == AnimatorControllerParameterType.Bool)
                    {
                        animasyon_records.Add(new AnimationRecord(name, animasyon.GetBool(name), item.type));
                    }
                    else if (item.type == AnimatorControllerParameterType.Float)
                    {
                        animasyon_records.Add(new AnimationRecord(name, animasyon.GetFloat(name), item.type));
                    }
                    else if (item.type == AnimatorControllerParameterType.Int)
                    {
                        animasyon_records.Add(new AnimationRecord(name, animasyon.GetInteger(name), item.type));
                    }
                }

            }
            Frame frame = new Frame(this.gameObject, transform.position, transform.rotation, transform.localScale, animasyon_records);
            Ekle(frame);
        }
    }
    void Ekle(Frame frm)
    {
        if (lenght < max_lenght)
        {
           
        }
        else 
        {
            frames.RemoveAt(0);
            lenght = max_lenght - 1;
        }
        frames.Add(frm);
        lenght++;
    }
    public void Play()
    {
        Frame frm;
        if ((frm = Get_Frame()) != null)
        {
            transform.position = frm.Position;
            transform.rotation = frm.Rotation;
            transform.localScale = frm.Scale_;
            foreach (AnimationRecord item in frm.Animation_Records)
            {
                string name = item.Name;
                if (item.Type == AnimatorControllerParameterType.Bool)
                {
                    animasyon.SetBool(name, item.Bool_);
                    continue;
                }
                else if (item.Type == AnimatorControllerParameterType.Int)
                {
                    animasyon.SetInteger(name, item.Int_);
                    continue;
                }
                else if (item.Type == AnimatorControllerParameterType.Float)
                {
                    animasyon.SetFloat(name, item.Float_);
                    continue;
                }
            }
        }
        else 
        {
            Debug.Log("REplay bitti");
            Game.Game_Mode = Game.Game_Modes.PAUSE;
        }
    }
    Frame Get_Frame()
    {
        frame_index++;
        if (Game.Game_Mode == Game.Game_Modes.PAUSE)
        {
            frame_index--;
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
        if (frame_index >= lenght)
        {
            Game.Game_Mode = Game.Game_Modes.PAUSE;
            frame_index = lenght - 1;
            return null;
        }
        if (frame_index == -1) 
        {
            frame_index = lenght - 1;
        }
        //Debug.Log(frame_index + "," + max_lenght+","+lenght);
        return frames[frame_index];
    }
    public void SetFrame(int value)
    {
        frame_index = value;
    }
    public int GetFrame()
    {
        return frame_index;
    }
    public int Lenght 
    {
        get 
        {
            return lenght;
        }
    }
}
