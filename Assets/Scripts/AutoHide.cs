using UnityEngine;

public class AutoHide : MonoBehaviour
{
    public bool shouldHide;
    [SerializeField] float timeToHide = 3;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToHide && shouldHide) { gameObject.SetActive(false); }
    }

    public void ResetTimer()
    {
        timer = 0; shouldHide = false;
    }
}
