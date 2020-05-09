using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Death : MonoBehaviour{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Zombie")
        {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            other.GetComponent<ZombieAI>().enabled = false;
            other.GetComponent<zombieController>().enabled = false;
            other.transform.LookAt(player.transform);
            other.transform.rotation = Quaternion.Euler(new Vector3(0f, other.transform.rotation.eulerAngles.y, 0f));
            player.transform.LookAt(other.transform);
            player.transform.rotation = Quaternion.Euler(new Vector3(0f, player.transform.rotation.eulerAngles.y, 0f));
            other.GetComponent<Animator>().SetInteger("statusCheck", 1);
            other.GetComponent<Animator>().speed = 1f;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack ()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(1);        
    }
}
