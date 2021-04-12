using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Unity.Entities;
using UnityEngine;

public class Patch : MonoBehaviour
{
	private void Start()
	{
		var harmony = new Harmony("com.needle.patchexception");
		Harmony.DEBUG = true;
		var t = typeof(Entity).Assembly.GetType("Unity.Entities.ComponentDependencyManager");
		Debug.Assert(t != null);
		var m = t.GetMethod("GetDependency");
		Debug.Assert(m != null);
		var prefix = GetType().GetMethod(nameof(Prefix), BindingFlags.Static | BindingFlags.NonPublic);
		Debug.Assert(prefix != null);
		harmony.Patch(m, new HarmonyMethod(prefix));
	}

	private static void Prefix()
	{
		// doing nothing
	}
}