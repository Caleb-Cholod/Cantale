using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Random=UnityEngine.Random;
using UnityEngine.Rendering.Universal;


public class GameManager : MonoBehaviour
{
    //MUSIC
    private AudioSource theMusic;
    public bool isPlaying = true;
    
    //BASIC
    public static GameManager instance;

    public GameObject player;
    public GameObject enemy;
    private int enemyid;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Sprite[] characterHitSprites;

    public bool isMyTurn = true;
    private int DmgCounter = 0;
    private int EnemyDmgCounter = 0;

    public AudioSource winSong;
    public AudioSource attackHitSong;

    private int Pcount;
    private int Gcount;
    private int Hcount;
    private int Mcount;
    //TEXT
    [HideInInspector]
    public TMP_Text PerfectText;
    [HideInInspector]
    public TMP_Text GoodText;
    [HideInInspector]
    public TMP_Text HitText;
    [HideInInspector]
    public TMP_Text MissText;
    [HideInInspector]
    public TMP_Text activeHitText;
    [HideInInspector]
    public GameObject endfightPanel;
    [HideInInspector]
    public GameObject healthbar;
    [HideInInspector]
    public GameObject EXPbar;
    [HideInInspector]
    public GameObject Enemyhealthbar;
    [HideInInspector]
    public TMP_Text scoreText;
    [HideInInspector]
    public TMP_Text multiplierText;
    [HideInInspector]
    public TMP_Text playerHPText;
    [HideInInspector]
    public TMP_Text EnemyHPText;
    [HideInInspector]
    public TMP_Text GoldText;
    [HideInInspector]
    public TMP_Text GoldReceivedText;
    [HideInInspector]
    public TMP_Text EXPReceivedText;
    [HideInInspector]
    public TMP_Text LevelText;
    
    private GameObject idholder;
    private GameObject Dataholder;
    private float resize;

    public TMP_Text dmgTextPhysics;
    public GameObject dmgTextParent;
    public TMP_Text PlyrdmgTextPhysics;
    public GameObject PlyrdmgTextParent;

    public GameObject hitParticle;
    public Light2D particleLight;
    public GameObject enemyHitParticle;
    public Light2D EnemyParticleLight;

    public GameObject EnemySliceAnim;
    public int characterFrame = 0;

    public GameObject DragShadow;
    public GameObject FakeDragon;
    public AudioSource dragonroar;

    public GameObject explosionParticle;
    public GameObject PlayerexplosionParticle;

    public GameObject PlayerDeathPanel;

    public GameObject HelpPanel;

    public TMP_Text comboText;
    private int Notecombo;

    //EQUIPMENT
    public GameObject EquipCharge;
    private int EquipHits = 0;
    private int EquipHitsFull;
    private bool isFocused = false;
    private float focusedTime = 2.5f;

    public GameObject EquipImg;
    private Sprite EquipInactive;
    private Sprite Equipactive;

    private GameObject EnemyDataholder;

    public Light2D EquipActiveLight;

    public AudioSource EquipUseAudio;


    public GameObject updownFade;

    private GameObject ItemHolder;
    public GameObject noMisseslabel;
    private bool isastar = false;

    private bool isinPauseMenu;
    //Items and Abilities
    private int poisonCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        EquipActiveLight.enabled = false;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        //Find Enemy and Load
        idholder = GameObject.FindWithTag("IDTag");
        Dataholder = GameObject.FindWithTag("DataHolder");
        ItemHolder = GameObject.FindWithTag("ItemHolder");
        EnemyDataholder = GameObject.FindWithTag("EnemyDataHolder");

        enemyid = Dataholder.GetComponent<EnemyData>().enemyID;

        if(enemyid == 4){
            //IF WE ARE IN DRAGON PHASE OF BOSS FIGHT
            //Remove fader
            updownFade.SetActive(false);
            //play dragon/shadow animation
            Invoke("DragonShadow", 0.0f);
            Invoke("DragonFlyDown", 10.0f);
            Invoke("DragonRoar", 17f);
            Invoke("StartDragonFight", 20.0f);   

        }
        
        
        theMusic = EnemyDataholder.GetComponent<EnemyDataHolder>().songs[enemyid];

        
        //Equipment
        Equipactive = ItemHolder.GetComponent<ItemDatabase>().Equipmentsprites[Dataholder.GetComponent<Preferences>().EquipmentID];
        EquipInactive = ItemHolder.GetComponent<ItemDatabase>().EquipmentInactivesprites[Dataholder.GetComponent<Preferences>().EquipmentID];
        EquipHitsFull = ItemHolder.GetComponent<ItemDatabase>().EquipmentCharges[Dataholder.GetComponent<Preferences>().EquipmentID];
        EquipImg.GetComponent<Image>().sprite = EquipInactive;

        //Pitch Bending----------
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("PitchMixer");
        theMusic.outputAudioMixerGroup = pitchBendGroup;

        float tempo = 1.0f;
        theMusic.pitch = tempo;
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / tempo);

        //-----------------------

        isPlaying = true;
        theMusic.Play();

        particleLight.intensity = 0.5f;

        noMisseslabel.SetActive(false);
        EnemySliceAnim.SetActive(false);
        Notecombo = 0;

        enemy = EnemyDataholder.GetComponent<EnemyDataHolder>().enemies[enemyid];
        EnemyHPText.text = enemy.GetComponent<Enemy>().enemyHP + "/" + enemy.GetComponent<Enemy>().CurrentEnemyHP;

        GoldText.text = "Gold: " + PlayerPrefs.GetInt("Gold");

        playerHPText.text = PlayerPrefs.GetInt("CurrentPlayerHP") + "/" + PlayerPrefs.GetInt("PlayerHP");
        //Resize all Healthbars
        //Resize Player Healthbar
        float currHP = PlayerPrefs.GetInt("CurrentPlayerHP");
        float maxHP = PlayerPrefs.GetInt("PlayerHP");
        healthbar.transform.localScale = new Vector3(3.85f * (currHP / maxHP), 0.2125f, 1f);


        resize = 1.925f * (1 - currHP / maxHP);

        //Resize Enemy HP bar
        float EnemycurrHP = enemy.GetComponent<Enemy>().CurrentEnemyHP;
        float EnemyHP = enemy.GetComponent<Enemy>().enemyHP;

        Enemyhealthbar.transform.localScale = new Vector3(3.85f * (EnemycurrHP / EnemyHP), 0.2125f, 1f);
        //Resize Player XP

        


        float cur = PlayerPrefs.GetInt("CurrXP");
        float max = Dataholder.GetComponent<Preferences>().xpLevels[PlayerPrefs.GetInt("XpLevel") - 1];
    
        EXPbar.transform.localScale = new Vector3(3.85f * (cur / max), 0.2125f, 1.0f);
        resize = 1.925f * (1 - cur / max);

        LevelText.text = "Lvl " + PlayerPrefs.GetInt("XpLevel");

        //Reset Note Counters
        Pcount = 0;
        Hcount = 0;
        Gcount = 0;
        Mcount = 0;

        if(enemyid != 4){
            Invoke("DelayedEnemy", 1f);
        }
    }
    void DelayedEnemy(){
        enemy.SetActive(true);

        Debug.Log("Load Enemy:" + enemyid);
    }

    void DelayedExplosion(){
        explosionParticle.SetActive(false);
        PlayerexplosionParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Help Panel
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Toggle the active state of the GameObject
            HelpPanel.SetActive(true);
            Time.timeScale = 0.0f;
            theMusic.Pause();
            isinPauseMenu = true;
        }
        else if(isinPauseMenu){
            HelpPanel.SetActive(false);
            Time.timeScale = 1.0f;
            theMusic.UnPause();
            isinPauseMenu = false;
        }

        //Speed Up Dev Setting
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // Toggle the active state of the GameObject
            Time.timeScale = 5.0f;

            //Music Speed Correction
            var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("PitchMixer");
            theMusic.outputAudioMixerGroup = pitchBendGroup;

            float tempo = 5.0f;
            theMusic.pitch = tempo;
            pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / tempo);
        }
        else
        {
            Time.timeScale = 1.0f;

            //Music Speed Correction
            var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("PitchMixer");
            theMusic.outputAudioMixerGroup = pitchBendGroup;

            float tempo = 1.0f;
            theMusic.pitch = tempo;
            pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / tempo);
        }

        //Equipment
        if (Input.GetKeyDown(KeyCode.Space) && EquipHits >= EquipHitsFull){
                //Use Equipment
                Debug.Log("Equipment Use");
                EquipCharge.transform.position -= new Vector3(0.0f, 50f, 0.0f);
                EquipCharge.transform.localScale -= new Vector3(0f, 1f, 0f);
                EquipHits = 0;
                

                EquipUseAudio.Play();
                isFocused = true;
                //Trigger Equipment based off equipment ID

                switch (Dataholder.GetComponent<Preferences>().EquipmentID){
                    case 0:
                        //Star Sticker
                        isastar = true;
                        break;
                    case 1:
                        //Crystallized Stopwatch
                        Time.timeScale = 0.5f;
                        //Pitch Bend Song to 50%
                        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("PitchMixer");
                        theMusic.outputAudioMixerGroup = pitchBendGroup;

                        float tempo = 0.5f;
                        theMusic.pitch = tempo;
                        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / tempo);
                        break;
                    case 2:
                    //Christophers face
                        enemy.GetComponent<Enemy>().CurrentEnemyHP -= 999;
                        enemy.GetComponent<Enemy>().Death();
                        Invoke("DelayedFightPanel", 1f);
                        break;
                }


                

        }
        if(isFocused){
            focusedTime -= Time.deltaTime;
            if(focusedTime <= 0f){
                //End Cooldown
                
                Time.timeScale = 1.0f;
                isFocused = false;
                focusedTime = 2.5f;

                //End Equipment States
                switch (Dataholder.GetComponent<Preferences>().EquipmentID){
                    case 0:
                        //Star Sticker
                        isastar = false;
                        break;
                    case 1:
                        //Crystallized Stopwatch
                        //Pitch Bend Song
                        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("PitchMixer");
                        theMusic.outputAudioMixerGroup = pitchBendGroup;

                        float tempo = 1.0f;
                        theMusic.pitch = tempo;
                        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / tempo);
                        break;
                }

            }
        }
    }

    public void SwitchSide(){
        if(isMyTurn){
            //Deal Dmg to enemy
            DmgCounter += Dataholder.GetComponent<Preferences>().additionalDMG;
            //Add Poison
            DmgCounter += poisonCounter;

            enemy.GetComponent<Enemy>().CurrentEnemyHP -= DmgCounter;
            EnemyHPText.text = enemy.GetComponent<Enemy>().CurrentEnemyHP + "/" + enemy.GetComponent<Enemy>().enemyHP;
            Debug.Log("I did " + DmgCounter);

            //Fire off projectile
            //hitParticle.SetActive(true);
            hitParticle.GetComponent<shootOrb>().isShooting = true;

            //Explosion Particle
            explosionParticle.SetActive(true);
            Invoke("DelayedExplosion", 1f);

            //create dmgText
            Debug.Log("creating number...");

            Transform parentTransform = dmgTextParent.transform.parent;

            GameObject dmgTextPart = Instantiate(dmgTextParent, parentTransform);
            dmgTextPart.SetActive(true);
            Transform firstChild = dmgTextPart.transform.GetChild(0);
            firstChild.GetComponent<dmgNumberAddForce>().Changetext(DmgCounter);


            DmgCounter = 0;

            attackHitSong.Play();


            //Resize Enemy HP bar
            float EnemycurrHP = enemy.GetComponent<Enemy>().CurrentEnemyHP;
            float EnemyHP = enemy.GetComponent<Enemy>().enemyHP;

            if(EnemycurrHP / EnemyHP <= 0){
                Enemyhealthbar.transform.localScale = new Vector3(0, 0.2125f, 1f);
            }
            else{
                Enemyhealthbar.transform.localScale = new Vector3(3.85f * (EnemycurrHP / EnemyHP), 0.2125f, 1f);
            }


            //If Enemy has died, end battle give gold etc. 
            //--------------------------------------------
            if(enemy.GetComponent<Enemy>().CurrentEnemyHP <= 0){

                //IF ENEMY IS NOT DA BOSS
                if(enemyid != 3){

                enemy.GetComponent<Enemy>().Death();

                //Give Gold
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + enemy.GetComponent<Enemy>().goldDropped);
                
                GoldText.text = "Gold: " + PlayerPrefs.GetInt("Gold");
                
                //Give XP/Level Up
                PlayerPrefs.SetInt("CurrXP", PlayerPrefs.GetInt("CurrXP") + enemy.GetComponent<Enemy>().xpDropped);

                if(PlayerPrefs.GetInt("CurrXP") > Dataholder.GetComponent<Preferences>().xpLevels[PlayerPrefs.GetInt("XpLevel") - 1]){
                    //If we are leveling up
                    PlayerPrefs.SetInt("CurrXP", PlayerPrefs.GetInt("CurrXP") - Dataholder.GetComponent<Preferences>().xpLevels[PlayerPrefs.GetInt("XpLevel") - 1]);
                    PlayerPrefs.SetInt("XpLevel", PlayerPrefs.GetInt("XpLevel") + 1);

                    //Level up Bonuses
                    Dataholder.GetComponent<Preferences>().additionalDMG += 1;

                    PlayerPrefs.SetInt("PlayerHP", PlayerPrefs.GetInt("PlayerHP") + 4);
                    PlayerPrefs.SetInt("CurrentPlayerHP", PlayerPrefs.GetInt("CurrentPlayerHP") + 4);

                }
                PlayerPrefs.Save();


                //Do HP Changes
                EnemyHPText.text = "0/" + enemy.GetComponent<Enemy>().enemyHP;
                Debug.Log("Lvl " + PlayerPrefs.GetInt("XpLevel") + ", xp: " + PlayerPrefs.GetInt("CurrXP"));
                float cur = PlayerPrefs.GetInt("CurrXP");
                float max = Dataholder.GetComponent<Preferences>().xpLevels[PlayerPrefs.GetInt("XpLevel") - 1];

                
                EXPbar.transform.localScale = new Vector3(3.85f * (cur / max), 0.2125f, 1.0f);

                LevelText.text = "Lvl " + PlayerPrefs.GetInt("XpLevel");

                //reset tracker
                activeHitText.text = "";

                theMusic.Pause();
                
                //Gold/XP text
                GoldReceivedText.text = enemy.GetComponent<Enemy>().goldDropped + " Gold";
                EXPReceivedText.text = enemy.GetComponent<Enemy>().xpDropped + " EXP";

                //Check if player gets any perfect rewards
                if(Mcount == 0){
                    noMisseslabel.SetActive(true);
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 20);
                }

                Invoke("DelayedFightPanel", 1f);
                
            }
            
            //IF WE KILLED THE BOSS SLIME
            else{
                //Kill slime
                enemy.SetActive(false);

                //Play ~20 second animation (somehow)
                
                //Do Shadow Animation
                theMusic.Pause();
                //8 seconds later play 
                Dataholder.GetComponent<EnemyData>().enemyID = 4;
                Dataholder.GetComponent<Preferences>().isInDragonBoss = true;

                SceneManager.LoadScene(2);

  
                }


            }

        }        
        else{

            //Deal dmg to player
            Debug.Log(PlayerPrefs.GetInt("CurrentPlayerHP"));
            PlayerPrefs.SetInt("CurrentPlayerHP", PlayerPrefs.GetInt("CurrentPlayerHP") - EnemyDmgCounter);
            Debug.Log("Enemy did: " + EnemyDmgCounter + "New Hp: " + PlayerPrefs.GetInt("CurrentPlayerHP"));
            PlayerPrefs.Save();

            attackHitSong.Play();

            enemyHitParticle.GetComponent<EnemyShootOrb>().isShooting = true;

            EnemySliceAnim.SetActive(true);
            Invoke("StopEnemySlash", 0.7f);

            //Explosion Particle
            PlayerexplosionParticle.SetActive(true);
            Invoke("DelayedExplosion", 1f);

            //Create PlayerHitText
            Transform parentTransform = PlyrdmgTextParent.transform.parent;

            GameObject PlyrdmgTextPart = Instantiate(PlyrdmgTextParent, parentTransform);
            PlyrdmgTextPart.SetActive(true);
            Transform firstChild = PlyrdmgTextPart.transform.GetChild(0);
            firstChild.GetComponent<dmgNumberAddForce>().Changetext(EnemyDmgCounter);

            //Resize Player Healthbar
            //------------------------
            float currHP = PlayerPrefs.GetInt("CurrentPlayerHP");
            float maxHP = PlayerPrefs.GetInt("PlayerHP");
            healthbar.transform.localScale = new Vector3(3.85f * (currHP / maxHP), 0.2125f, 1f);

            //Change Text
            playerHPText.text = PlayerPrefs.GetInt("CurrentPlayerHP") + "/" + PlayerPrefs.GetInt("PlayerHP");
            //Reset
            EnemyDmgCounter = 0;



            //If Player has died
            if(currHP <= 0){

                //Play death animation
                Invoke("DelayedDeathPanel", 1f);
            }
        }


        isMyTurn = !isMyTurn;

    }


    public void DelayedDeathPanel(){
        PlayerDeathPanel.SetActive(true);
        Time.timeScale = 0;
        
    }
    public void StopEnemySlash(){
        EnemySliceAnim.SetActive(false);
    }

    public void DragonShadow(){
        DragShadow.SetActive(true);
    }
    public void DragonFlyDown(){
        FakeDragon.SetActive(true);
    }
    public void DragonRoar(){
        dragonroar.Play();
    }
    public void StartDragonFight(){
            

            //Reset Conveyor
            FakeDragon.SetActive(false);

            //Enter Real Enemy Dragon
            enemy = EnemyDataholder.GetComponent<EnemyDataHolder>().enemies[4];
            
            enemy.SetActive(true);
            //Resize Enemy HP bar
            float EnemycurrHP = enemy.GetComponent<Enemy>().CurrentEnemyHP;
            float EnemyHP = enemy.GetComponent<Enemy>().enemyHP;

            if(EnemycurrHP / EnemyHP <= 0){
                Enemyhealthbar.transform.localScale = new Vector3(0, 0.2125f, 1f);
            }
            else{
                Enemyhealthbar.transform.localScale = new Vector3(3.85f * (EnemycurrHP / EnemyHP), 0.2125f, 1f);
            }

        }

    public void DelayedFightPanel(){
        winSong.Play();

        endfightPanel.SetActive(true);
        endfightPanel.GetComponent<FightMenu>().Enable();


        PerfectText.text = "- " + Pcount;
        GoodText.text = "- " + Gcount;
        HitText.text = "- " + Hcount;
        MissText.text = "- " + Mcount;

        Time.timeScale = 0;
    }

    public void NoteHit(){
        //change player sprite
        player.GetComponent<SpriteRenderer>().sprite = characterHitSprites[characterFrame];
        Debug.Log(characterHitSprites[characterFrame]);


        if (isMyTurn){
            //MyTurn
            DmgCounter += 1;

            //Add Dmg if Focused
            if(isastar){
                //Apply Equipment Ability
                DmgCounter += 1;
            }

            //UPDATE PLAYER EQUIP
            else{
                if(EquipHits < EquipHitsFull){

                    EquipImg.GetComponent<Image>().sprite = EquipInactive; 
                    EquipActiveLight.enabled = false;
                    EquipHits += 1;

                    //Move and scale equip meter based on equip charge
                    EquipCharge.transform.localScale += new Vector3(0.0f, 1f / (EquipHitsFull), 0.0f);
                    EquipCharge.transform.position += new Vector3(0.0f, 45f / EquipHitsFull, 0.0f);  
                    
                }
                else{
                    EquipImg.GetComponent<Image>().sprite = Equipactive;
                    EquipActiveLight.enabled = true;
                }
            }
            
            //PARTICLES
            hitParticle.GetComponent<shootOrb>().growOrb();
            particleLight.intensity += 1;


            //Poison
            if (Dataholder.GetComponent<Preferences>().baseAddPoison > 0){
                float rnd = Random.Range(0f, 1f);

                if(rnd <= Dataholder.GetComponent<Preferences>().baseAddPoison / 10.0f){
                    poisonCounter += 1;
                }
            }

        }
        else{
            //EnemyTurn
            enemyHitParticle.GetComponent<EnemyShootOrb>().showOrb();
        }


        if(currentMultiplier - 1 < multiplierThresholds.Length){
            multiplierTracker++;
            //If we hit next multiplier threshhold
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker){
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
    multiplierText.text = "Multiplier: " + currentMultiplier;
    scoreText.text = "Score: " + currentScore;

    }

    public void NormalHit(){
        //change player sprite
        
        
        //update values
        currentScore += scorePerNote * currentMultiplier;
        Hcount += 1;
        Notecombo += 1;
        comboText.text = Notecombo.ToString();
        activeHitText.text = "Hit";
        if(!isMyTurn){
            EnemyDmgCounter += 1;
        }
        NoteHit();
    }
        public void GoodHit(){
        //change player sprite
        
        //update values
        currentScore += scorePerGoodNote * currentMultiplier;
        Gcount += 1;
        Notecombo += 1;
        comboText.text = Notecombo.ToString();
        activeHitText.text = "Good";
        NoteHit();
    }
        public void PerfectHit(){
        //change player sprite
        
        Debug.Log("char " + characterFrame);
        //update values
        currentScore += scorePerPerfectNote * currentMultiplier;
        Pcount += 1;
        Notecombo += 1;
        comboText.text = Notecombo.ToString();
        activeHitText.text = "Perfect";
        NoteHit();
    }
        public void NoteMissed(){
        Mcount += 1;
        activeHitText.text = "Miss";
        currentMultiplier = 1;
        multiplierTracker = 0;
        Notecombo = 0;
        comboText.text = Notecombo.ToString();

        if(!isMyTurn){
            EnemyDmgCounter += 2;
        }

        multiplierText.text = "Multiplier: " + currentMultiplier;
    }
}
