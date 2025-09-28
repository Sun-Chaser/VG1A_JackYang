using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

namespace SpaceShooter
{
    public class GameController : MonoBehaviour
    {
        
        public static GameController instance;
        
        // Outlets
        public Transform[] spawnPoints;
        public GameObject[] asteroidPrefabs;
        public GameObject explosionPrefab;
        public TMP_Text textScore;
        public TMP_Text textMoney;
        public TMP_Text missileSpeedUpgradeText;
        public TMP_Text bonusUpgradeText;
        
        // Configuration
        public float maxAsteroidDelay = 2f;
        public float minAsteroidDelay = 0.2f;
        
        // State Tracking
        public float timeElapsed;
        public float asteroidDelay;
        public int score;
        public int money;
        public float missileSpeed = 2f;
        public float bonusMultiplier = 1f;

        void Start()
        {
            StartCoroutine("AsteroidSpawnTimer");
            
            score = 0;
            money = 0;
            
            missileSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(missileSpeed * 25);
            bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier);
        }
        
        // Methods
        void Awake()
        {
            instance = this;
        }

        void Update()
        {
            // Increment passage of time for each frame of the game
            timeElapsed += Time.deltaTime;
            
            // Computer Asteroid Delay
            float decreaseDelayOverTime = maxAsteroidDelay - ((asteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
            asteroidDelay = Math.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);

            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            textScore.text = score.ToString();
            textMoney.text = money.ToString();
        }

        void SpawnAsteroid()
        {
            // Pick random spawn point and random asteroid prefabs
            int randomSpawnIndex = Random.Range(0, asteroidPrefabs.Length);
            Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
            int randomAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
            GameObject randomAsteroidPrefab = asteroidPrefabs[randomAsteroidIndex];
            
            // Spawn
            Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
        }

        IEnumerator AsteroidSpawnTimer()
        {
            // Wait
            yield return new WaitForSeconds(asteroidDelay);
            
            // Spawn
            SpawnAsteroid();
            
            // Repeat
            StartCoroutine("AsteroidSpawnTimer");
        }

        public void EarnPoints(int pointAmount)
        {
            score += Mathf.RoundToInt(pointAmount * bonusMultiplier);
            money += Mathf.RoundToInt(pointAmount * bonusMultiplier);
        }

        public void UpgradeMissileSpeed()
        {
            int cost = Mathf.RoundToInt(missileSpeed * 25);
            if (cost <= money)
            {
                money -= cost;

                missileSpeed += 1f;
                
                missileSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(missileSpeed * 25);
            }
        }

        public void UpgradeBonus()
        {
            int cost = Mathf.RoundToInt(100 * bonusMultiplier);
            if (cost <= money)
            {
                money -= cost;
                
                bonusMultiplier += 1f;
                
                bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier);
            }
        }
    }
}

