using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;

public class EditorHelper : EditorWindow
{
	[MenuItem("Jay/EditorHelper")]
	public static void ShowTime()
	{
		EditorWindow.GetWindow<EditorHelper>();
	}
	private UnityEditor.Animations.AnimatorController animationController;
	private string NameToInsertGameObjectTo;
	private GameObject ObjectToInsert;

	[MenuItem("Hotkey/RunTitle #1", false, 100)]
	public static void RunTitle()
	{
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/title.unity", UnityEditor.SceneManagement.OpenSceneMode.Single);
		EditorApplication.isPlaying = true;
	}

	[MenuItem("Hotkey/RunMain #2", false, 100)]
	public static void GoMain()
	{
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/main.unity", UnityEditor.SceneManagement.OpenSceneMode.Single);
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/essential.unity", UnityEditor.SceneManagement.OpenSceneMode.Additive);
	}

	[MenuItem("Hotkey/RunExploration #3", false, 100)]
	public static void GoExploration()
	{
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/exploration.unity", UnityEditor.SceneManagement.OpenSceneMode.Single);
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/essential.unity", UnityEditor.SceneManagement.OpenSceneMode.Additive);
	}

	[MenuItem("Hotkey/RunEssential #4", false, 100)]
	public static void GoEssential()
	{
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/ready.unity", UnityEditor.SceneManagement.OpenSceneMode.Single);
		UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scene/essential.unity", UnityEditor.SceneManagement.OpenSceneMode.Additive);
	}

	private void OnGUI()
	{

		animationController = EditorGUILayout.ObjectField(animationController, typeof(UnityEditor.Animations.AnimatorController), false) as UnityEditor.Animations.AnimatorController;

		if (GUILayout.Button("Multiapply on Root"))
		{
			foreach (GameObject go in Selection.gameObjects)
			{
				PrefabUtility.ReplacePrefab(go, PrefabUtility.GetCorrespondingObjectFromSource(go), ReplacePrefabOptions.ConnectToPrefab);
			}
		}
		if (GUILayout.Button("CpCollider on Root"))
		{
			foreach (GameObject go in Selection.gameObjects)
			{
				foreach (BoxCollider bc in go.GetComponents<BoxCollider>())
				{
					BoxCollider cp = go.AddComponent<BoxCollider>();
					cp.size = bc.size;
					cp.center = bc.center;
					cp.isTrigger = true;
				}
			}
		}

		if (GUILayout.Button("Nuke Playerprefs"))
		{
			PlayerPrefs.DeleteAll();
		}

		if (GUILayout.Button("Room to visual"))
		{
			foreach (GameObject room in Selection.gameObjects)
			{
				GameObject visual = GameObject.Find(room.name + " (1)");
				visual.transform.position = room.transform.position;
				visual.transform.rotation = room.transform.rotation;

				List<GameObject> childList = new List<GameObject>();
				foreach (Transform child in visual.transform)
				{
					childList.Add(child.gameObject);
				}

				foreach (GameObject child in childList)
				{
					DestroyImmediate(child.gameObject);
				}
				childList.Clear();


				foreach (Transform child in room.transform)
				{
					childList.Add(child.gameObject);
				}
				foreach (GameObject child in childList)
				{
					child.transform.SetParent(visual.transform);
					foreach (MonoBehaviour script in child.GetComponentsInChildren<MonoBehaviour>())
					{
						DestroyImmediate(script);
					}
					foreach (Collider script in child.GetComponentsInChildren<Collider>())
					{
						DestroyImmediate(script);
					}
				}
			}
		}


		if (GUILayout.Button("Nuke Scripts"))
		{
			foreach (GameObject go in Selection.gameObjects)
			{
				foreach (MonoBehaviour script in go.GetComponentsInChildren<MonoBehaviour>())
				{
					DestroyImmediate(script);
				}
				foreach (Collider script in go.GetComponentsInChildren<Collider>())
				{
					DestroyImmediate(script);
				}
			}
		}


		NameToInsertGameObjectTo = EditorGUILayout.TextField("Name of GameObjects", NameToInsertGameObjectTo);
		ObjectToInsert = EditorGUILayout.ObjectField(ObjectToInsert, typeof(GameObject), true) as GameObject;
		if (GUILayout.Button("Insert Gameobject as children foreach gameobject with given name"))
		{
			foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
			{
				if (go.name == NameToInsertGameObjectTo)
				{
					GameObject obj = Instantiate(ObjectToInsert);
					obj.name = ObjectToInsert.name;
					obj.transform.SetParent(go.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localRotation = Quaternion.identity;
				}
			}
		}

		if (GUILayout.Button("Insert Gameobject as children foreach selected gameobject"))
		{
			foreach (GameObject go in Selection.gameObjects)
			{
				GameObject obj = Instantiate(ObjectToInsert);
				obj.name = ObjectToInsert.name;
				obj.transform.SetParent(go.transform);
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localRotation = Quaternion.identity;
			}
		}

		if (GUILayout.Button("Replace Gameobject as children foreach gameobject with given name"))
		{
			foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
			{
				if (go.name == NameToInsertGameObjectTo)
				{
					GameObject obj = Instantiate(ObjectToInsert);
					obj.name = ObjectToInsert.name;
					Transform child = go.transform.Find(obj.name);
					while (child != null)
					{
						DestroyImmediate(child.gameObject);
						child = go.transform.Find(obj.name);
					}

					obj.transform.SetParent(go.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localRotation = Quaternion.identity;
				}
			}
		}

		if (GUILayout.Button("AnimatorStateEnterExit"))
		{
			foreach (var script in animationController.GetBehaviours<StateMachineBehaviour>())
			{
				DestroyImmediate(script, true);
			}
			foreach (var layer in animationController.layers)
			{
				foreach (var state in layer.stateMachine.states)
				{
					string stateName = state.state.name;
					AnimationStateTranslator.StateMessage message = new AnimationStateTranslator.StateMessage();
					message.onEnter = "EnterState";
					message.onExit = "ExitState";
					message.stateFullName = stateName;

					AnimationStateTranslator translator = animationController.AddEffectiveStateMachineBehaviour<AnimationStateTranslator>(state.state, 0);
					translator.OutgoingMessages = new AnimationStateTranslator.StateMessage[1]  {
						message
					};
				}
			}
		}



	}

}
