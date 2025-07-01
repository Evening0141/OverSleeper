using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateText : MonoBehaviour
{
    [Header("�v���n�u�E�Q�Ɛݒ�")]
    public GameObject charPrefab;         // 1�����\���p��UI�v���n�u�iText�t���j
    public Transform parentCanvas;        // �e�ƂȂ�Canvas�iRectTransform�j
    public TextData textData;             // �q���g�Ȃǂ̕�����f�[�^�iScriptableObject�j

    [Header("�����\���Ԋu�ݒ�")]
    public float charSpacing = 50f;       // �e�����̊Ԋu�ipx�P�ʁA�Œ蕝�j
    public float charOffsetY = 0f;        // �\��Y���W�i�c�ʒu�̒����j
    public float charDelay = 0.1f;        // �e�����̏o���Ԋu�i�^�C�s���O���̑��x�j
    public float spawnInterval = 3f;      // 1�̕��͕\�����������Ă���A���̕��͂��o���܂ł̑ҋ@����

    [Header("�F�ύX�ݒ�")]
    public Color highlightColor = Color.red;   // �F�ύX�p�J���[
    public char[] colorMarkers = new char[] { '&', '%', '$' }; // ����̋L���i����1������F�t���j

    private bool isSpawning = false;      // ���ݕ��͂𐶐������ǂ����̃t���O�i�d��������h���j

    /// <summary>
    /// �O������Ăяo���ăq���g�\�����J�n����
    /// </summary>
    public void TIPSText()
    {
        if (!isSpawning)
        {
            StartCoroutine(GenerateHintCoroutine());
        }
    }

    /// <summary>
    /// �R���[�`����1��������TIPS�𐶐����ĕ\������i�^�C�s���O�����o�{���蕶���F�j
    /// </summary>
    IEnumerator GenerateHintCoroutine()
    {
        isSpawning = true;

        // �e�L�X�g�f�[�^�����݂��邩�m�F
        if (textData == null || textData.hintMessages.Length == 0)
        {
            Debug.LogWarning("TextData���ݒ肳��Ă��Ȃ����A���b�Z�[�W����ł��B");
            yield break;
        }

        // �����_����1�̃��b�Z�[�W��I��
        string message = textData.hintMessages[Random.Range(0, textData.hintMessages.Length)];

        float xOffset = 800f; // �J�n�ʒu�i��ʉE���j
        bool nextCharIsColored = false;

        // 1����������
        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];

            // �F�ύX�p�̋L�����ǂ������`�F�b�N
            if (IsColorMarker(c))
            {
                nextCharIsColored = true;
                continue; // �L���͕\�����Ȃ�
            }

            // �v���n�u�𐶐�
            GameObject charObj = Instantiate(charPrefab, parentCanvas);
            Text text = charObj.GetComponentInChildren<Text>();
            string charStr = c.ToString();
            text.text = charStr;

            // �F�ύX�t���O�������Ă���ΐF��ύX
            if (nextCharIsColored)
            {
                text.color = highlightColor;
                nextCharIsColored = false;
            }

            // ���W��ݒ�
            RectTransform rt = charObj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(xOffset, charOffsetY);
            xOffset += charSpacing;

            // ���̕����܂őҋ@�i�^�C�s���O�����o�j
            yield return new WaitForSeconds(charDelay);
        }

        // �S���\����A���̃��b�Z�[�W�܂őҋ@
        yield return new WaitForSeconds(spawnInterval);
        isSpawning = false;
    }

    /// <summary>
    /// �w�肵���������F�ύX�}�[�J�[���ǂ����𔻒�
    /// </summary>
    private bool IsColorMarker(char c)
    {
        foreach (char marker in colorMarkers)
        {
            if (c == marker) return true;
        }
        return false;
    }
}
