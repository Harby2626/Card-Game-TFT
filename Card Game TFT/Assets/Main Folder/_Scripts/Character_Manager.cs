using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ! DIDN'T TESTED YET
class Character_Manager : MonoBehaviour
{
    // * Singleton
    private static Character_Manager instance;
    public static Character_Manager Instance
    {
        get => instance == null
            ? (instance = FindObjectOfType<Character_Manager>())
            : instance;
    }
    public List<PlayerCharacter> playerCharacters;
    public List<EnemyWave> enemyWaves;
    public List<EnemyCharacter> aliveEnemyCharacters { get => enemyWaves[currentWaveIndex].aliveEnemyCharacters; }
    private int currentWaveIndex;

    void Start()
    {
        currentWaveIndex = 0;
        SpawnWave();
    }
    public Character RequestTarget(CharacterType ownerType)
    {
        /** 
        * * This function will be called in 'Character' class
        * * finds target that Character requested
        */
        if (aliveEnemyCharacters.Count == 0) return null;
        Character target = null;
        if (ownerType == CharacterType.NONE) return null;
        else if (ownerType == CharacterType.PLAYER)
        {
            /** 
            * * if target request cames from a player,
            * * following code warks which selects target
            * * from alive enemy characters
            */
            target = aliveEnemyCharacters.First();
            foreach (Character character in aliveEnemyCharacters)
            {
                if (FindTargetStretegy(target, character)) target = character;
            }
        }
        else
        {
            /** 
            * * if target request cames from an enemy,
            * * following code warks which selects target
            * * from player characters
            */
            target = playerCharacters.First();
            foreach (Character character in playerCharacters)
            {
                if (FindTargetStretegy(target, character)) target = character;
            }
        }
        return target;
    }

    private bool FindTargetStretegy(Character target, Character character)
    {
        // ! the strategy is to find target is finding the lowest health
        // ? can be change in the future
        return target.Health < character.Health;
    }

    private void SpawnWave()
    {
        // todo add delay
        /** 
        * * This initiates new wave
        * * If wave index exceeds or equals to wave count
        * * then it means that game ended
        * * if not then continue initiating the wave 
        * * by spawnşig the enemies in new wave
        * * then increments wave index for next round
        */
        if (currentWaveIndex >= enemyWaves.Count)
        {
            return;
        }

        enemyWaves[currentWaveIndex].SpawnEnemies();

        currentWaveIndex++;
    }

    public void CheckWaveEnded()
    {
        /** 
        * * Checking if there are any enemy characters left in this wave
        * * If not then initiate new wave
        */
        if (aliveEnemyCharacters.Count == 0)
        {
            SpawnWave();
        }
    }
    public void DidPlayerFailed()
    {
        /** 
        * * Checking if there are any player characters left
        * * If not then show alert dialog (Try Again?)
        */
        if (playerCharacters.Count == 0)
        {
            // todo player fail
        }
    }
}