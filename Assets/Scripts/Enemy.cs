using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    //[SerializeField]
    private Player _player;
    //[SerializeField]    
    private AudioManager _explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explosionSound = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Game Boundaries - Enemy X Location Based
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.5f, 9.5f), transform.position.y, 0);

        //Enemy Moves Down
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //Respawn at Top in random location
        if (transform.position.y < -7.5f)
        {
            transform.position = new Vector2(Random.Range(-9.5f, 9.5f), 7.5f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            //Player player = other.transform.GetComponent<Player>();
            //null check to not cause code to crash
            if (_player != null)
            {
                _player.DamageControl(1);
            }
            DeathAnimation();
            Destroy(this.gameObject, 2.25f);
        }

        if (other.CompareTag("Laser"))
        {
            Debug.Log(other.name);
            if (_player != null)
            {
                _player.UpdateScore(10);
            }
            DeathAnimation();
            Destroy(other.gameObject);
            Destroy(this.gameObject,2.25f);

        }
    }

    private void DeathAnimation()
    {
        _enemySpeed = 0f;
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<Animator>().SetTrigger("OnEnemyDeath");
        _explosionSound.ExplosionSound();
    }
}
