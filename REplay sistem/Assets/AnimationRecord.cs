using UnityEngine;
using System.Collections;

public class AnimationRecord  {
    string name;
    float Deger_float;
    int Deger_int;
    bool bool_deger;
    AnimatorControllerParameterType type;
    public AnimationRecord(string n , float deger, AnimatorControllerParameterType ty)
    {
        Deger_float = deger;
        name = n;
        type = ty;
    }
    public AnimationRecord(string n, int deger, AnimatorControllerParameterType ty)
    {
        Deger_int = deger;
        name = n;
        type = ty;
    }
    public AnimationRecord(string n, bool deger, AnimatorControllerParameterType ty)
    {
        bool_deger = deger;
        name = n;
        type = ty;
    }
    public string Name
    {
        get 
        {
            return name;
        }
    }
    public float Float_
    {
        get
        {
            return Deger_float;
        }
    }
    public int Int_
    {
        get
        {
            return Deger_int;
        }
    }
    public bool Bool_
    {
        get
        {
            return bool_deger;
        }
    }
    public AnimatorControllerParameterType Type
    {
        get 
        {
            return type;
        }
    }
}
