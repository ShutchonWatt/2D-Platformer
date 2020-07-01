using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float waitToRespawn;
    public PlayerController thePlayer;

    public GameObject deathSplosion;

    public int coinCount;
    private int coinBonusLifeCount;
    public int bonusLifeThreshold;

    public AudioSource coinSound;

    public Text coinText;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;

    public bool respawning;

    public ResetOnRespawn[] objectsToReset;

    public bool invincible;

    public Text livesText;
    public int startingLives;
    public int currentLives;

    public GameObject gameOverScreen;

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;

    public bool respawnCoActive;

    // Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();

        healthCount = maxHealth;

        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        if(PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        coinText.text = "Coins: " + coinCount;
        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        }
        else
        {
            currentLives = startingLives;
        }
        livesText.text = "Live x " + currentLives;
	}
	
	// Update is called once per frame
	void Update () {
        if (healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }

        if (coinBonusLifeCount >= bonusLifeThreshold)
        {
            currentLives += 1;
            livesText.text = "Live x " + currentLives;
            coinBonusLifeCount -= bonusLifeThreshold;
            coinCount-= bonusLifeThreshold;
            coinText.text = "Coins: " + coinCount;
        }
	}

    public void Respawn()
    {
        //thePlayer.gameObject.SetActive(false);
        //thePlayer.transform.position = thePlayer.respawnPosition;
        //thePlayer.gameObject.SetActive(true);
        currentLives -= 1;
        livesText.text = "Live x " + currentLives;

        if(currentLives > 0)
        {
            StartCoroutine("RespawnCo");
        }
        else
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            levelMusic.Stop();
            gameOverMusic.Play();

            //levelMusic.volume = 0.5f;
        }
    }

    public IEnumerator RespawnCo() // corotine function
    {
        respawnCoActive = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathSplosion,thePlayer.transform.position,thePlayer.transform.rotation);
        yield return new WaitForSeconds(waitToRespawn);
        respawnCoActive = false;

        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

        coinCount = 0;
        coinBonusLifeCount = 0;
        coinText.text = "Coins: " + coinCount;
        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

        for(int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].ResetObject();
            objectsToReset[i].gameObject.SetActive(true);
        }
    }
    public void AddCoins(int coinsToAdd)
    {
        coinCount+=coinsToAdd;
        coinBonusLifeCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;

        coinSound.Play();
    }
    public void HurtPlayer(int damageToTake)
    {
        if (!invincible)
        {
            thePlayer.hurtSound.Play();
            healthCount -= damageToTake;
            UpdateHeartMeter();
            thePlayer.Knockback();
        }
    }
    public void GiveHealth(int healthToGive)
    {
        healthCount += healthToGive;
        if(healthCount> maxHealth)
        {
            healthCount = maxHealth;
        }
        UpdateHeartMeter();
        coinSound.Play();
    }


    public void UpdateHeartMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }

    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;
        coinSound.Play();
     }
}
