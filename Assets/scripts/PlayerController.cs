using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private GameObject CrystalHintPointer;

    [Header("Crystal Hint Pointer Settings")]
    [SerializeField] private float orbitRadius = 1.5f;
    [SerializeField] private float orbitHeight = 0f;
    [SerializeField] private float rotationOffsetY = 0f;

    void Update()
    {
        if (GameManager.Instance.PointerPoint != null)
        {
            CrystalHintPointer.SetActive(true);
            RotatePointerTowardsCrystal();
        }
        else
        {
            CrystalHintPointer.SetActive(false);
        }
    }

    private void RotatePointerTowardsCrystal()
    {
        Vector3 crystalPos = GameManager.Instance.PointerPoint.position;
        Vector3 parentPos  = CrystalHintPointer.transform.parent != null
                             ? CrystalHintPointer.transform.parent.position
                             : CrystalHintPointer.transform.position;

        Vector3 direction = crystalPos - parentPos;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetWorldRot = Quaternion.LookRotation(direction.normalized);

        Quaternion parentRot = CrystalHintPointer.transform.parent != null
                               ? CrystalHintPointer.transform.parent.rotation
                               : Quaternion.identity;

        Quaternion targetLocalRot = Quaternion.Inverse(parentRot) * targetWorldRot;

        Vector3 currentLocalEuler = CrystalHintPointer.transform.localEulerAngles;
        currentLocalEuler.y = targetLocalRot.eulerAngles.y + rotationOffsetY;
        CrystalHintPointer.transform.localEulerAngles = currentLocalEuler;

        // Hitung arah orbit di world space lalu konversi ke local space
        // sehingga hanya X dan Z local yang berubah, Y local tetap dari parent
        Vector3 worldOrbitDir = direction.normalized * orbitRadius;
        Vector3 localOrbitDir = CrystalHintPointer.transform.parent != null
                                ? CrystalHintPointer.transform.parent.InverseTransformDirection(worldOrbitDir)
                                : worldOrbitDir;

        CrystalHintPointer.transform.localPosition = new Vector3(
            localOrbitDir.x,
            orbitHeight,   // Y local tidak dipengaruhi orbit, hanya orbitHeight
            localOrbitDir.z
        );
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void die()
    {
        characterController.enabled = false;
        StartCoroutine(GameManager.Instance.Gameover());
    }
}