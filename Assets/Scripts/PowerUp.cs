using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 2.0f;

    [SerializeField]
    private int _powerUpId;

    //[SerializeField]
    private AudioManager _powerUpSound;

    // Start is called before the first frame update
    void Start()
    {
        _powerUpSound = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Game Boundaries - PowerUp X Location Based
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.3f, 9.3f), transform.position.y, 0);

        //PowerUp Moves Down
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        //Destroy if it falls off screen
        if (transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            //null check to not cause code to crash
            if (player != null)
            {
                //PowerUpID 0 - TripleShot, 1 - SPeed, 2 - Shield
                switch (_powerUpId)
                {
                    case 2:
                        player.ShieldEnable();
                        break;
                    case 1:
                        player.SpeedEnable();
                        break;
                    case 0:
                        player.TripleShotEnable();
                        break;
                }


                
            }
            _powerUpSound.PowerUpSound();
            Destroy(this.gameObject);
        }

        //if (other.CompareTag("Laser"))
        //{
        //    Debug.Log(other.name);
        //    Destroy(other.gameObject);
        //    Destroy(this.gameObject);

        //}
    }
}
