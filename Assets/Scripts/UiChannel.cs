using UnityEngine;
[CreateAssetMenu(fileName = "UiChannel", menuName = "ScriptableObjects/UiChannel", order = 2)]
public class UiChannel : ScriptableObject
{
    public float maxH;
    public float velfirstStage, velSecondStage;
    public float hStage, hFstage;
    public void Reset()
    {
        maxH = 0f;
        velfirstStage = 0f;
        velSecondStage = 0f;
        hStage = 0f;
        hFstage = 0f;
    }
}
