using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
  public GameDataLocal m_gameDataLocal;

  public List<BattleState> m_BattleStates = new List<BattleState>();

  public int m_CurrentBattleStateIndex = 0;


  void Start()
  {
    Battle();
  }

  void Battle()
  {
    m_BattleStates = GameManager.Battle();
    StartCoroutine(BattleCoroutine());
  }



  [ContextMenu("TestBattle")]
  void TestBattle()
  {
    m_BattleStates.Clear();
    BattleAuto battleAuto = new BattleAuto();
    BattleCharacter boaris1 = new BattleCharacter(m_gameDataLocal.characters[0]);
    BattleCharacter grungus1 = new BattleCharacter(m_gameDataLocal.characters[1]);
    battleAuto.Battle(new Team(boaris1), new Team(grungus1));
    m_BattleStates = battleAuto.BattleStates;
    StartCoroutine(BattleCoroutine());
  }

  IEnumerator BattleCoroutine()
  {
    while (m_CurrentBattleStateIndex < m_BattleStates.Count)
    {
      m_BattleStates[m_CurrentBattleStateIndex].RenderBattle();
      m_CurrentBattleStateIndex++;
      yield return new WaitForSeconds(1.0f);
    }
    yield return null;
    m_CurrentBattleStateIndex = 0;
  }


  [ContextMenu("CreateTestBattle")]
  void CreateTestBattle()
  {
    Debug.Log("CreateTestBattle");
    BattleCharacter boaris1 = new BattleCharacter(m_gameDataLocal.characters[0]);
    BattleCharacter grungus1 = new BattleCharacter(m_gameDataLocal.characters[1]);
    BattleCharacter grungus2 = grungus1.Clone();
    BattleState battleState1 = new BattleState(new Team(boaris1), new Team(grungus1));
    m_BattleStates.Add(battleState1);
    grungus2.health = grungus2.health - 1;
    BattleState battleState2 = new BattleState(new Team(boaris1), new Team(grungus2));
    m_BattleStates.Add(battleState2);
  }

  [ContextMenu("RenderBattle")]
  void RenderBattle()
  {
    m_BattleStates[m_CurrentBattleStateIndex].RenderBattle();
  }
}
