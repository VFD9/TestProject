using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
	static public SliderController Instance = null;

	[SerializeField] private RectTransform TargetUITransform;
	[SerializeField] private RectTransform StartPoint;
	[SerializeField] private RectTransform EndPoint;
	
	public bool MoveCheck = false;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void Start()
	{
		TargetUITransform.position = StartPoint.position;

		MoveCheck = false; // ��� ��ư�� ������ �������� �������� �ʾ����� �ʱ� MoveCheck�� false�� �����Ѵ�. TargetUI(ScrollView)�� Canvas �ٱ��ʿ� ��ġ������.
	}

	float SetTime()
	{
		float distance = Vector3.Distance(StartPoint.position, EndPoint.position);  // ���� ������ �� ���� ������ �Ÿ��� ����.
		// MoveCheck�� ���� Ȯ���ؼ� true�� ��ü�� ��ǥ�� ������ ������ ���� Offset�� �����ϰ�, false��� ��ü�� ��ǥ�� �������� ������ ���� Offset�� ����
		float Offset = MoveCheck ? Vector3.Distance(TargetUITransform.position, EndPoint.position) : Vector3.Distance(TargetUITransform.position, StartPoint.position);   

		return  1 - (Offset / distance); // ���� ������ ����ϱ� ����(0 ~ 1 ����), 1���� OffsetPoint/distance�� ���� ����.
	}

	public IEnumerator SlideInCoroutine_01() // ������ ���̴� �ڷ�ƾ �Լ�
	{
		// �⺻ �����̵�
		float time = SetTime();

		while (time <= 1.0f && MoveCheck) // 1�� �����̰� �����س��� MoveCheck�� ���� ��ġ�� ��
		{
			time += Time.deltaTime; // while�� �ȿ� �ִ� time�� �� ������ �� ����Ǵ� �ð��� ��� �����ش�.
			TargetUITransform.position = Vector3.Lerp(StartPoint.position, EndPoint.position, time); // �������� ������ ���� ��ü�� 1�� ���ϱ��� �������� time ������ ����� �̵���Ų��.
			yield return null;
		}
	}

	public IEnumerator SlideOutCoroutine_01() // �ٱ����� �������� �ڷ�ƾ �Լ�
	{
		// �⺻ �����̵�
		float time = SetTime();

		while (time <= 1.0f && !MoveCheck) // 1�� �����̰� �����س��� MoveCheck�� ���� ����ġ�� ��
		{
			time += Time.deltaTime; // while�� �ȿ� �ִ� time�� �� ������ �� ����Ǵ� �ð��� ��� �����ش�.
			TargetUITransform.position = Vector3.Lerp(EndPoint.position, StartPoint.position, time); // ������ �������� ���� ��ü�� 1�� ���ϱ��� �������� time ������ ����� �̵���Ų��.
			yield return null;
		}
	}
}
