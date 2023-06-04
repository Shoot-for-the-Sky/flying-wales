using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Enemiess
    [SerializeField] protected GameObject meteorPrefab;
    [SerializeField] public bool createMeteors = false;
    private bool createdLastMeteor = false;
    GameObject lastMeteorInstance;
    public int numberOfMeteorPass = 0;

    // Values
    public int points;

    // Whales
    [SerializeField] protected GameObject whalePrefab;
    [SerializeField] protected int numberOfWhales = 3;
    private List<GameObject> whales;
    public WhaleState currentWhalesState;

    // Whales speed params
    [SerializeField] public float whaleSpeed = 1f;
    [SerializeField] public float whaleRotateSpeed = 5f;

    // Buttons contolling the whales
    [SerializeField] InputAction dynamicStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction trackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction attackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction LeftMouseButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

    // States UI Sprites
    [SerializeField] protected List<Sprite> dynamicSprites;
    [SerializeField] protected SpriteRenderer dynamicSpriteRenderer;

    [SerializeField] protected List<Sprite> trackSprites;
    [SerializeField] protected SpriteRenderer trackSpriteRenderer;

    [SerializeField] protected List<Sprite> attackSprites;
    [SerializeField] protected SpriteRenderer attackSpriteRenderer;

    // Cursors
    public Texture2D cursorArrow;
    public Texture2D cursorDynamic;
    public Texture2D cursorTrack;
    public Texture2D cursorAttack;

    void OnEnable()
    {
        dynamicStateButton.Enable();
        trackStateButton.Enable();
        attackStateButton.Enable();
        LeftMouseButton.Enable();
    }

    void OnDisable()
    {
        dynamicStateButton.Disable();
        trackStateButton.Disable();
        attackStateButton.Disable();
        LeftMouseButton.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set default cursor
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

        // Create whales
        CreateWhales();
        currentWhalesState = WhaleState.Dynamic;
        ChangeWhalesState();
        ChangeStatesUI(1, 0, 0);

        StartCoroutine(SpawnMeteorCoroutine());
    }

    private void CreateWhales()
    {
        whales = new List<GameObject>();
        for (int i = 0; i < numberOfWhales; i++)
        {
            GameObject whale = Instantiate(whalePrefab);
            whales.Add(whale);
        }
    }

    public void CreateMeteor()
    {
        GameObject prefabInstance = Instantiate(meteorPrefab);
        prefabInstance.transform.position = Vector3.zero;
    }

    private IEnumerator SpawnMeteorCoroutine()
    {
        while (true)
        {
            if (createMeteors)
            {
                Debug.Log("Create Meteor");
                lastMeteorInstance = Instantiate(meteorPrefab, Vector3.zero, Quaternion.identity);
                createdLastMeteor = true;
            }
            yield return new WaitForSeconds(10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        WhalesControl();
        if (createdLastMeteor && lastMeteorInstance == null)
        {
            Debug.Log("Meteor Destroy");
            numberOfMeteorPass++;
            createdLastMeteor = false;
        }
    }

    private void WhalesControl()
    {
        if (isWhaleStateControllerDisabled)
        {
            return;
        }
        if (dynamicStateButton.WasPressedThisFrame())
        {
            Cursor.SetCursor(cursorDynamic, Vector2.zero, CursorMode.ForceSoftware);
            currentWhalesState = WhaleState.Dynamic;
            ChangeWhalesState();
            ChangeStatesUI(1, 0, 0);
        }
        else if (trackStateButton.WasPressedThisFrame())
        {
            Cursor.SetCursor(cursorTrack, Vector2.zero, CursorMode.ForceSoftware);
            currentWhalesState = WhaleState.Track;
            ChangeWhalesState();
            ChangeStatesUI(0, 1, 0);
        }
        else if (attackStateButton.WasPressedThisFrame())
        {
            Cursor.SetCursor(cursorAttack, Vector2.zero, CursorMode.ForceSoftware);
            currentWhalesState = WhaleState.Attack;
            ChangeWhalesState();
            ChangeStatesUI(0, 0, 1);
        }
        else if (LeftMouseButton.WasPerformedThisFrame())
        {
            foreach (GameObject whale in whales)
            {
                whale.GetComponent<WhaleStateManager>().LeftMouseButtonClicked();
            }
        }
    }

    private void ChangeWhalesState()
    {
        foreach (GameObject whale in whales)
        {
            whale.GetComponent<WhaleStateManager>().ChangeStateByName(currentWhalesState);
        }
    }

    private void ChangeWhalesSpeed()
    {
        foreach (GameObject whale in whales)
        {
            whale.GetComponent<WhaleStateManager>().ChangeWhaleSpeed(whaleSpeed);
            whale.GetComponent<WhaleStateManager>().ChangeWhaleRotateSpeed(whaleRotateSpeed);
        }
    }

    private void ChangeStatesUI(int dynamic, int track, int attack)
    {
        dynamicSpriteRenderer.sprite = dynamicSprites[dynamic];
        trackSpriteRenderer.sprite = trackSprites[track];
        attackSpriteRenderer.sprite = attackSprites[attack];
    }
}
