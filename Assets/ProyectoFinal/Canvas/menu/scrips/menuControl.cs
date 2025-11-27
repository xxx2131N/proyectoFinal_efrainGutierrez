using UnityEngine;

public class menuControl : MonoBehaviour
{
    public GameObject menuPrincipal;
    public MonoBehaviour PlayerMovement;
    public MonoBehaviour MirarCamara;
    private bool MenuOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuPrincipal.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
           AlternarMenu();
        }
    }
    void AlternarMenu()
    {
        MenuOpen = !MenuOpen;
        menuPrincipal.SetActive(MenuOpen);
        if (MenuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PlayerMovement.enabled = false;
            MirarCamara.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PlayerMovement.enabled = true;
            MirarCamara.enabled = true;
        }
    }
}
