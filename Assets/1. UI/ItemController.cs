using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private RectTransform SlotList;
	[SerializeField] private GameObject Parent;

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position; // ���� ��ǥ�� Ŭ���� ��ǥ�� ����
		transform.SetParent(Parent.transform);

		Debug.Log("OnDrag");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 0.85f, 1.0f);

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
		InputItem();

		transform.localScale = new Vector3(1, 1, 1);

		Debug.Log("OnPointerUp");
	}

	private void InputItem()
	{
		for (int i = 0; i < SlotList.transform.childCount - 1; ++i)
		{
			for (int j = 0; j < SlotList.transform.GetChild(i).childCount; ++j)
			{
				Vector3 _position = SlotList.transform.GetChild(i).GetChild(j).position;
				float Distance = Vector3.Distance(SlotList.transform.GetChild(i).GetChild(j).position, transform.position);

				if (Distance <= 50.0f)
				{
					transform.SetParent(SlotList.transform.GetChild(i).GetChild(j));
					transform.position = _position;
				}
			}
		}
	}
}
