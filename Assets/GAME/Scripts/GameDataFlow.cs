using System.Collections.Generic;
using System.Threading.Tasks;
using DapperLabs.Flow.Sdk;
using DapperLabs.Flow.Sdk.Cadence;
using DapperLabs.Flow.Sdk.DataObjects;
using DapperLabs.Flow.Sdk.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataFlow", menuName = "Scriptable Objects/GameDataFlow")]
public class GameDataFlow : ScriptableObject
{
  public CadenceScriptAsset getMinionsScript;
  public List<Character> characters = new List<Character>();

  [ContextMenu("GetMinions")]
  public async Task Init()
  {
    FlowSDK.Init(new FlowConfig
    {
      NetworkUrl = FlowConfig.TESTNETURL,
      Protocol = FlowConfig.NetworkProtocol.HTTP
    });
    FlowScriptResponse response = await Scripts.ExecuteAtLatestBlock(getMinionsScript.text);
    CadenceArray responseVal = (CadenceArray)response.Value;
    characters.Clear();
    foreach (CadenceBase value in responseVal.Value)
    {
      CadenceComposite composite = (CadenceComposite)value;
      CadenceString name = composite.CompositeFieldAs<CadenceString>("name");
      CadenceNumber tier = composite.CompositeFieldAs<CadenceNumber>("tier");
      CadenceNumber health = composite.CompositeFieldAs<CadenceNumber>("health");
      CadenceNumber attack = composite.CompositeFieldAs<CadenceNumber>("attack");
      CadenceString description = composite.CompositeFieldAs<CadenceString>("description");
      characters.Add(new Character
      {
        name = name.Value,
        tier = int.Parse(tier.Value),
        health = int.Parse(health.Value),
        attack = int.Parse(attack.Value),
        description = description.Value
      });

    }
  }
}
