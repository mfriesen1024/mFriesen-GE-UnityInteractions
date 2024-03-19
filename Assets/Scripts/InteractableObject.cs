using System.Linq;
using UnityEngine;

enum interactableType { info, collectable}

public class InteractableObject : MonoBehaviour
{
    public float detectionRange = 3;
    [SerializeField] string allowedTag = "Player";
    [SerializeField] string detectorTag = "InterObject";

    [Header("Upon Interaction")]
    [SerializeField] interactableType type = interactableType.info;
    public string infoString = "You picked up a d20.";
    public int collectableValue;

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
            detector.Load(this, detectionRange, allowedTag);
        }
    }

    public void Use()
    {
        if(type == interactableType.collectable) { gameObject.SetActive(false); }
    }
}

[RequireComponent(typeof(CircleCollider2D))]
public class InteractableDetector : MonoBehaviour
{
    // This will detect the player and inform them that the parent is interactable, while not interfering with other possible collisions.
    CircleCollider2D circleCollider;
    InteractableObject interObj;

    [SerializeField] string allowedTag;

    public void Load(InteractableObject interObj, float radius = 3, string allowedTag = null)
    {
        this.allowedTag = allowedTag;
        this.interObj = interObj;

        circleCollider = GetComponent<CircleCollider2D>();

        circleCollider.radius = radius;
        circleCollider.isTrigger = true;
    }
}