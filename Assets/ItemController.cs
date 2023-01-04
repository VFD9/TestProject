using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject Obj;

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position; // ���� ��ǥ�� Ŭ���� ��ǥ�� ����

		Debug.Log("OnDrag");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Ŀ���� ����");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Ŀ���� ����");
	}

	public void OnPointerUp(PointerEventData eventData) // �������� �巡���ؼ� ���Ծȿ� ���� �ϱ�
	{
		//GameObject Obj = GameObject.Find("SkillSlot");
		float _width = Obj.GetComponent<RectTransform>().rect.width * 0.5f;

		if (eventData.pointerDrag.transform.position.x <= Obj.transform.position.x + _width
			|| eventData.pointerDrag.transform.position.x >=Obj.transform.position.x + _width)
			eventData.pointerDrag.transform.position = new Vector3(Obj.transform.position.x, transform.position.y, transform.position.z);

		Debug.Log("OnPointerUp");
	}

	void Start()
    {
        
    }

    void Update()
    {
		if (Obj != null)
			Obj = GameObject.Find("SkillSlot");
	}
}
