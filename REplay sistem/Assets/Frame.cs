using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Frame {


    Vector3 pos,  Scale;
    Quaternion rot;
    GameObject gameobject;
    List<AnimationRecord> animation_records;
    public Frame(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale,List<AnimationRecord> anim_records)
    {
        pos = position;
        rot = rotation;
        Scale = scale;
        animation_records = anim_records;
        gameobject = go;
    }
    public Vector3 Position
    {
        get 
        {
            return pos;
        }
    }
    public Vector3 Scale_
    {
        get
        {
            return Scale;
        }
    }
    public GameObject Gameobject
    {
        get
        {
            return gameobject;
        }
    }
    public Quaternion Rotation
    {

        get 
        {
            return rot;
        }
    }
    public List<AnimationRecord> Animation_Records
    {
        get 
        {
            return animation_records;
        }
    }
}
