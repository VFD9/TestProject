using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
{
	[SerializeField] private RectTransform SlotList;
	[SerializeField] private GameObject Parent;
	private Animator animator;

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position; // ���� ��ǥ�� Ŭ���� ��ǥ�� ����

		Debug.Log("OnDrag");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		transform.SetParent(Parent.transform);
		transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);

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
		SlotinItem();

		Debug.Log("OnPointerUp");
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
	}

	void Start()
    {
		animator = GetComponent<Animator>();
	}

	private void SlotinItem()
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
				else
				{
					transform.localScale = new Vector3(1, 1, 1);
				}
			}
		}
	}
}
