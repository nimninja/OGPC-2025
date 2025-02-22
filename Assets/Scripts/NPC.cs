using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject textBox;
    public Transform player;
    public float triggerRadius = 5f;

    private void Start()
    {
        textBox.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        textBox.SetActive(distance <= triggerRadius);
    }
}
