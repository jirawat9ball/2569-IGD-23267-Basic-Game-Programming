using UnityEngine;

public class FirstPersonInterface : MonoBehaviour
{
    public TextMesh textMeshName;

    // Start is called before the first frame update
    public void SetColorCapsule(Color _color, MeshRenderer meshRenderer) {
        meshRenderer.material.color = _color;
    }
    public void SetUIName(string Name) {
        if (textMeshName != null) {
            textMeshName.text = Name;
        }
    }
    public void SetUIHp(int Hp, GameObject Heart, Transform SpawnPointHeart) {
        for (int i = 0; i < Hp; i++)
        {
            Vector3 pos = SpawnPointHeart.position + new Vector3(i, 0, 0);
            Instantiate(Heart, pos, SpawnPointHeart.rotation, SpawnPointHeart);
        }
    }
}
