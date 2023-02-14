using UnityEngine;
using UnityEngine.UI;

public class RubikSelectFace : MonoBehaviour
{
    [SerializeField]
    private Text rubikText;
    private int rowsCounter = -1, faceColumnsCounter = -1, lateralColumnsCounter = -1;

    public static RubikSelectFace rubikSelectFaceInstance;

    private RubikFacesColors rubikManager;

    private void Awake()
    {
        rubikSelectFaceInstance = this;
        rubikManager = GetComponent<RubikFacesColors>();
    }

    public void buttonPressed(string typeOfBtn)
    {
        switch (typeOfBtn)
        {
            case "RowsBtn":
                faceColumnsCounter = -1;
                lateralColumnsCounter = -1;

                if (rowsCounter < 2)
                    ++rowsCounter;
                else
                    rowsCounter = 0;

                switch (rowsCounter)
                {
                    case 0:
                        rubikText.text = "Prima riga selezionata.";
                        break;

                    case 1:
                        rubikText.text = "Seconda riga selezionata.";
                        break;

                    case 2:
                        rubikText.text = "Terza riga selezionata.";
                        break;
                }
                break;

            case "FaceColumnsBtn":
                rowsCounter = -1;
                lateralColumnsCounter = -1;

                if (faceColumnsCounter < 2)
                    ++faceColumnsCounter;
                else
                    faceColumnsCounter = 0;

                switch (faceColumnsCounter)
                {
                    case 0:
                        rubikText.text = "Faccia Sinistra selezionata.";
                        break;

                    case 1:
                        rubikText.text = "Colonna centrale (di facciata) selezionata.";
                        break;

                    case 2:
                        rubikText.text = "Faccia Destra selezionata.";
                        break;
                }
                break;

            case "LateralColumnsBtn":
                rowsCounter = -1;
                faceColumnsCounter = -1;

                if (lateralColumnsCounter < 2)
                    ++lateralColumnsCounter;
                else
                    lateralColumnsCounter = 0;

                switch (lateralColumnsCounter)
                {
                    case 0:
                        rubikText.text = "Faccia Back selezionata.";
                        break;

                    case 1:
                        rubikText.text = "Colonna centrale (laterale) selezionata.";
                        break;

                    case 2:
                        rubikText.text = "Faccia Frontale selezionata.";
                        break;
                }
                break;

            case "ConfirmBtn":
                //rotate face
                switch (rowsCounter)
                {
                    case 0:
                        rubikManager.rotateFace("Row1", 0, 1, 0);
                        break;

                    case 1:
                        rubikManager.rotateFace("Row2", 0, 1, 0);
                        break;

                    case 2:
                        rubikManager.rotateFace("Row3", 0, 1, 0);
                        break;

                }

                switch (faceColumnsCounter)
                {
                    case 0:
                        rubikManager.rotateFace("FaceColumn1", 1, 0, 0);
                        break;

                    case 1:
                        rubikManager.rotateFace("FaceColumn2", 1, 0, 0);
                        break;

                    case 2:
                        rubikManager.rotateFace("FaceColumn3", 1, 0, 0);
                        break;

                }

                switch (lateralColumnsCounter)
                {
                    case 0:
                        rubikManager.rotateFace("LateralColumn1", 0, 0, 1);
                        break;

                    case 1:
                        rubikManager.rotateFace("LateralColumn2", 0, 0, 1);
                        break;

                    case 2:
                        rubikManager.rotateFace("LateralColumn3", 0, 0, 1);
                        break;

                }
                break;
        }
    }
}
