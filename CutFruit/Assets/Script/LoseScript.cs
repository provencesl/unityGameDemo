using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour {

	void Start () 
    {
        Button btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(ReStart);
	}

    void ReStart()  
        {
            SceneManager.LoadScene(1);
        }
}
