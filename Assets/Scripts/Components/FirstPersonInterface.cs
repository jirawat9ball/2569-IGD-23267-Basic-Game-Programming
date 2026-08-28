using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstPersonInterface : MonoBehaviour
{
    public TextMeshPro textMeshName;
    // Start is called before the first frame update
    public void SetColorCapsule(Color _color, MeshRenderer meshRenderer) {
        meshRenderer.material.color = _color;
    }
    public void SetUIName(string Name) {
        textMeshName.text = Name;
    }
    public void SetUIHp(int Hp, GameObject Heart,Transform SpwanPointHeart) {
        for (int i = 0; i < Hp; i++)
        {
            Vector3 pos = SpwanPointHeart.position + new Vector3(i, 0, 0);
            Instantiate(Heart, pos, SpwanPointHeart.rotation, SpwanPointHeart);
        }
    }
}
