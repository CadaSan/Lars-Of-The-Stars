using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 2f;

    //[SerializeField]
    private Player _player;

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
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
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
            Destroy(this.gameObject, 2.20f);
        }

        if (other.CompareTag("Laser"))
        {
            Debug.Log(other.name);
            if (_player != null)
            {
                _player.UpdateScore(2);
            }
            DeathAnimation();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.20f);

        }
    }

    private void DeathAnimation()
    {
        //_rotateSpeed = 0f;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<Animator>().SetTrigger("OnAsteroidDeath");
        _explosionSound.ExplosionSound();
    }
}
