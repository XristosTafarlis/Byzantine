using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// In-game script

public class PlayerScript : MonoBehaviour{
	[Header("References")]
	[SerializeField] Image healthBar;
	[SerializeField] GameObject bow;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip[] playerPainSounds;
	[Space(10)]
	public bool wonTheGame;	//Used fot text when level finishes
	public bool endTheGame;

	[Header("Variables")]
	public int damage = 5;
	public int life;
	int maxLife;
	GameObject[] enemys;

	void Start(){
		audioSource = GetComponent<AudioSource>();
		SetPlayerLife();
		maxLife = life;
	}

	void Update(){
		GameFinished();
		HealthRender();
		Death();
	}

	void HealthRender(){
		if(healthBar != null)
			healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)life / maxLife, 0.05f);
	}

	void SetPlayerLife(){
		if(LevelSystem.level == 1){
			life = 100;
		}else if(LevelSystem.level == 2){
			life = 110;
		}else if(LevelSystem.level == 3){
			life = 120;
		}else if(LevelSystem.level == 4){
			life = 130;
		}else if(LevelSystem.level == 5){
			life = 150;
		}else if(LevelSystem.level == 6){
			life = 180;
		}else if(LevelSystem.level == 7){
			life = 230;
		}else if(LevelSystem.level == 8){
			life = 310;
		}else if(LevelSystem.level == 9){
			life = 440;
		}else if(LevelSystem.level >= 10){
			life = 650;
		}
	}

	public void TakeDamage(int dmg){
		audioSource.PlayOneShot(playerPainSounds[Random.Range(0, playerPainSounds.Length)]);
		life -= dmg;
	}

	void GameFinished(){
		if(WaveSpawner.wavesFinished == true){
			WaveSpawner.wavesFinished = false;
			endTheGame = true;
			wonTheGame = true;
			XpWon();
			Invoke("MainMenu", 2f);
		}
	}

	void XpWon(){
		if(SceneManager.GetActiveScene().name == "Spaniae"){
			LevelSystem.xpOnWin = 17;
		}else if(SceneManager.GetActiveScene().name == "Italia Annonaria"){
			LevelSystem.xpOnWin = 18.53f;
		}else if(SceneManager.GetActiveScene().name == "Italia Suburbicaria"){
			LevelSystem.xpOnWin = 20.2f;
		}else if(SceneManager.GetActiveScene().name == "Illyricum"){
			LevelSystem.xpOnWin = 22.02f;
		}else if(SceneManager.GetActiveScene().name == "Dacia"){
			LevelSystem.xpOnWin = 24;
		}else if(SceneManager.GetActiveScene().name == "Macedonia"){
			LevelSystem.xpOnWin = 26.16f;
		}else if(SceneManager.GetActiveScene().name == "Thracia"){
			LevelSystem.xpOnWin = 28.51f;
		}else if(SceneManager.GetActiveScene().name == "Quaestura Exercitus"){
			LevelSystem.xpOnWin = 31.08f;
		}else if(SceneManager.GetActiveScene().name == "Pontica"){
			LevelSystem.xpOnWin = 33.87f;
		}else if(SceneManager.GetActiveScene().name == "Asiana"){
			LevelSystem.xpOnWin = 36.92f;
		}else if(SceneManager.GetActiveScene().name == "Oriens"){
			LevelSystem.xpOnWin = 40.25f;
		}else if(SceneManager.GetActiveScene().name == "Aegyptus"){
			LevelSystem.xpOnWin = 43.87f;
		}else if(SceneManager.GetActiveScene().name == "Africa"){
			LevelSystem.xpOnWin = 47.82f;
		}else if(SceneManager.GetActiveScene().name == "Taginae"){
			MainMapCanvasScript.hasWonOstrogoths = true;
			LevelSystem.xpOnWin = 75;
		}else if(SceneManager.GetActiveScene().name == "Dara"){
			MainMapCanvasScript.hasWonSassanids = true;
			LevelSystem.xpOnWin = 90;
		}
	}

	void MainMenu(){
		SceneManager.LoadScene(0);
	}

	void Death(){
		if(life <= 0){
			endTheGame = true;
			wonTheGame = false;

			bow.GetComponent<Bow>().enabled = false;

			enemys = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject _enemy in enemys){ //Freeze enemies
				_enemy.GetComponent<Animator>().SetBool("EnemyIsStill", true);
				_enemy.GetComponent<Animator>().SetBool("EnemyAttacks", false);
				_enemy.GetComponent<Enemy>().enabled = false;
			}
			Invoke("MainMenu", 2f);
		}
	}
}