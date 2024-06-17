using System;
using System.Collections;
using UnityEngine;

public class UIBattle : MonoBehaviour
{
  [SerializeField] private GameObject _prefabBattleCharacter;
  [SerializeField] private Transform _battleCharacterParent;
  [SerializeField] private Transform[] _playerSlots;
  [SerializeField] private Transform[] _enemySlots;

  void OnEnable()
  {
    BattleState.OnBattleRender += HandleBattleRender;
  }

  void OnDisable()
  {
    BattleState.OnBattleRender -= HandleBattleRender;
  }

  private void HandleBattleRender(BattleState battleState)
  {
    StartCoroutine(HandleBattleRenderCo(battleState));
  }

  private IEnumerator HandleBattleRenderCo(BattleState battleState)
  {
    yield return new WaitForEndOfFrame();
    foreach (Transform child in _battleCharacterParent)
    {
      if (!battleState.Exists(child.GetComponent<UIBattleCharacter>().UID))
      {
        Destroy(child.gameObject);
      }
    }
    int charIndex = 0;
    foreach (var bc in battleState.playerTeam.BattleCharacters)
    {
      UIBattleCharacter uiBattleCharacter = CreateOrReuseBattleCharacter(bc);
      uiBattleCharacter.transform.position = _playerSlots[charIndex].position;
      charIndex++;
    }
    charIndex = 0;
    foreach (var bc in battleState.enemyTeam.BattleCharacters)
    {
      UIBattleCharacter uiBattleCharacter = CreateOrReuseBattleCharacter(bc);
      uiBattleCharacter.transform.position = _enemySlots[charIndex].position;
      uiBattleCharacter.Flip = true;
      charIndex++;
    }
  }

  UIBattleCharacter CreateOrReuseBattleCharacter(BattleCharacter character)
  {
    UIBattleCharacter[] uiBattleCharacters = _battleCharacterParent.GetComponentsInChildren<UIBattleCharacter>();
    UIBattleCharacter uiBattleCharacter = Array.Find(uiBattleCharacters, x => x.UID == character.UID);
    if (uiBattleCharacter == null)
    {
      GameObject bcGO = Instantiate(_prefabBattleCharacter, _battleCharacterParent);
      uiBattleCharacter = bcGO.GetComponent<UIBattleCharacter>();
    }
    uiBattleCharacter.SetFromCharacter(character);
    uiBattleCharacter.name = character.name;
    return uiBattleCharacter;
  }
}
