using System.Collections;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
     public Camera playerCamera;
    public Camera cutsceneCamera;

    public Transform camStart;
    public Transform camEnd;

    public float duration = 3f;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(PlayCutscene(other.gameObject));
        }
    }

    IEnumerator PlayCutscene(GameObject player)
    {
        // ปิดการควบคุมผู้เล่น
        player.GetComponent<PlayerMovementScript>().enabled = false;

        // สลับกล้อง
        playerCamera.enabled = false;
        cutsceneCamera.enabled = true;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // แพนกล้อง
            cutsceneCamera.transform.position = Vector3.Lerp(camStart.position, camEnd.position, t);
            cutsceneCamera.transform.rotation = Quaternion.Lerp(camStart.rotation, camEnd.rotation, t);

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        // กลับไปเล่น
        cutsceneCamera.enabled = false;
        playerCamera.enabled = true;

        player.GetComponent<PlayerMovementScript>().enabled = true;
    }
}
