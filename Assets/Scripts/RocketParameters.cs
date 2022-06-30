using UnityEngine;
[CreateAssetMenu(fileName = "RocketParameters", menuName = "ScriptableObjects/RocketParameters", order = 1)]
public class RocketParameters : ScriptableObject
{
    public AnimationCurve animCurve;
    public AnimationCurve interpolationAngleParam;
    public float forceStage;
    public bool reusable;
    
}
