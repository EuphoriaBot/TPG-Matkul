using UnityEngine;

public class Unitcamera : MonoBehaviour
{
   public GameObject Player;    

   private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
    }
}
