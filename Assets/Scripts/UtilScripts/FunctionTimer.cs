using System;
using UnityEngine;

// Help taken from: https://www.youtube.com/watch?v=1hsppNzx7_0
public class FunctionTimer
{
    public static FunctionTimer Create(Action action, float timer) {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;
        return functionTimer;
    }

    // Dummy class to have access to MonoBehaviour functions
    public class  MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null)
            {
                onUpdate();
            }
        }
    }

    private Action action;
    private float timer;
    private GameObject gameObject;
    private bool isDestroyed;

    private FunctionTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                action();
                DestroyedSelf();
            }
        }
    }

    private void DestroyedSelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}