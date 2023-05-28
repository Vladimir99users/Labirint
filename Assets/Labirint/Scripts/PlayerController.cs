using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Данные игрока")]
    [SerializeField] private Transform _spawnPointer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Player _player;

    [Space]
    [Header("Настройка силы")]
    [SerializeField] private float force;


    [Space]
    [Header("Иговой худ")]
    [SerializeField] private Hud _hud;




    public bool IsMoved {get; set;}

    private void Start()
    {
        IsMoved = true;
        _player.OnFinished += Finished;
    }

    private void OnDisable ()
    {
        _player.OnFinished -= DisableMoved;
    }

    private void Finished()
    {
        DisableMoved();
        _hud.OpenMenu();
    }

    private void DisableMoved()
    {
        IsMoved = false;
    }

    

    // Update is called once per frame
    private void Update()
    {

        if(IsMoved == false) return;
        
        if(Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(Vector3.forward * force,ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(-Vector3.forward * force,ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector3.left * force,ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(-Vector3.left * force,ForceMode.Force);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ManipulateMap();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _player.gameObject.transform.position = _spawnPointer.position;
        }
    }

    private void ManipulateMap()
    {
        _hud.ViewWindow();
    }
}
