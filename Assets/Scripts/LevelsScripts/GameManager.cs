using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Enemies
    [SerializeField] protected GameObject meteorPrefab;
    [SerializeField] public bool createMeteors = false;
    public int numberOfSurvivedEnemies = 0;

    // Save enemies instance when created them
    // For checking when destroy and manipulate
    private List<GameObject> enemies;

    // Values
    public int score;

    // Whales
    [SerializeField] protected GameObject whalePrefab;
    [SerializeField] protected int numberOfWhales = 3;
    private List<GameObject> whales;
    public WhaleState currentWhalesState;

    // Whales speed params
    [SerializeField] public float whaleSpeed = 1f;
    [SerializeField] public float whaleRotateSpeed = 5f;

    // Buttons controlling the whales
    [SerializeField] InputAction dynamicStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction trackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction attackStateButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction LeftMouseButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

    // UI
    [SerializeField] protected Text ScoreText;

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

    // Registers
    public Dictionary<string, int> destroyedEnemiesCounter;
    public Dictionary<string, int> survivedEnemiesCounter;
    public Dictionary<string, int> playerPowersCounter;

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
        StartCoroutine(RandomScore());

        // Registers
        destroyedEnemiesCounter = new Dictionary<string, int>();
        survivedEnemiesCounter = new Dictionary<string, int>();
        playerPowersCounter = new Dictionary<string, int>();
        enemies = new List<GameObject>();
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

    private IEnumerator SpawnMeteorCoroutine()
    {
        while (true)
        {
            if (createMeteors)
            {
                GameObject meteor = Instantiate(meteorPrefab, Vector3.zero, Quaternion.identity);
                enemies.Add(meteor);
            }
            yield return new WaitForSeconds(10f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WhalesControl();
        ScoreText.text = score.ToString();
    }

    // Controlling the whales by their current state
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

    // Change the state of all the current whales
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

    // Register when enemy is destroyed by player (by attacking)
    public void RegisterDestroyedEnemy(string enemyTagName)
    {
        AddCounterToRegister(destroyedEnemiesCounter, enemyTagName);
        foreach (var kvp in destroyedEnemiesCounter) {
            Debug.Log("RegisterDestroyedEnemy) Key = " + kvp.Key + ", Value = " + kvp.Value);
        }
    }

    // Register when enemy is destroyed by himself (for example - meteor out of borders)
    public void RegisterSurvivedEnemy(string enemyTagName)
    {
        AddCounterToRegister(survivedEnemiesCounter, enemyTagName);
        foreach (var kvp in survivedEnemiesCounter) {
            Debug.Log("RegisterSurvivedEnemy [" + enemyTagName + "]) Key = " + kvp.Key + ", Value = " + kvp.Value);
        }
    }

    // Register the use of power by player
    public void RegisterPowerPlayer(string powerName)
    {
        AddCounterToRegister(playerPowersCounter, powerName);
        foreach (var kvp in playerPowersCounter) {
            Debug.Log("RegisterPowerPlayer) Key = " + kvp.Key + ", Value = " + kvp.Value);
        }
    }

    // General function that add count to register dictionary counter
    private void AddCounterToRegister(Dictionary<string, int> registerCounter, string keyName)
    {
        if (registerCounter.ContainsKey(keyName))
        {
            registerCounter[keyName]++;
        }
        else
        {
            registerCounter[keyName] = 1;
        }
    }

    // Check if task is filled with required destroyed enemies
    public bool IsFilledDestroyedEnemies(string enemyTagName, int count)
    {
        return IsFilledRegister(destroyedEnemiesCounter, enemyTagName, count);
    }

    // Check if task is filled with required survived enemies
    public bool IsFilledSurvivedEnemies(string enemyTagName, int count)
    {
        return IsFilledRegister(survivedEnemiesCounter, enemyTagName, count);
    }

    // Check if task is filled with required player powers
    public bool IsFilledPlayerPowers(string powerTagName, int count)
    {
        return IsFilledRegister(playerPowersCounter, powerTagName, count);
    }

    // General function for checking if task is filled with register dictionary counter
    private bool IsFilledRegister(Dictionary<string, int> registerCounter, string tagName, int count)
    {
        bool isFilled = false;
        if (count == 0)
        {
            isFilled = true;
        }
        else if (registerCounter.ContainsKey(tagName))
        {
            isFilled = registerCounter[tagName] >= count;
        }
        return isFilled;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    private IEnumerator RandomScore()
    {
        while (true)
        {
            if (currentWhalesState == WhaleState.Dynamic)
            {
                if (UtilFunctions.RollInPercentage(25))
                {
                    int scoreToAdd = UtilFunctions.GetRandomIntInRange(1, 3);
                    AddScore(scoreToAdd);
                    // todo: Take random alive whale add make animation of gathering score
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
