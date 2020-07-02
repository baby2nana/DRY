using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek{
    
public class MenuEditor : MonoBehaviour
{
    [UnityEditor.MenuItem("JinBao/Menu/Create")]
    static void CreateObj()
    {
        GameObject obj = new GameObject("JinBao");
        obj.AddComponent<AddCompont>();
    }
}

}
