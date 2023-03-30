using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPreload : MonoBehaviour
{

    [SerializeField] private GameObject preloadPrefab;
    [SerializeField] private int preloadAmount;

	// Use this for initialization
	void Start () {
		SimplePool.Preload(preloadPrefab, preloadAmount);
	}
	
}
