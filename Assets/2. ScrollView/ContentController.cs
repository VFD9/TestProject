using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
    [SerializeField] private GameObject HorizontalListPrefab;
    [SerializeField] private GameObject AddHorizontalButton;

    private RectTransform ContentTransform;

	private void Awake()
	{
        ContentTransform = GetComponent<RectTransform>();
	}

	void Start()
    {
        AddHorizontalList(0.0f, 120.0f);

        for (int i = 0; i < 5; ++i)
            AddHorizontalList(0.0f, 115.0f); // Content�� �ִ� Grid Layout Group �������� spacing�� 15�� �����س������Ƿ� ������ ���� 100�� 15�� ���� ���� �ִ´�.
    }

    private void AddHorizontalList(float _x, float _y)
	{
        GameObject Obj = Instantiate(HorizontalListPrefab); // �� ���� �������� Ŭ�� ����

        ContentTransform.sizeDelta = new Vector2(
                ContentTransform.sizeDelta.x + _x,
                ContentTransform.sizeDelta.y + _y);

        Obj.transform.SetParent(transform);

        AddHorizontalButton.transform.SetAsLastSibling(); // SetAsLastSibling() �ش� �ڽ� ��ü�� ���� ���������� �̵���Ŵ. 
    }

    public void HorizontalButton()
	{
        AddHorizontalList(0.0f, 115.0f);
    }
}
