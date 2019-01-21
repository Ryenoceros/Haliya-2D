using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour
{
	public int points;

	public Text coinsText;

    // Update is called once per frame
    void Update()
    {
        coinsText.text = ("" + points);
    }
}
