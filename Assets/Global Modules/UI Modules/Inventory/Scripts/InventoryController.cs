//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tajurbah_Gah
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Inventory UI Template")]
        [SerializeField] GameObject itemUITemplate;
        [SerializeField] Transform itemsUIParent;

        [Header("Any Parent")]
        public bool makeParent = false;
        [ShowWhen("makeParent", true)]
        public GameObject Parent;

        [Header("Enter Inventory Items Data")]
        [SerializeField] InventoryItems[] inventoryItems;

        ObjectPoolController[] pooledInventoryItems;
        Image[] itemUIImages;

        int itemIndex;
        bool clickedOnItem = false;

        GameObject getTarget;

        Camera camera;

        private void Awake()
        {
            GameObject inventoryPoolManager = new GameObject();
            inventoryPoolManager.name = "Inventory Pool Manager";
            inventoryPoolManager.SetActive(false);

            if(makeParent)
            {
                inventoryPoolManager.transform.parent=Parent.transform;
            }

            pooledInventoryItems = new ObjectPoolController[inventoryItems.Length];
            itemUIImages = new Image[inventoryItems.Length];

            for (int i = 0; i < inventoryItems.Length; i++)
            {
                int locali = i;

                GameObject item = new GameObject();
                item.transform.SetParent(inventoryPoolManager.transform);
                item.name = inventoryItems[locali].name;
                pooledInventoryItems[locali] = item.AddComponent<ObjectPoolController>();
                pooledInventoryItems[locali].pooledObject = inventoryItems[locali].item;

                pooledInventoryItems[locali].pooledAmount = inventoryItems[locali].pooledAmount;
                pooledInventoryItems[locali].willGrow = true;

                GameObject itemUI = Instantiate(itemUITemplate);
                itemUI.transform.SetParent(itemsUIParent.transform, false);
                itemUI.name = "Item " + (locali + 1).ToString();
                itemUIImages[i] = itemUI.GetComponent<Image>();
                itemUIImages[i].sprite = inventoryItems[locali].image;

                EventTrigger trigger = itemUI.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { ClickedOnItem(locali); });
                trigger.triggers.Add(entry);

                itemUI.SetActive(true);
            }

            inventoryPoolManager.SetActive(true);

            camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && clickedOnItem)
            {
                MouseButtonDownAction();
            }

            //Mouse Button Up
            if (Input.GetMouseButtonUp(0))
            {
                MouseButtonUpAction();
            }

        }

        void ClickedOnItem(int ItemIndex)
        {
            itemIndex = ItemIndex;
            clickedOnItem = true;
        }

        void MouseButtonUpAction()
        {
            clickedOnItem = false;
        }

        void MouseButtonDownAction()
        {
            getTarget = pooledInventoryItems[itemIndex].GetPooledObject();
            getTarget.SetActive(false);
            getTarget.transform.position = inventoryItems[itemIndex].item.transform.position;
            getTarget.transform.rotation = inventoryItems[itemIndex].item.transform.rotation;
            getTarget.transform.localScale = inventoryItems[itemIndex].item.transform.localScale;

            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = camera.WorldToScreenPoint(getTarget.transform.position).z;
            getTarget.transform.position= camera.ScreenToWorldPoint(mousePoint);

            getTarget.SetActive(true);
            getTarget.GetComponent<DragDropController>()?.OnMouseDown();

            clickedOnItem = false;
            getTarget=null;
        }
    }

    [System.Serializable]
    public class InventoryItems
    {
        public GameObject item;
        public string name = null;
        public Sprite image;
        public int pooledAmount = 1;
    }
}