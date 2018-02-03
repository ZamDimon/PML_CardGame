using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlaceCard))]
public class PlaceCardInspector : Editor {
	public override void OnInspectorGUI() {
		GUIStyle style = new GUIStyle ();
		style.fontSize = 28; style.richText = true; style.alignment = TextAnchor.UpperCenter;

		GUIStyle textStyle = new GUIStyle ();
		textStyle.fontSize = 14; textStyle.richText = true; textStyle.alignment = TextAnchor.UpperCenter;

		GUILayout.Label ("Card settings", style);

		PlaceCard script = (PlaceCard)target;
		EditorGUILayout.BeginVertical ("box"); {
			GUILayout.Label ("<b>Card settings:</b>", textStyle);

			script.cardType = (PlaceCard.CardType)EditorGUILayout.EnumPopup ("Card type:", script.cardType);
			script.battleCry = (PlaceCard.BattleCry)EditorGUILayout.EnumPopup ("Battlecry type:", script.battleCry);

			if(script.cardType == PlaceCard.CardType.Creature)
				script.StartStats = EditorGUILayout.IntField ("Stats:", script.StartStats);
			
			script.StatsObject = (GameObject)EditorGUILayout.ObjectField ("Stats object:", script.StatsObject, typeof (GameObject), true);
			script.tableObject = (GameObject)EditorGUILayout.ObjectField ("Table object:", script.tableObject, typeof(GameObject), true);
			EditorGUILayout.EndVertical();
		}

		if (script.battleCry != PlaceCard.BattleCry.Nothing) {
			EditorGUILayout.BeginVertical ("box");
			{
				GUILayout.Label ("<b>Skill:</b>", textStyle);

				switch (script.battleCry) {
				case PlaceCard.BattleCry.PowerUpAllCreatures:
					script.PowerUpAllCreaturesValue = EditorGUILayout.IntField ("Value:", script.PowerUpAllCreaturesValue);
					break;
			
				case PlaceCard.BattleCry.SummonCreatures:
					script.summonCardStats = EditorGUILayout.IntField ("Stats:", script.summonCardStats);
					script.summonCardObject = (GameObject)EditorGUILayout.ObjectField ("Card object:", script.summonCardObject, typeof(GameObject), false);
					script.summonTextObject = (GameObject)EditorGUILayout.ObjectField ("Text card object:", script.summonTextObject, typeof(GameObject), false);
					break;
				}

				EditorGUILayout.EndVertical ();
			}
		} else
			EditorGUILayout.LabelField ("It does nothing. Really.", textStyle);

		EditorGUILayout.BeginVertical ("box"); {
			GUILayout.Label ("<b>Animation settings:</b>", textStyle);
			script.animationSpeed = EditorGUILayout.FloatField ("Animation Speed:", script.animationSpeed);
			script.moveSpeed = script.animationSpeed;

			EditorGUILayout.EndVertical();
		}
	}
}
