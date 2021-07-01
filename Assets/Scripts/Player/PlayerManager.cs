using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] CapsuleCollider _capsCollider;
    [SerializeField] float _speed;
    [SerializeField] float _increaseFootSize;
    [SerializeField] GameObject _playerCharacter;

    [SerializeField] Camera _cam;
    [SerializeField] float _camOffset;

    [SerializeField] GameObject[] _foots;
    float _currentFootSize;
    float _camInitialHeight;

    float _initialCapsHeight;
    bool _canMove;


    // Use this for initialization
    void Start()
    {
        //Store initial cam height
        _camInitialHeight = _cam.transform.position.y;
        //Sotre initial capsule height
        _initialCapsHeight = _capsCollider.height;
        //start moving
        StartMoving();
    }

    public void StartMoving()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_canMove)
        {
            if(Input.GetMouseButton(0))
            {
                _canMove = true;
                //Change animator's parameter value
                _playerCharacter.GetComponent<Animator>().SetBool("isWalking", true);
            }

            return;
        }
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * swerveSpeed * Mathf.Deg2Rad;
            transform.position += new Vector3(rotX, 0, 0);
        }

#elif UNITY_ANDROID

		if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved )
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			float rotX = touchDeltaPosition.x* swerveSpeed/18* Mathf.Deg2Rad;
            transform.position += new Vector3(rotX, 0,0);
		}
        
#endif
        //Store player's pos
        Vector3 tempPos = transform.position;
        //clamp so we don't fall on left or right
        tempPos.x = Mathf.Clamp(tempPos.x, -2, 2);
        //set the clamped values back
        transform.position = tempPos;
        //Move forward
        transform.Translate(0, 0, Time.deltaTime * _speed);
        //Use player's z pos for camera pos
        _cam.transform.position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, transform.position.z - _camOffset);



        UIManager.Instance.UpdateProgressionBar(1.0f - Vector3.Distance(transform.position, GameManager.Instance.EndPos) / GameManager.Instance.InitialDistanceToEnd);
        
    }

    void ChangeColliderSize()
    {
        //Change capsule height
        _capsCollider.height = _initialCapsHeight + _currentFootSize;
        //Change the center of the capsule
        _capsCollider.center = new Vector3(0, _capsCollider.height / 2.0f, 0);
    }

    void ChangeFootShoeSize()
    {
        for (int i = 0; i < _foots.Length; i++)
        {
            _foots[i].transform.localScale = new Vector3(1,1, _currentFootSize*10);
        }
    }

    void ChangePlayerHeight()
    {
        //Store the character pos
        Vector3 pos = _playerCharacter.transform.position;
        //Set the modified value back
        _playerCharacter.transform.position = new Vector3(pos.x, transform.position.y + _currentFootSize, pos.z);
    }

    void ChangeCamHeight()
    {
        //Store the cam pos
        Vector3 pos = _cam.transform.position;
        //Change the pos.y
        pos.y = transform.position.y + _camInitialHeight + _currentFootSize;
        //Set the modified value back to cam
        _cam.transform.position = pos;
    }

    public void CutShoes(int platformHeight)
    {
        //Calculate the value we need to cut
        float footSizeToCut = platformHeight / _increaseFootSize;
        //Decrease foot size by the value we calculated
        _currentFootSize -= footSizeToCut * _increaseFootSize;
        Debug.Log(_currentFootSize);
        if (_currentFootSize >= 0)
        {

            //Update the position of the player
            transform.position = new Vector3(transform.position.x, transform.position.y + platformHeight + _currentFootSize, transform.position.z);
            //Call the methods to change collider, foot shoe size, player height and cam height
            ChangeColliderSize();
            ChangeFootShoeSize();
            ChangePlayerHeight();
            ChangeCamHeight();
        }
        else
        {
            Fail();
        }

    }

    public void IncreaseShoe()
    {
        //Increase the current foot size by default value
        _currentFootSize += _increaseFootSize;
        //Call the methods to change collider, foot shoe size, player height and cam height
        ChangeColliderSize();
        ChangeFootShoeSize();
        ChangePlayerHeight();
        ChangeCamHeight();
    }

    public void Finish()
    {
        //Stop moving the character
        StopMoving();
        //Change animation to "Victory"
        _playerCharacter.GetComponent<Animator>().Play("Victory");
        //Rotate the player character by 180 degrees to face the camera
        _playerCharacter.transform.localEulerAngles += new Vector3(0, 180, 0);
        //Activate Success UI
        UIManager.Instance.ActivateSuccessMenu();
    }

    public void Fail()
    {
        //Stop moving the character
        StopMoving();
        //Change animation to "Victory"
        _playerCharacter.GetComponent<Animator>().Play("Fail");
        //Rotate the player character by 180 degrees to face the camera
        _playerCharacter.transform.localEulerAngles += new Vector3(0, 180, 0);
        //Activate Success UI
        UIManager.Instance.ActivateFailMenu();
    }

    public void StopMoving()
    {
        //Change speed to 0
        _speed = 0;
        //Change Animator's parameter to false
        _playerCharacter.GetComponent<Animator>().SetBool("isWalking", false);
    }
}
