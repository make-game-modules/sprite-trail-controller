/**
 * ����һ��Unity��SpriteController�ű���������Ҫ��������һ����Ϸ�����ڰ��¼�ͷ���ƶ�ʱ�����ɲ�����һϵ���侫���¡�塣
 * ��Щ��¡�������Ϸ������ƶ�������ƫ�Ʋ��𽥱�͸����Ϊ��Ϸ�����ṩһ��β��Ч��������Ϸ����ֹͣ�ƶ�ʱ�����еĿ�¡��ᱻ���١�
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrailController: MonoBehaviour
{
    public float offset = 0.1f;  // ��¡���ƫ����
    public float transparency = 0.5f;  // ��¡���͸����

    private SpriteRenderer spriteRenderer;  // ��Ϸ����ľ�����Ⱦ��
    private Vector3 lastPosition;  // ��Ϸ�������һ��λ��
    private List<GameObject> clones = new List<GameObject>();  // �洢���п�¡����б�

    void Start()
    {
        lastPosition = transform.position;  // ��ʼ����һ��λ��
        spriteRenderer = GetComponent<SpriteRenderer>();  // ��ȡ������Ⱦ�����
    }

    void Update()
    {
        Vector3 deltaPosition = transform.position - lastPosition;  // ����λ�õı仯

        if (deltaPosition != Vector3.zero)  // ���λ���б仯
        {
            if (clones.Count == 0)  // �����û�п�¡��
            {
                for (int i = 0; i < 3; i++)  // ����3����¡��
                {
                    GameObject clone = new GameObject();  // ����һ���µ���Ϸ������Ϊ��¡��
                    clone.transform.parent = transform;  // ���ÿ�¡��ĸ�����Ϊ��ǰ��Ϸ����
                    clone.AddComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;  // ���ƾ���

                    SpriteRenderer cloneSpriteRenderer = clone.GetComponent<SpriteRenderer>();  // ��ȡ��¡��ľ�����Ⱦ��
                    Color spriteColor = cloneSpriteRenderer.color;  // ��ȡ�������ɫ
                    spriteColor.a = Mathf.Clamp01(spriteColor.a * Mathf.Pow(transparency, i + 1));  // ����͸���ȺͿ�¡��������޸ľ������ɫ
                    cloneSpriteRenderer.color = spriteColor;  // Ӧ���µ���ɫ

                    cloneSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;  // ������Ⱦ��ID
                    cloneSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;  // ������Ⱦ˳��

                    clones.Add(clone);  // �ѿ�¡����뵽�б���
                }
            }

            UpdateClonePositions();  // ���¿�¡���λ��
        }
        else  // ���λ��û�б仯
        {
            foreach (GameObject clone in clones)  // ����ÿһ����¡��
            {
                Destroy(clone);  // ������
            }
            clones.Clear();  // ����б�
        }

        lastPosition = transform.position;  // ������һ��λ��
    }

    // ��������������ݰ���������¿�¡���λ��
    void UpdateClonePositions()
    {
        Vector3 direction = Vector3.zero;  // ��ʼ���ƶ�����

        if (Input.GetKey(KeyCode.LeftArrow))  // ����������
        {
            direction.x += 1;  // �����ƶ�
        }

        if (Input.GetKey(KeyCode.RightArrow))  // ��������Ҽ�
        {
            direction.x -= 1;  // �����ƶ�
        }

        if (Input.GetKey(KeyCode.UpArrow))  // ��������ϼ�
        {
            direction.y -= 1;  // �����ƶ�
        }

        if (Input.GetKey(KeyCode.DownArrow))  // ��������¼�
        {
            direction.y += 1;  // �����ƶ�
        }

        direction = direction.normalized;  // ��һ���ƶ�����

        //Debug.Log(direction);  // ��ӡ�ƶ�����

        for (int i = 0; i < clones.Count; i++)  // ����ÿһ����¡��
        {
            GameObject clone = clones[i];  // ��ȡ��¡��
            Vector3 newPosition = transform.position + direction * (i + 1) * offset;  // �����µ�λ��
            clone.transform.position = newPosition;  // �����µ�λ��
        }
    }
}
