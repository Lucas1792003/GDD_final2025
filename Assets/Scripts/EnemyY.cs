using UnityEngine;

public class EnemyX : MonoBehaviour
{
    // Global speed set by SpawnManagerX each wave
    private static float GlobalSpeed = 6f;
    public static void SetGlobalSpeed(float s) => GlobalSpeed = Mathf.Max(0f, s);

    private Rigidbody enemyRb;
    private Transform playerGoal;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // HINT #5: make sure this reference is assigned to avoid NREs
        GameObject goalObj = GameObject.Find("Player Goal"); // must match your object name
        if (goalObj != null) playerGoal = goalObj.transform;
        else Debug.LogError("EnemyX: Could not find 'Player Goal' in the scene.");
    }

    // Use FixedUpdate for physics forces
    void FixedUpdate()
    {
        if (playerGoal == null || enemyRb == null) return;

        Vector3 dir = (playerGoal.position - transform.position).normalized;
        enemyRb.AddForce(dir * GlobalSpeed * Time.fixedDeltaTime, ForceMode.Force);
        // (Time.fixedDeltaTime keeps it frame-rate consistent)
    }

    private void OnCollisionEnter(Collision other)
    {
        // destroy on either goal
        if (other.gameObject.name == "Enemy Goal" || other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
