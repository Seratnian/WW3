using UnityEngine;
using System.Collections;
using Assets;
using System.Reflection;

public class DBInteraction : MonoBehaviour {
    public static readonly string URI = "localhost/ww3/change.php";

    public static Player player = new Player();
    

	// Use this for initialization
	void Start () {
	    // TEST: add player
        //AddPlayer("Testname " + (int)(Random.value * 1000));
        //Debug.Log(player_id);

        // TEST: remove player
        //RemovePlayer(11);
	}

    
	
	// Update is called once per frame
	void Update () {
        Debug.Log(player.id);
	}

    void AddPlayer(string name)
    {
        StartCoroutine(GetPlayerID(GenerateRequest(
            new string[] { "task", "add_player", "player_name", name }
        )));
    }

    void RemovePlayer(int id)
    {
        GenerateRequest(
            new string[] { "task", "remove_player", "player_id", "" + id }
        );
    }

    void AddBird(string name)
    {
        GenerateRequest(
            new string[] { "task", "add_bird", "bird_name", name }
        );
    }

    void RemoveBird(int id)
    {
        GenerateRequest(
            new string[] { "task", "remove_bird", "bird_id", "" + id }
        );
    }

    void AddBirdToPlayer(int player_id, int bird_id)
    {
        GenerateRequest(
            new string[] { "task", "add_bird_to_player", "player_id", "" + player_id, "bird_id", "" + bird_id }
        );
    }

    void AddRandomBirdsToPlayer(int player_id, int count)
    {
        GenerateRequest(
            new string[] { "task", "add_random_birds_to_player", "player_id", "" + player_id, "count", "" + count }
        );
    }

    void AddMelody(string melody_file)
    {
        GenerateRequest(
            new string[] { "task", "add_melody", "melody_file", melody_file }
        );
    }

    void RemoveMelody(int melody_id)
    {
        GenerateRequest(
            new string[] { "task", "remove_melody", "melody_id", "" + melody_id }
        );
    }

    void AddAnnotation(int player_id, int melody_id )
    {
        GenerateRequest(
            new string[] { "task", "add_annotation", "player_id", "" + player_id, "melody_id", "" + melody_id }
        );
    }

    void RemoveAnnotation(int annotation_id)
    {
        GenerateRequest(
            new string[] { "task", "remove_annotation", "annotation_id", "" + annotation_id }
        );
    }

    void SetAnnotation(int annotation_id, string status)
    {
        GenerateRequest(
            new string[] { "task", "set_annotation", "annotation_id", "" + annotation_id, "status", status }
        );
    }

    WWW GenerateRequest(string[] parameters)
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i <= parameters.Length / 2; i += 2)
        {
            form.AddField(parameters[i], parameters[i + 1]);
        }
        WWW request = new WWW(URI, form);
        return request;
    }

    IEnumerator GetPlayerID(WWW www)
    {
        yield return www;

        if (www.error == null)
            player.id = int.Parse(www.text);
        else
            Debug.Log("error" + www.error);
    }

    IEnumerator GetBirdList(WWW www)
    {
        yield return www;

        if (www.error == null)
            player.SetBirds(www.text);
        else
            Debug.Log("error" + www.error);
    }


}
