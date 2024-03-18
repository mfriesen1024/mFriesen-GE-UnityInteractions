using System.Linq;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] float detectionRange = 3;
    [SerializeField] string[] allowedTags = { "Player" };
    [SerializeField] string detectorTag = "InterObject";

    GameObject detectorObj;
    InteractableDetector detector;

    // Start is called before the first frame update
    void Start()
    {
        DetectorSetup();

        void DetectorSetup()
        {
            detectorObj = new GameObject();
            detectorObj.tag = detectorTag;
            detectorObj.transform.parent = transform; detectorObj.transform.position = transform.position;

            detector = detectorObj.AddComponent<InteractableDetector>();
            detector.Load(this, detectionRange, allowedTags);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[RequireComponent(typeof(CircleCollider2D))]
public class InteractableDetector : MonoBehaviour
{
    // This will detect the player and inform them that the parent is interactable, while not interfering with other possible collisions.
    CircleCollider2D circleCollider;
    InteractableObject interObj;

    [SerializeField] string[] allowedTags;

    public void Load(InteractableObject interObj, float radius = 3, string[] allowedTags = null)
    {
        this.allowedTags = allowedTags;
        this.interObj = interObj;

        circleCollider = GetComponent<CircleCollider2D>();

        circleCollider.radius = radius;
        circleCollider.isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowedTags.ToList().Contains(collision.gameObject.tag))
        {
            // Send the parent's component to the colliding object.
            collision.SendMessage("AddInteractable", interObj);
        }
    }
}