using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int ID => GetInstanceID();

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite Image { get; set; }

    [field: SerializeField]
    public bool isStackable { get; set; }
    [field: SerializeField]
    public int maxStackable { get; set; }

}
