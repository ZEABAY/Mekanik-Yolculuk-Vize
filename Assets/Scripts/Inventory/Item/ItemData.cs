using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class ItemData : ScriptableObject
{
    public int ID;
    public string Displayname;
    [TextArea(4, 4)] public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public bool usable;
    public GameObject OnGroundItemPrefab;
    public GameObject UsableItemPrefab;

    public Vector3 Position;

    public Transform InHandTransform
    {
        get
        {
            GameObject obj = new GameObject("YeniObj"); // Yeni bir GameObject oluþtur
            Transform newTransform = obj.transform;

            newTransform.position = Position;
            newTransform.eulerAngles = Vector3.zero;
            newTransform.localScale = Vector3.zero;

            return newTransform;
        }
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        var data = (ItemData)target;

        if (data == null || data.Icon == null) return null;

        var texture = new Texture2D(width, height);
        EditorUtility.CopySerialized(data.Icon.texture, texture);
        return texture;
    }
}
#endif
