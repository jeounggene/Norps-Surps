using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
	public SpriteRenderer connectionRenderer;

	public float horizon = 0;
	public buttonsMovement buttonsM1,buttonsM2, statsM1,statsM2, endGameM;

	public TextMeshProUGUI leftStats, rightStats, endTxt;

	public Transform center;
	public cameraMovement camera;
	public GameObject personPrefab, particles;
	public Transform norp,surp;

	public float norpPop = 0, norpIncome = 1, norpHappy = 100, norpWealth = 0, norpEnviron = 100;
	public float surpPop = 0, surpIncome = 1, surpHappy = 100, surpWealth = 0, surpEnviron = 100;

	public bool connection = true;

	public GameObject[] leftObjs, rightObjs;

	public float mode = 0; // -1 0 1
    
    void Start()
    {
    }

    
    void Update()
    {

		//leftStats.SetText ("Population: {0:0}\nIncome: ${1:2}/t\nHappiness: {2:0}%\n Wealth: {3:2}\nEnvironment: {4:0}",norpPop, norpIncome, norpHappy, norpWealth, norpEnviron);
		//rightStats.SetText ("Population: {1:0}\nIncome: ${2:2}/t\nHappiness: {3:0}%\n Wealth: {4:2}\nEnvironment: {5:0}",surpPop, surpIncome, surpHappy, surpWealth, surpEnviron);
		if (!(depression()||prosperity()||war()||environmentCrisis()))
		{
			
			if (Input.GetMouseButtonDown (0)) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 mousePos2D = new Vector2 (mousePos.x, mousePos.y);

				RaycastHit2D hit = Physics2D.Raycast (mousePos2D, Vector2.zero);
				if (hit.collider != null) {
					var gobj = hit.transform.gameObject;
					if (gobj.name == "Earth_1") {
						mode = -1;
						camera.moveTo (gobj.transform, true);
						buttonsM1.moveTo (false);
						buttonsM2.moveTo (true);
						statsM1.moveTo (false);
						statsM2.moveTo (true);
					} else if (gobj.name == "Earth_2") {
						mode = 1;
						camera.moveTo (gobj.transform, true);
						buttonsM1.moveTo (true);
						buttonsM2.moveTo (false);
						statsM1.moveTo (true);
						statsM2.moveTo (false);
					} else if (gobj.tag == "destroyableObject") {
						Instantiate (particles, gobj.transform.position, Quaternion.identity);
						Destroy(hit.transform.gameObject);
					} else {
						//mode = 0;
						//camera.moveTo (center, false);
					}
				} else if (mousePos2D.y >= horizon){
					mode = 0;
					camera.moveTo (center, false);
					buttonsM1.moveTo (true);
					buttonsM2.moveTo (true);
					statsM1.moveTo (true);
					statsM2.moveTo (true);
				}
			}
			leftStats.text = ("Population: " + norpPop+"\nIncome: $" + norpIncome+"/t\nHappiness: " + norpHappy+"%\nWealth: " + norpWealth+"\nEnvironment: " + norpEnviron);
			rightStats.text = ("Population: " + surpPop+"\nIncome: $" + surpIncome+"/t\nHappiness: " + surpHappy+"%\nWealth: " + surpWealth+"\nEnvironment: " + surpEnviron);
		}
		leftStats.text = ("Population: " + norpPop+"\nIncome: $" + norpIncome+"/t\nHappiness: " + norpHappy+"%\nWealth: " + norpWealth+"\nEnvironment: " + norpEnviron);
		rightStats.text = ("Population: " + surpPop+"\nIncome: $" + surpIncome+"/t\nHappiness: " + surpHappy+"%\nWealth: " + surpWealth+"\nEnvironment: " + surpEnviron);
    }



	void OnMouseClick(){
		Debug.Log ("click");
	}

	public void spawnPerson(){
		// change variables
		//spawn(personPrefab);
	}

	public void spawn(GameObject prefab){
		if (mode == -1) {
			Instantiate (prefab, norp.position,
				Quaternion.Euler (new Vector3 (0, 0, Random.Range(-180,180))), norp);
		} else if (mode == 1) {
			Instantiate (prefab, surp.position,
				Quaternion.Euler (new Vector3 (0, 0, 0)), surp);
		} else {
			Debug.Log ("LOL U GOT A FLIPPING ERROR");
		}
	}

	public void restart()
	{
		norpPop = 0; norpIncome = 1; norpHappy = 100; norpWealth = 0; norpEnviron = 100;
		surpPop = 0; surpIncome = 1; surpHappy = 100; surpWealth = 0; surpEnviron = 100;
		connection = true;
		endGameM.moveTo(true);

	}
	public void end(string msg){
		endTxt.text = msg;
		endGameM.moveTo(false);
	}

	public float calculateScore()
	{ 
		return norpEnviron + norpWealth + norpHappy + norpIncome + norpPop + surpEnviron + surpWealth + surpHappy + surpIncome + surpPop;
	}

 //ENDGAME
	public bool environmentCrisis()
	{
		if(norpEnviron < 10 || surpEnviron < 10)
		{
			Debug.Log("Environmental Catastrophe: Emissions killed Norp and Surp");
			return true;
		}
		return false;
	}
	public bool depression()
	{
		if(norpWealth<0 && surpWealth<0)
		{
			Debug.Log("Economic Catastrophe: The economies of both planets crashed and BOTH planets are doomed");
			return true;
		}
		return false;
	}
	public bool prosperity()
	{
		if(norpWealth>1000 && surpWealth>1000)
		{
			Debug.Log ("Economic Success: The economy propers, both planets are insanely rich");
			return true;
		}
		return false;
	}
	public bool war()
	{
		if((norpHappy<10 && surpHappy<10) || ((norpWealth<.01*surpWealth)||(surpWealth<.01*norpWealth)))
		{
			Debug.Log ("The War of the Worlds: One planet gained too much and the other planet got jealous");
			return true;
		}
		return false;
	}

	public void changeConnect()
	{
		connection = !connection;
	}

//What you choose
	public void addNorpMan()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		if(norpPop <= 75){
			norpPop+=15;
			norpHappy+=10;
		}
		else {
			Debug.Log("OVERPOPULATION");
			norpPop += 15;
			norpHappy-=25;
		}
		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addNorpHouse()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		norpPop+=5;
		norpHappy+=15;
		norpWealth-=10;
		norpEnviron-=10;

		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addNorpFactory()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		norpPop-=10;
		if (norpPop<0)
			norpPop = 0;
		norpHappy-=5;
		norpIncome+=15;
		norpWealth-=20;
		norpEnviron-=25;
		if (surpIncome>norpIncome && connection)
			norpIncome+=5;

		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addNorpTree()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		norpHappy+=5;
		norpWealth-=5;
		norpEnviron+=15;

		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addSurpMan()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		if(surpPop <= 75){
			surpPop+=15;
			surpHappy+=10;
		}
		if(surpPop > 75){
			Debug.Log("OVERPOPULATION");
			surpPop += 15;
			surpHappy-=15;
		}
		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addSurpHouse()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		surpPop+=5;
		surpHappy+=15;
		surpWealth-=10;
		surpEnviron-=10;

		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addSurpFactory()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		surpPop-=10;
		if (surpPop<0)
			surpPop = 0;
		surpHappy-=5;
		surpIncome+=15;
		surpWealth-=20;
		surpEnviron-=25;
		if (norpIncome>surpIncome && connection)
			surpIncome+=5;

		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}

	public void addSurpTree()
	{
		if(!(depression()||prosperity()||war()||environmentCrisis())){
		surpHappy+=5;
		surpWealth-=5;
		surpEnviron+=15;

		norpWealth += norpIncome;
		surpWealth += surpIncome;}
	}
	public void changeConnection(){
		connection = !connection;
		Color tmp = connectionRenderer.color;
		if (connection) {
			tmp.a = 1f;
		} else {
			tmp.a = 0f;
		}
		connectionRenderer.color = tmp;
	}

}
