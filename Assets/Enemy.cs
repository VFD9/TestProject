using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Object
{
	// �߻� Ŭ������ �Լ��� ��� �޾Ƽ�(override) ������
	public override void Initialize()
	{
		// �θ� Ŭ�������� ����� ������ ����Ҷ��� base.�� �ٿ�����Ѵ�.
		base.Value = 10;
		base.Key = "Enemy";

		Debug.Log(base.Key + " : " + base.Key);
	}

	public override void Progress()
	{
		
	}

	public override void Release()
	{
		
	}
}
