using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RightControllerManager : MonoBehaviour {

    private SteamVR_TrackedObject _trackedObj;
    SteamVR_Controller.Device _device;

    public GameObject _inventoryObj;
    public GameObject _handModelObj;

    //creates a list of items for the invebtory to manage
    public List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    private Vector2 _touchpad;
    private float _angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public bool _isInventoryOpen;
    public bool _firstPressUp;

    public bool _hasItemInHand;

    public int _currentItem;
    public int _oldItem;

    public GameObject _inHandObj;

    public GameObject _cursorObj;

    public Color _idleUIColor;
    public Color _highlightedUIColor;

    [Header("Inventory Post Effect Properties")]
    public float _exposureAmnt;
    public float _dofAmnt;

    void Start () {
        _trackedObj = GetComponent<SteamVR_TrackedObject>();
        _inventoryObj.SetActive(false);
    }
	
	void Update () {
        if (_trackedObj.gameObject.activeInHierarchy)
        {
            _device = SteamVR_Controller.Input((int)_trackedObj.index);
        }

        if (_device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !_isInventoryOpen)
        {
            
            _isInventoryOpen = true;
            //CheckInventoryItemPlacement();
            _inventoryObj.SetActive(true);
            //ShowInventoryItems(true);
            PostProcessControl.InventoryOpenPostEffect(_exposureAmnt);
            HapticFeedback.HapticAmount(500);
            PlaceItemsInInventory(false, true);
            CheckHandModelVisibility();
            //_handModelObj.gameObject.SetActive(true);

            //if (_inHandObj != null)
            //{
            //    _inHandObj.SetActive(false);
            //    _hasItemInHand = false;
            //}

        }

        if (_isInventoryOpen)
        {
            OpenInventory();
        }

        //if (_hasItemInHand)
        //{
        //    _inHandObj.transform.position = Vector3.Lerp(_inHandObj.transform.position, transform.position, Time.deltaTime * 12f);
        //    _inHandObj.transform.rotation = Quaternion.Slerp(_inHandObj.transform.rotation, transform.rotation, Time.deltaTime * 12f);
        //} else
        //{
        //    foreach (var slot in _inventorySlots)
        //    {
        //        if (slot.hasItemInHand)
        //        {
        //            PutInInventory(slot);
        //        }
        //    }
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            if (_device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (CheckInventoryItemExists(collision.gameObject.GetComponent<Collectable>().itemName))
                {
                    //print("The object we are trying to pick up does not yet exists in our inventory");
                    PlaceItemsInInventory(false, true);
                    //print("going to add an inventory item");
                    AddInventoryItem(collision.gameObject);
                }
            }
        }
    }

    public void OpenInventory()
    {
        TouchpadEnabled();
        //ShowInventoryItems(false);

        if (_device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && !_firstPressUp)
            _firstPressUp = true;
        
        if (_touchpad.magnitude > .25f)
        {
            // Inventory Slot #1
            if (_angleFromCenter > 275 && _angleFromCenter < 335)
                _currentItem = 0;
            // Inventiry Slot #2
            else if (_angleFromCenter > 15 && _angleFromCenter < 75)
                _currentItem = 1;
            else
                _currentItem = -1;
        }
        else
            _currentItem = -1;


        if (_currentItem != _oldItem)
        {
            if (_currentItem > -1)
            {
                foreach (var slot in _inventorySlots)
                {
                    slot.inventoryOutline.color = _idleUIColor;
                    slot.textTag.gameObject.SetActive(false);
                }

                _inventorySlots[_currentItem].inventoryOutline.color = _highlightedUIColor;
                _inventorySlots[_currentItem].textTag.gameObject.SetActive(true);

                HapticFeedback.HapticAmount(500);

            } else
            {
                foreach (var slot in _inventorySlots)
                {
                    slot.inventoryOutline.color = _idleUIColor;
                    slot.textTag.gameObject.SetActive(false);
                }
            }

            _oldItem = _currentItem;
        }

        if (_device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && _currentItem > -1 && _firstPressUp)
        {
            if (_inventorySlots[_currentItem].inventoryObj != null)
            {
                PutItemInHand(_inventorySlots[_currentItem]);
                HapticFeedback.HapticAmount(750);

                //_inventorySlots[_currentItem].hasItemInHand = true;
                //_handModelObj.SetActive(false);
                CloseInventory();
            }



            //if (_inventorySlots[_currentItem].itemInHandObj != null)
            //{
            //    _inventorySlots[_currentItem].itemInHandObj.SetActive(true);
            //    //objInHand.transform.position = transform.position;
            //    //objInHand = itemList[currentItem].itemInHandObj;
            //    //objInHand.SetActive(true);
            //    _hasItemInHand = true;
            //}






        }

        if (_device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && _firstPressUp & _currentItem == -1)
            CloseInventory();

    }

    public void CloseInventory()
    {
        PostProcessControl.InventoryClosePostEffect();

        //print("closing inventory");
        _isInventoryOpen = false;
        _inventoryObj.SetActive(false);
        PlaceItemsInInventory(true, false);
        //ShowInventoryItems(false);
        _firstPressUp = false;
    }

    private bool CheckInventoryItemExists(string name)
    {
        //print("Checking if exists:" + name);
        foreach (var slot in _inventorySlots)
        {
            if (slot.slotTaken && slot.name == name)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return true;
    }

    private void PlaceItemsInInventory(bool itemsNotInHand, bool allItems) //check what items the player currently has
    {
        //print("placing items in inventory");
        foreach (var slot in _inventorySlots)
        {
            if (itemsNotInHand)
            {
                if (slot.slotTaken && !slot.hasItemInHand)
                {
                    PutInInventory(slot);
                    slot.hasItemInHand = false;
                }
            } else if (allItems)
            {
                if (slot.slotTaken)
                {
                    PutInInventory(slot);
                    slot.hasItemInHand = false;
                }
            }
            
        }
        //print("finished placing items in inventory");
    }


    public void GiveAwayItem(InventorySlot inventorySlot)
    {
        ClearImventorySlot(inventorySlot);
        CheckHandModelVisibility();
        //_handModelObj.SetActive(true);
    }

    private void PutInInventory(InventorySlot inventorySlot)
    {
        //_handModelObj.SetActive(true);
        

        inventorySlot.inventoryObj.tag = "Collectable";

        inventorySlot.inventoryObj.transform.parent = inventorySlot.attachPoint.transform;

        if (inventorySlot.name == "Pistol")
            inventorySlot.inventoryObj.transform.localPosition = new Vector3(-0.03f, .005f, 0f); // inventorySlot.attachPoint.localScale;
        else
            inventorySlot.inventoryObj.transform.localPosition = Vector3.zero; // inventorySlot.attachPoint.localPosition;

        inventorySlot.inventoryObj.transform.localRotation = Quaternion.Euler(0, 0, 0);// inventorySlot.attachPoint.localRotation;

        if (inventorySlot.name == "Pistol")
            inventorySlot.inventoryObj.transform.localScale = new Vector3(.65f, .65f, .65f); // inventorySlot.attachPoint.localScale;
        else
            inventorySlot.inventoryObj.transform.localScale = new Vector3(1, 1, 1); // inventorySlot.attachPoint.localScale;


        inventorySlot.hasItemInHand = false;
        CheckHandModelVisibility();

        //print(inventorySlot.name + " was put in the inventory");


        //inventorySlot.inventoryObj.transform.position = Vector3.Lerp(inventorySlot.inventoryObj.transform.position, inventorySlot.attachPoint.position, Time.deltaTime * 12f);
        //inventorySlot.inventoryObj.transform.rotation = Quaternion.Slerp(inventorySlot.inventoryObj.transform.rotation, inventorySlot.attachPoint.rotation, Time.deltaTime * 12f);
    }

    private void PutItemInHand(InventorySlot inventorySlot)
    {
        //print("Putting in hand");
        PlaySound.PlayAudioFromSelection(inventorySlot.inventoryObjAudioSource, inventorySlot.inventoryObjAudioClips, true, -.05f, .05f);
        //_handModelObj.SetActive(false);

        inventorySlot.inventoryObj.tag = "Collected";

        //_inHandObj = inventorySlot.inventoryObj;
        inventorySlot.inventoryObj.transform.parent = this.transform;
        inventorySlot.hasItemInHand = true;
        //_hasItemInHand = true;

        //print("The status of the hand model should be true:" + _handModelObj.gameObject.activeInHierarchy);
        //print("The status of the hand model should be false:" + _handModelObj.gameObject.activeInHierarchy);

        inventorySlot.inventoryObj.transform.localPosition = inventorySlot.collectable.attachPoint.localPosition;
        inventorySlot.inventoryObj.transform.localRotation = inventorySlot.collectable.attachPoint.localRotation;
        inventorySlot.inventoryObj.transform.localScale = inventorySlot.inventoryObjOriginalScale;

        CheckHandModelVisibility();

        //print(inventorySlot.name + " was put in my hand");

        //inventorySlot.rb.useGravity = false;
        //inventorySlot.rb.isKinematic = true;

        //objCollectable.transform.localPosition = collectable.attachPoint.localPosition;
        //objCollectable.transform.localRotation = collectable.attachPoint.localRotation;

        //inventorySlot.inventoryObj.transform.position = Vector3.Lerp(inventorySlot.inventoryObj.transform.position, transform.position, Time.deltaTime * 12f);
        //inventorySlot.inventoryObj.transform.rotation = Quaternion.Slerp(inventorySlot.inventoryObj.transform.rotation, transform.rotation, Time.deltaTime * 12f);

        //_handModelObj.gameObject.SetActive(false);

        //// The item that will be in the player's hand is the item the player has just collected
        ////objInHand = collision.gameObject;
        //objCollectable.transform.parent = this.transform;

        //objCollectable.transform.localPosition = collectable.attachPoint.localPosition;
        //objCollectable.transform.localRotation = collectable.attachPoint.localRotation;

        ////collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
        ////collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }

    private void AddInventoryItem(GameObject objCollectable)
    {
        // Get Collectable class properties from object

        //print("I am beginning to add something to the inventory");
        // Find an inventory slot that is not already in use
        var inventorySlot = new InventorySlot();

        //print("I have assigned an empty inventory slot in memory for temporay use");


        for (int i = 0; i <= _inventorySlots.Capacity - 1; i++)
        {
            //print("I am adding an inventory item. Here is the slot I am on:" + i);

            if (!_inventorySlots[i].slotTaken)
            {
                inventorySlot = _inventorySlots[i];
                
                //print("The inventory slot I have decided to add the item in is:" + i);
                break;

            }
        }

        //objCollectable.CompareTag("Collected";
        inventorySlot.collectable = objCollectable.GetComponent<Collectable>();
        //inventorySlot.rb = objCollectable.GetComponent<Rigidbody>();

        inventorySlot.collectable.isCollected = true;

        // Assign object to empty inventory slot 
        inventorySlot.name = inventorySlot.collectable.itemName;
        inventorySlot.inventoryObj = inventorySlot.collectable.gameObject;
        inventorySlot.textTag.text = inventorySlot.collectable.itemName;
        inventorySlot.inventoryObjOriginalScale = objCollectable.transform.localScale;
        inventorySlot.inventoryObjAudioSource = objCollectable.GetComponent<AudioSource>();
        inventorySlot.inventoryObjAudioClips = objCollectable.GetComponent<Collectable>()._soundsWhenEnabled;


        inventorySlot.slotTaken = true;
        //inventorySlot.hasItemInHand = true;

        PutItemInHand(inventorySlot);

        //return inventorySlot;

        //inventoryslot.textTag


        //foreach (InventorySlot slot in _inventorySlots)
        //{
        //    if (!slot.slotTaken)
        //    {
        //        inventoryslot = 
        //    }

        //    item.hasItemInHand = false;
        //    if (item.itemInHandObj != null)
        //    {
        //        item.itemInHandObj.SetActive(false);
        //    }

        //    if (!item.hasItemInInventory)
        //    {
        //        if (collectable.itemName == item.name)
        //        {
        //            // We have the item in our inventory
        //            item.hasItemInInventory = true;
        //            item.itemInHandObj = objCollectable.gameObject;


        //        }
        //    }
        //}
        //collectable.isCollected = true;
        //_handModelObj.gameObject.SetActive(false);

        //// The item that will be in the player's hand is the item the player has just collected
        ////objInHand = collision.gameObject;
        //objCollectable.transform.parent = this.transform;

        //objCollectable.transform.localPosition = collectable.attachPoint.localPosition;
        //objCollectable.transform.localRotation = collectable.attachPoint.localRotation;

        ////collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
        ////collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;


        //// a check to say that the item is in the hands of the player
        ////item.hasItemInHand = true;
        //_hasItemInHand = true;
    }

    private void TouchpadEnabled()
    {
        _cursorObj.SetActive(true);

        // if the menu is open, get both the x and y values of the touchpad
        _touchpad.x = _device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        _touchpad.y = _device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        _cursorObj.transform.localPosition = Vector3.Lerp(_cursorObj.transform.localPosition, _touchpad * .085f, Time.unscaledDeltaTime * 10f);

        Vector2 fromVector2 = new Vector2(0, 1);
        Vector2 toVector2 = _touchpad;

        // Measure the users' thumb angle in relation to the center of the touchpad.
        _angleFromCenter = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        // This will get the angle of the users' thumb so we know what inventory item they are trying to highlight
        if (cross.z > 0)
        {
            _angleFromCenter = 360 - _angleFromCenter;
        }

    }

    private void ClearImventorySlot(InventorySlot inventorySlot)
    {
        inventorySlot.slotTaken = false;
        inventorySlot.inventoryObj = null;
        inventorySlot.name = "";
        inventorySlot.hasItemInHand = false;
    }

    public void AttachToDoor(string keyToUnlock, Transform attachPoint)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.name == keyToUnlock)
            {
                var key = slot.inventoryObj;
                GiveAwayItem(slot);
                key.transform.position = attachPoint.position;
                key.transform.parent = attachPoint.transform;
                //return true;
            } else
            {
                //return false;
            }
        }
    }

    private void ShowInventoryItems(bool show)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.inventoryObj != null && !slot.hasItemInHand)
            {
                if (show)
                    slot.inventoryObj.SetActive(true);
                else
                    slot.inventoryObj.SetActive(false);
            }
        }
    }

    private void CheckHandModelVisibility()
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.hasItemInHand)
            {
                _handModelObj.SetActive(false);
                break;
            }
            else
                _handModelObj.SetActive(true);
        }
    }



    [System.Serializable]
    public class InventorySlot
    {
        public string name;
        public bool slotTaken;
        public GameObject inventoryObj; 
        public Collectable collectable;
        public AudioSource inventoryObjAudioSource;
        public AudioClip[] inventoryObjAudioClips;
        //public Rigidbody rb;
        public Transform attachPoint;
        public Image inventoryOutline;
        public Vector3 inventoryObjOriginalScale;
        //public GameObject inventoryItemObj; // just the item object that is in the inventory
        //public GameObject itemInHandObj; // the item that will be in the hands of the player
        public TextMeshPro textTag;
        //public bool hasItemInInventory;
        public bool hasItemInHand;
    }

}