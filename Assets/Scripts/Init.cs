using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

    public GameObject world_prefab;

    void Awake()
    {
        if (!GameObject.Find("World"))
        {
            GameObject obj = Instantiate(world_prefab);
            obj.name = "World";
        }
        DestroyImmediate(gameObject);
    }

	void Start()
    {
        
    }
}