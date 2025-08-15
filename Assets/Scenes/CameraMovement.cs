using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Param�tres de d�placement")]
    public float moveSpeed = 5.0f;        // Vitesse de d�placement
    public float mouseSensitivity = 2.0f; // Sensibilit� de la souris pour regarder autour
    public float rotationSpeed = 50.0f;   // Vitesse de rotation avec les touches

    private float rotationX = 0f;

    void Start()
    {
        // Verrouille le curseur au centre de l'�cran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        // R�cup�re les entr�es clavier
        float horizontal = Input.GetAxis("Horizontal"); // A/D ou Fl�ches gauche/droite
        float vertical = Input.GetAxis("Vertical");     // W/S ou Fl�ches haut/bas

        // D�placement vertical (haut/bas)
        float upDown = 0f;
        if (Input.GetKey(KeyCode.Space))
            upDown = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
            upDown = -1f;

        // Calcule le mouvement bas� sur la direction de la cam�ra
        Vector3 direction = transform.right * horizontal + transform.forward * vertical + Vector3.up * upDown;

        // Applique le mouvement
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        // Rotation avec les touches du clavier
        float rotateHorizontal = 0f;
        float rotateVertical = 0f;

        // Rotation horizontale (autour de l'axe Y)
        if (Input.GetKey(KeyCode.Q))
            rotateHorizontal = -1f;
        if (Input.GetKey(KeyCode.E))
            rotateHorizontal = 1f;

        // Rotation verticale (autour de l'axe X)
        if (Input.GetKey(KeyCode.R))
            rotateVertical = 1f;
        if (Input.GetKey(KeyCode.F))
            rotateVertical = -1f;

        // Rotation autour de l'axe Z (inclinaison)
        float rotateRoll = 0f;
        if (Input.GetKey(KeyCode.T))
            rotateRoll = 1f;
        if (Input.GetKey(KeyCode.G))
            rotateRoll = -1f;

        // Applique les rotations
        if (rotateHorizontal != 0f)
            transform.Rotate(0, rotateHorizontal * rotationSpeed * Time.deltaTime, 0, Space.World);

        if (rotateVertical != 0f)
            transform.Rotate(rotateVertical * rotationSpeed * Time.deltaTime, 0, 0, Space.Self);

        if (rotateRoll != 0f)
            transform.Rotate(0, 0, rotateRoll * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void HandleMouseLook()
    {
        // Rotation horizontale (Y) bas�e sur le mouvement de la souris en X
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        // Rotation verticale (X) bas�e sur le mouvement de la souris en Y
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limite l'angle vertical

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        // G�re le verrouillage du curseur quand la fen�tre perd/gagne le focus
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}