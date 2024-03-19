using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField] float timeToHide = 3;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToHide) { gameObject.SetActive(false); }
    }

    public void ResetTimer()
    {
        timer = 0;
    }
}
