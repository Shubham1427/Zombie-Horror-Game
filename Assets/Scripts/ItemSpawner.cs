using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] locations;
    public GameObject[] drawerOpenables;
    public GameObject[] SpawnedFinalItems;
    public int[] occupied;
    public GameObject crosshair, actionCrosshair, pickUpText, buttonText, dropText, dropButtonText;
    public GameObject zombie;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    { 
        occupied = new int[locations.Length];
        for (int i = 0; i < occupied.Length; i++)
            occupied[i] = 0;
        SpawnedFinalItems = new GameObject[items.Length]; 
        if (items.Length > locations.Length)
        {
            print("More items than locations! Not spawning!");
            enabled = false;
        }
        int j = locations.Length - 1;
        int temp = Random.Range(1, 4);
        GameObject item = Instantiate(items[temp]);         
        item.transform.position = locations[j].transform.position;
        item.transform.rotation = locations[j].transform.rotation;
        if (temp == 3)
        {
            item.transform.position += new Vector3(0.1f, 0f, 0f);
        }
        item.GetComponentInChildren<ItemPickup>().enabled = true;
        item.GetComponentInChildren<ItemPickup>().buttonText = buttonText;
        item.GetComponentInChildren<ItemPickup>().pickUpText = pickUpText;
        item.GetComponentInChildren<ItemPickup>().crosshair = crosshair;
        item.GetComponentInChildren<ItemPickup>().actionCrosshair = actionCrosshair;
        item.GetComponentInChildren<ItemPickup>().dropButtonText = dropButtonText;
        item.GetComponentInChildren<ItemPickup>().dropText = dropText;
        item.GetComponentInChildren<ItemPickup>().zombie = zombie;
        item.GetComponentInChildren<ItemPickup>().cam = cam;
        occupied[j] = 1;
        SpawnedFinalItems[temp] = item;
        for (int i = 0; i < items.Length; i++)
        {
            if (i == temp)
                continue;
            while (true)
            {
                j = Random.Range(0, locations.Length - 1);
                if (occupied[j] != 1)
                {
                    if ((locations[j].name == "Location Drawers" || locations[j].name == "Location Drawers 1" || locations[j].name == "Location Drawers 2") && i == 0)
                    {
                        continue;
                    }
                    item = Instantiate(items[i]);
                    item.transform.position = locations[j].transform.position;
                    item.transform.rotation = locations[j].transform.rotation;
                    item.GetComponentInChildren<ItemPickup>().enabled = true;
                    item.GetComponentInChildren<ItemPickup>().buttonText = buttonText;
                    item.GetComponentInChildren<ItemPickup>().pickUpText = pickUpText;
                    item.GetComponentInChildren<ItemPickup>().crosshair = crosshair;
                    item.GetComponentInChildren<ItemPickup>().actionCrosshair = actionCrosshair;
                    item.GetComponentInChildren<ItemPickup>().dropButtonText = dropButtonText;
                    item.GetComponentInChildren<ItemPickup>().dropText = dropText;
                    item.GetComponentInChildren<ItemPickup>().zombie = zombie;
                    item.GetComponentInChildren<ItemPickup>().cam = cam;
                    if (locations[j].name == "Location Drawers")
                        item.transform.parent = drawerOpenables[0].transform;
                    else if (locations[j].name == "Location Drawers 1")
                        item.transform.parent = drawerOpenables[1].transform;
                    else if (locations[j].name == "Location Drawers 2")
                        item.transform.parent = drawerOpenables[2].transform;
                    occupied[j] = 1;
                    SpawnedFinalItems[i] = item;
                    break;
                }
            }
        }
    }
}
