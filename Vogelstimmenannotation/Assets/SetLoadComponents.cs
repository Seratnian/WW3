using UnityEngine;
using System.Collections;

public class SetLoadComponents : MonoBehaviour {
    private Tip tip;
    private Material LoadScreenImage;

	void Start () {
        // TODO load tip from externel resource
        Tip[] tips = new Tip[] { new Tip(Resources.Load("Images/ExampleImage") as Texture, "Hilfetext, \nmehrzeilig") };
        tip = tips[(int)(tips.Length * Random.value)];

        Texture Image = tip.Image;
        LoadScreenImage = Resources.Load("LoadScreenImage") as Material;
        LoadScreenImage.mainTexture = Image;

        Transform tipText = transform.GetChild(1);
        (tipText.GetComponent<TextMesh>() as TextMesh).text = tip.Text;


	}
	
	void OnDestroy () {
        LoadScreenImage.mainTexture = null;
	}
}
