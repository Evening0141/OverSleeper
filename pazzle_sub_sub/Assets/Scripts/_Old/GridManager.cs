using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Field
    [Header("��"),SerializeField] int width = 10;      // ��
    [Header("����"),SerializeField] int height = 10;   // ����

    [Header("�}�X�̃v���t�@�u"),SerializeField] 
    GameObject CellPrefab;    // �}�X�̃v���t�@�u

    [Header("�O���b�h(�������g)"),SerializeField] 
    Transform gridTransform;  // �O���b�h(�������g)

    private GameObject[,] gridCells; // �}�X�̓񎟌��z��
    #endregion

    void Start()
    {
        CreateGrid();  // �O���b�h�쐬
    }

    #region Method
    private void CreateGrid() // �O���b�h�쐬
    {
        gridCells = new GameObject[width, height]; // �񎟌��z��̐���

        for(int y=0;y<height;++y)
        {
            for(int x=0;x<width;++x)
            {
                // �}�X�̐���
                GameObject cell = 
                    Instantiate(CellPrefab, gridTransform);
                // �z��Ɋi�[
                gridCells[x, y] = cell;
            }
        }
    }
    #endregion
}
