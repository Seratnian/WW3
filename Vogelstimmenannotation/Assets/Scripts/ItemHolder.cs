using System.Collections;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    new private Camera camera;
    public GameObject[] items;
    private ArrayList currentItems;

	void Start ()
    {
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentItems = new ArrayList();

        foreach (GameObject item in items)
        {
            AddItem(item);
        }
    }
	
	void Update ()
    {
        Vector3 cameraPosition = camera.transform.position;
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y / 2, cameraPosition.z);

        float cameraRotation = camera.transform.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(cameraRotation, Vector3.up);
	}

    internal void RemoveItem(GameObject item)
    {
        if (currentItems.Contains(item))
        {
            int index = currentItems.IndexOf(item);
            currentItems.Remove(item);
            StartCoroutine(AddItemLater(items[index]));
        }
    }

    private void AddItem(GameObject item)
    {
        GameObject clone = Instantiate(item, transform);
        currentItems.Add(clone);
    }

    private IEnumerator AddItemLater(GameObject item)
    {
        yield return new WaitForSeconds(1);
        AddItem(item);
    }
}
