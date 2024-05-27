using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject, List<GameObject>> objectList = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetGameObject(GameObject defaultGameObject)
    {
        if (objectList.ContainsKey(defaultGameObject))
        {
            foreach (GameObject o in objectList[defaultGameObject])
            {
                if (o.activeSelf)
                {
                    continue;
                }
                return o;
            }

            GameObject g = Instantiate(defaultGameObject, this.transform.position, Quaternion.identity);
            objectList[defaultGameObject].Add(g);
            g.SetActive(false);

            return g;
        }

        List<GameObject> instantObjectList = new List<GameObject>();

        GameObject g2 = Instantiate(defaultGameObject, this.transform.position, Quaternion.identity);
        instantObjectList.Add(g2);
        g2.SetActive(false);
        objectList.Add(defaultGameObject, instantObjectList);

        return g2;
    }
}
