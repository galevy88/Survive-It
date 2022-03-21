using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethorLauncher : MonoBehaviour
{
    // methor prefab
    [SerializeField] GameObject methor;
    // time gap between instantiations
    [SerializeField] float timeGap = 0.05f;
    // player alive Status. when the player die the methors stops
    bool playerAlive = true;
    private void Start()
    {
        StartCoroutine(CreateNewMethor());
    }

    // Launching methors while the player is alive
    private IEnumerator CreateNewMethor()
    {
        while (playerAlive)
        {
            // instantiate a methor in a randomly position
            GameObject methorObj = Instantiate(methor, GenereteVector(), Quaternion.identity);
            // add RigidBody to the methor thus he can use the mg gravity.
            methorObj.AddComponent<Rigidbody>();
            methorObj.GetComponent<Rigidbody>().useGravity = true;

            yield return new WaitForSeconds(timeGap);
        }


    }

    // Generete a position to instantiaton of methor
    private Vector3 GenereteVector()
    {
        float xPos = Random.Range(2f, 46f);
        float zPos = Random.Range(2f, 46f);
        float yPos = 20f;
        return new Vector3(xPos, yPos, zPos);

    }

    // called by DeathHandler stop the methors form launching
    public void StopMethor()
    {
        playerAlive = false;
    }
}
