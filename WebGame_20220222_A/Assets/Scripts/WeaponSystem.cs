using UnityEngine;

namespace KID
{
    /// <summary>
    /// �Z���t��
    /// 1.�x�s���a�֦����Z�����
    /// 2.�ھڪZ����ƥͦ��Z��
    /// 3.�ᤩ�Z���������,����t�סB�����O
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("�Z�����")]
        private DataWeapon dataWeapon;

        /// <summary>
        /// �p�ɾ�
        /// </summary>
        private float timer;
        #region///<summary> ø�s�ϥ�  �@��:�b�s�边��(Unity)ø�s�U�عϧλ��U�}�o  ���|�b�����ɤ��X�{
        #endregion
        private void OnDrawGizmos()
        {
            //1.�ϥ��C��
            //new Color(���A��A�šA�z����) �� 0 - 1
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            //2.ø�s�ϥ�
            //�ϥܪ�ø�s���(�����I�A�b�|)
            //transform.position + dataWeapon.v2SpawnPoint[0] �}��y�� + �ͦ���m
            //�ت�:�ͦ���m�|�ھڸ}�⬰�s�@�۹ﲾ��

            //for �j��
            //(��l��;����;�C�@���j�鵲������϶�)
            for (int i = 0; i < dataWeapon.v2SpawnPoint.Length; i++)
            {
                Gizmos.DrawSphere(transform.position + dataWeapon.v2SpawnPoint[i], 0.1f);
            }
        }

        private void Start()
        {
            // 2D ���z.�����ϼh�I��(�ϼh1 , �ϼh2)
            Physics2D.IgnoreLayerCollision(3, 6);       //���a �P �Z�� ���I��
            Physics2D.IgnoreLayerCollision(6, 6);       //�Z�� �P �Z�� ���I��
        }

        private void Update()
        {
            SpawnWeapon();
        }

        ///<summary>
        ///�ͦ��Z��
        ///�C�j�Z������ɶ��N�ͦ��Z���b�ͦ���m�W
        ///</summary>
        private void SpawnWeapon()
        {
            //print("�g�L�ɶ�:" + timer);

            //�p�G �p�ɾ� �j�󵥩� ����ɶ�
            if(timer >= dataWeapon.interval)
            {
                //�H���� = �H��.�d��(�̤p��,�̤j��) �� ��Ƥ��]�t�̤j��
                int random = Random.Range(0, dataWeapon.v2SpawnPoint.Length);
                //�y��
                Vector3 pos = transform.position + dataWeapon.v2SpawnPoint[random];
                //Quaternion �|�줸 - �O�����׸�T
                //Quaternion.identity �s����(0,0,0)
                //�Ȧs�Z�� = �ͦ�(����)
                GameObject temp = Instantiate(dataWeapon.goWeapon, pos, Quaternion.identity);
                //�Ȧs�Z��.���o����<����>().�K�[���O (��V * �t��)
                temp.GetComponent<Rigidbody2D>().AddForce(dataWeapon.v3Direction * dataWeapon.speedFly);
                //�p�ɾ� �k�s
                timer = 0;
            }
            //�_�h
            else
            {
                //�p�ɾ� �֥[ �@�Ӽv�檺�ɶ�
                timer += Time.deltaTime;
            }
        }
    }
}