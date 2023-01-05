using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject SkillList;
	[SerializeField] private List<RectTransform> Slots;

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
		Slots.Clear();

		for (int i = 0; i < SkillList.transform.childCount; ++i)
		{
			for (int j = 0; j < SkillList.transform.GetChild(i).childCount; ++j)
				Slots.Add(SkillList.transform.GetChild(i).GetChild(j).GetComponent<RectTransform>());
		}

		foreach(RectTransform element in Slots)
        {
			float _width = element.rect.width;

			if (element.transform.position.x + _width >= transform.position.x ||
				element.transform.position.x - _width <= transform.position.x)
				transform.position = new Vector2(element.transform.position.x, transform.position.y);
        }

		Debug.Log("OnPointerUp");
	}

    void Update()
    {
		
	}
}
