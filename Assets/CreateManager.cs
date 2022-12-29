using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
	static public CreateManager Instance = null;

	[Range(3.0f, 15.0f)]
	private float time;
	[ReadOnly, SerializeField] private Enemy EnemyObj;

	public List<Object> EnemyList = new List<Object>();

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void Start()
	{
		time = 5.0f;
	}

	public void CreateObject(Enemy _Obj) // �̱��濡�� ������Ʈ�� ������ִ� �Լ�
	{
		Object enemy = Instantiate(_Obj);
		enemy._Object.AddComponent<MeshFilter>();
		enemy.Initialize();

		EnemyList.Add(enemy); // �ʱⰪ ���� ���� ������Ʈ�� �����ϰ� List�� �־���.

		// ��ü ������ ������, �����δ� ������ �߽��� ���������� ���� ����Ѵ�.
		//List<Enemy> tempList = (List<Enemy>)(from e in EnemyList where e.Output() select e); // EnemyList�� �ִ� �͵� �߿� Ư�� ���ǿ� �ش�Ǵ� e�� �Ѱ��ش�.

		// ���ٽ�
		//List<Enemy> tempList = EnemyList.FindAll(e => e.Output());
		//Enemy temp = EnemyList.Find(e => e.Output());
		//EnemyList.Sort( (e, d) => e.Value.CompareTo(d) );
	}

	private void FixedUpdate()
	{
		time -= Time.deltaTime; // time�� ���۰��� 5�ʿ��� deltaTime�� �ð���ŭ ��� ���ش�.

		if (time < 0.0f) // time�� ���� 0 �̸��� �� ��� �۵���.
		{
			time = 5.0f; // time�� �ٽ� 5�ʷ� �������ش�.

			// ������Ʈ�� ������ִ� �Լ��� �ٽ� ȣ���Ѵ�.
			CreateObject(EnemyObj);
		}
	}
}
