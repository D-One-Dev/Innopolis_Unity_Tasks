using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(-5f, 1.6f, 0f);

    private GameObject _enemy;

    private void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab);
            _enemy.transform.position = spawnPosition;
            _enemy.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
        }
    }
}