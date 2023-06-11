using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    // Enemies
    [SerializeField] protected GameObject meteorPrefab;
    [SerializeField] public bool canCreateMeteors = false;
    [SerializeField] public float createMeteorEachSec = 10;
    [SerializeField] public bool canGatherScore = false;
    [SerializeField] public bool requiredStateForTime = false;
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
    [SerializeField] InputAction leftMouseButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction callPowerButton = new InputAction(type: InputActionType.Button);
    [SerializeField] InputAction shieldPowerButton = new InputAction(type: InputActionType.Button);
    public bool isWhaleStateControllerDisabled = false;

    // Powers
    public bool isShieldPowerActive = false;
    public bool isShieldDisableToUse = false;
    public bool isCallPowerActive = false;
    public bool isCallDisableToUse = false;
    private PowerUIScript shieldPowerUIScript;
    private PowerUIScript callPowerUIScript;
    [SerializeField] private GameObject shieldGameObject;
    private GameObject shieldInstance;

    // UI
    [SerializeField] protected Text ScoreText;
    [SerializeField] protected GameObject scoreCollectorPrefab;

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

    // Whale status params
    public bool whalesAttacking = false;

    void OnEnable()
    {
        dynamicStateButton.Enable();
        trackStateButton.Enable();
        attackStateButton.Enable();
        leftMouseButton.Enable();
        callPowerButton.Enable();
        shieldPowerButton.Enable();
    }

    void OnDisable()
    {
        dynamicStateButton.Disable();
        trackStateButton.Disable();
        attackStateButton.Disable();
        leftMouseButton.Disable();
        callPowerButton.Disable();
        shieldPowerButton.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set default cursor
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

        // Create whales
        whales = new List<GameObject>();
        CreateWhales();
        currentWhalesState = WhaleState.Dynamic;
        ChangeWhalesState();
        ChangeStatesUI(1, 0, 0);

        StartCoroutine(SpawnMeteorCoroutine());

        // Registers
        destroyedEnemiesCounter = new Dictionary<string, int>();
        survivedEnemiesCounter = new Dictionary<string, int>();
        playerPowersCounter = new Dictionary<string, int>();
        enemies = new List<GameObject>();

        // Powers
        GameObject shieldGameObject = GameObject.FindWithTag("ShieldPowerIcon");
        shieldPowerUIScript = shieldGameObject.GetComponent<PowerUIScript>();

        GameObject callGameObject = GameObject.FindWithTag("CallPowerIcon");
        callPowerUIScript = callGameObject.GetComponent<PowerUIScript>();
    }

    private void CreateWhales()
    {
        for (int i = 0; i < numberOfWhales; i++)
        {
            CreateWhale();
        }
    }

    private void CreateWhale()
    {
        GameObject whale = Instantiate(whalePrefab);
        whales.Add(whale);
    }

    private IEnumerator SpawnMeteorCoroutine()
    {
        while (true)
        {
            if (canCreateMeteors)
            {
                GameObject meteor = Instantiate(meteorPrefab, Vector3.zero, Quaternion.identity);
                enemies.Add(meteor);
            }
            yield return new WaitForSeconds(createMeteorEachSec);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleDeadWhales();
        WhalesControl();
        CheckPowers();
        ScoreText.text = score.ToString();
    }

    private void HandleDeadWhales()
    {
        UtilFunctions.RemoveNullObjectsFromList(whales);
    }

    public void CheckPowers()
    {
        shieldPowerUIScript.inUse = isShieldPowerActive;
        callPowerUIScript.inUse = isCallPowerActive;
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
            whalesAttacking = false;
            ChangeWhalesState();
            ChangeStatesUI(1, 0, 0);
        }
        else if (trackStateButton.WasPressedThisFrame())
        {
            Cursor.SetCursor(cursorTrack, Vector2.zero, CursorMode.ForceSoftware);
            currentWhalesState = WhaleState.Track;
            whalesAttacking = false;
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
        else if (leftMouseButton.WasPressedThisFrame())
        {
            foreach (GameObject whale in whales)
            {
                whale.GetComponent<WhaleStateManager>().LeftMouseButtonClicked();
            }
        }
        else if (callPowerButton.WasPressedThisFrame() && !isCallPowerActive && !isCallDisableToUse)
        {
            UseCallPower();
        }
        else if (shieldPowerButton.WasPressedThisFrame() && !isShieldPowerActive && !isShieldDisableToUse)
        {
            UseShieldPower();
        }
    }

    // Change the state of all the current whales
    public void ChangeWhalesState()
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
        Debug.Log("destroyedEnemiesCounter:");
        UtilFunctions.PrintDictionary(destroyedEnemiesCounter);
    }

    // Register when enemy is destroyed by himself (for example - meteor out of borders)
    public void RegisterSurvivedEnemy(string enemyTagName)
    {
        AddCounterToRegister(survivedEnemiesCounter, enemyTagName);
        Debug.Log("RegisterSurvivedEnemy:");
        UtilFunctions.PrintDictionary(survivedEnemiesCounter);
    }

    // Register the use of power by player
    public void RegisterPowerPlayer(string powerName)
    {
        AddCounterToRegister(playerPowersCounter, powerName);
        Debug.Log("survivedEnemiesCounter:");
        UtilFunctions.PrintDictionary(survivedEnemiesCounter);
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

    private GameObject GetRandomAliveWhale()
    {
        GameObject randomWhale = null;
        while (randomWhale == null)
        {
            int randomWhaleIndex = UtilFunctions.GetRandomIntInRange(0, whales.Count);
            randomWhale = whales[randomWhaleIndex];
        }
        return randomWhale;
    }

    public void RandomWhaleTakeScore(int scoreToAdd)
    {
        GameObject randomWhale = GetRandomAliveWhale();
        Vector2 randomWhalePosition = new Vector2(randomWhale.transform.position.x, randomWhale.transform.position.y);
        string scoreText = "+" + scoreToAdd.ToString();
        GameObject scoreCollector = Instantiate(scoreCollectorPrefab, randomWhale.transform.position, Quaternion.identity);
        ScoreCollectorScript scoreCollectorScript = scoreCollector.GetComponent<ScoreCollectorScript>();
        scoreCollectorScript.score = scoreToAdd;
    }

    public void UseCallPower()
    {
        RegisterPowerPlayer("Call");
        isCallPowerActive = true;
        CreateWhale();
        Debug.Log("UseCallPower");
        FunctionTimer.Create(DisableUseCallPower, 5f);
        ChangeWhalesState();
    }

    public void DisableUseCallPower()
    {
        isCallPowerActive = false;
        Debug.Log("DisableUseCallPower");
    }

    public void UseShieldPower()
    {
        RegisterPowerPlayer("Shield");
        isShieldPowerActive = true;
        shieldInstance = Instantiate(shieldGameObject, Vector3.zero, Quaternion.identity);
        Debug.Log("UseShieldPower");
        FunctionTimer.Create(DisableUseShieldPower, 5f);
    }

    public void DisableUseShieldPower()
    {
        isShieldPowerActive = false;
        if (shieldInstance != null)
        {
            Destroy(shieldInstance);
        }
        Debug.Log("DisableUseShieldPower");
    }
}
