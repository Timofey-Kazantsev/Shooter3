using UnityEngine;
using UnityEngine.SceneManagement;
using XtremeFPS.FPSController;

namespace XtremeFPS.WeaponSystem
{
    public class SC_EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public HealthPlayer playerHealth;
        public FirstPersonController player;
        public Texture crosshairTexture;
        public float spawnInterval = 2f;
        public int enemiesPerWave = 5;
        public Transform[] spawnPoints;

        private float nextSpawnTime = 0;
        private int waveNumber = 1;
        private bool waitingForWave = true;
        private float newWaveTimer = 10f;
        private int enemiesToEliminate;
        private int enemiesEliminated = 0;
        private int totalEnemiesSpawned = 0;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartNewWave();
        }

        void Update()
        {
            if (waitingForWave)
            {
                newWaveTimer -= Time.deltaTime;

                if (newWaveTimer <= 0)
                {
                    StartNewWave();
                }
            }
            else
            {
                if (Time.time >= nextSpawnTime && totalEnemiesSpawned < enemiesToEliminate)
                {
                    SpawnEnemy();
                }
            }
        }

        void StartNewWave()
        {
            enemiesToEliminate = waveNumber * enemiesPerWave;
            enemiesEliminated = 0;
            totalEnemiesSpawned = 0;
            waitingForWave = false;
        }

        void SpawnEnemy()
        {
            nextSpawnTime = Time.time + spawnInterval;

            if (spawnPoints.Length > 0)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
                SC_NPCEnemy npc = enemy.GetComponent<SC_NPCEnemy>();
                npc.playerTransform = player.transform;

                totalEnemiesSpawned++;
            }
        }

        public void EnemyEliminated(SC_NPCEnemy enemy)
        {
            enemiesEliminated++;

            if (enemiesEliminated >= enemiesToEliminate)
            {
                waveNumber++;
                waitingForWave = true;
                newWaveTimer = 10f;
            }
        }
    }
}
