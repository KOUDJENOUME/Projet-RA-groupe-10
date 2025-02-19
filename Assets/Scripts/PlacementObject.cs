using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;
    private int count;
    public GameObject MoteurAn;

    [SerializeField]
    private bool IsLocked;

    public bool Selected
    {
        get { return IsSelected; }
        set
        {
            IsSelected = value;
            ToggleUI();
            if (IsSelected)
            {
                PlayAudio();
            }
            
        }
    }


    public bool Locked
    {
        get { return IsLocked; }
        set { IsLocked = value; }
    }

    [SerializeField]
    private TextMeshPro OverlayText;

    [SerializeField] private AudioSource audioSource; // Ajout d'un AudioSource

    [SerializeField]
    private Canvas canvasComponent;

    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private TextMeshProUGUI panelText;

    [SerializeField]
    private Button nextPageButton;

    [SerializeField]
    private Button previousPageButton; // Nouveau bouton pr�c�dent

    [SerializeField]
    private Button exitButton;  // Nouveau bouton de sortie

    private int currentPage = 0;
    private string[] pages;
    void Start()
    {
        



    }
    void Awake()
    {
        OverlayText = GetComponentInChildren<TextMeshPro>();

        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(false);
        }

        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }

        if (nextPageButton != null)
        {
            nextPageButton.onClick.AddListener(NextPage);
        }

        if (previousPageButton != null)
        {
            previousPageButton.onClick.AddListener(PreviousPage); // Action de retour � la page pr�c�dente
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ClosePanel);  // Action de fermeture du panneau
        }

        // V�rifie le texte dans OverlayText et d�finit les pages en cons�quence
        if (OverlayText.text == "Arbre � canne")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Arbre � canne</color></size>\n<size=20>�tape 1 : V�rifiez l'�tat de l'arbre � canne.</size>",
        "<size=20>�tape 2: Nettoyez les dents et v�rifiez l'alignement.</size>",
        "<size=20>�tape 3: Remplacez les pi�ces endommag�es et remontez l'arbre.</size>"
    };
        }
        else if (OverlayText.text == "bielle")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Bielle</color></size>\n<size=20>�tape 1: V�rifiez l'�tat de la bielle pour des fissures ou de l'usure.</size>",
        "<size=20>�tape 2: D�montez et nettoyez la bielle.</size>",
        "<size=20>�tape 3: Remplacez la bielle si elle est endommag�e, puis remontez.</size>"
    };
        }
        else if (OverlayText.text == "blocmoteur")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Bloc moteur</color></size>\n<size=20>�tape 1: Retirez le bloc moteur et inspectez-le pour des fissures.</size>",
        "<size=20>�tape 2: D�montez les composants internes, tels que les pistons.</size>",
        "<size=20>�tape 3: Nettoyez et remplacez les pi�ces us�es, puis remontez.</size>"
    };
        }
        else if (OverlayText.text == "bougie")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Bougie</color></size>\n<size=20>�tape 1: V�rifiez la bougie pour des signes d'usure ou de d�bris.</size>",
        "<size=20>�tape 2: Nettoyez la bougie ou remplacez-la si n�cessaire.</size>",
        "<size=20>�tape 3: R�installez la bougie et v�rifiez son fonctionnement.</size>"
    };
        }
        else if (OverlayText.text == "capuchon")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Capuchon</color></size>\n<size=20>�tape 1: Inspectez le capuchon pour des fissures ou des signes d'usure.</size>",
        "<size=20>�tape 2: Remplacez le capuchon si n�cessaire.</size>",
        "<size=20>�tape 3: Remontez le capuchon et assurez-vous de son bon ajustement.</size>"
    };
        }
        else if (OverlayText.text == "chainedistribution")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Cha�ne de distribution</color></size>\n<size=20>�tape 1: V�rifiez la tension de la cha�ne de distribution.</size>",
        "<size=20>�tape 2: Remplacez la cha�ne si elle est us�e ou endommag�e.</size>",
        "<size=20>�tape 3: R�installez la nouvelle cha�ne et r�ajustez la tension.</size>"
    };
        }
        else if (OverlayText.text == "collecteurechappement")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Collecteur d'�chappement</color></size>\n<size=20>�tape 1: Inspectez le collecteur d'�chappement pour des fissures ou des fuites.</size>",
        "<size=20>�tape 2: D�montez le collecteur si n�cessaire.</size>",
        "<size=20>�tape 3: Remplacez ou soudez les fissures, puis remontez.</size>"
    };
        }
        else if (OverlayText.text == "couronnedentee")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Couronne dent�e</color></size>\n<size=20>�tape 1: V�rifiez l'usure de la couronne dent�e.</size>",
        "<size=20>�tape 2: D�montez la couronne si elle est endommag�e.</size>",
        "<size=20>�tape 3: Remplacez-la par une nouvelle couronne et remontez.</size>"
    };
        }
        else if (OverlayText.text == "couvreculasse")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Couvre-culasse</color></size>\n<size=20>�tape 1: Retirez le couvre-culasse pour inspection.</size>",
        "<size=20>�tape 2: V�rifiez les joints et les fixations.</size>",
        "<size=20>�tape 3: Remplacez les joints si n�cessaire et remontez le couvre-culasse.</size>"
    };
        }
        else if (OverlayText.text == "culasse")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Culasse</color></size>\n<size=20>�tape 1: Retirez la culasse pour inspection.</size>",
        "<size=20>�tape 2: V�rifiez les soupapes et les guides de soupapes.</size>",
        "<size=20>�tape 3: Remplacez les pi�ces endommag�es et remontez la culasse.</size>"
    };
        }
        else if (OverlayText.text == "cylinder")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Cylindre</color></size>\n<size=20>�tape 1: Inspectez les cylindres pour des rayures ou des fissures.</size>",
        "<size=20>�tape 2: Polissez les cylindres ou remplacez-les si n�cessaires.</size>",
        "<size=20>�tape 3: Remontez les cylindres avec les joints neufs.</size>"
    };
        }
        else if (OverlayText.text == "filtre")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Filtre</color></size>\n<size=20>�tape 1: Retirez le filtre et v�rifiez son �tat.</size>",
        "<size=20>�tape 2: Nettoyez ou remplacez le filtre si n�cessaire.</size>",
        "<size=20>�tape 3: R�installez le filtre et assurez-vous de son bon fonctionnement.</size>"
    };
        }
        else if (OverlayText.text == "goujons")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Goujons</color></size>\n<size=20>�tape 1: V�rifiez les goujons pour des signes de dommage.</size>",
        "<size=20>�tape 2: Remplacez les goujons si n�cessaire.</size>",
        "<size=20>�tape 3: R�installez les goujons neufs et assurez-vous de leur bonne fixation.</size>"
    };
        }
        else if (OverlayText.text == "plaquedesupport")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Plaquette de support</color></size>\n<size=20>�tape 1: Inspectez les plaquettes de support pour tout signe d'usure.</size>",
        "<size=20>�tape 2: Remplacez les plaquettes si n�cessaire.</size>",
        "<size=20>�tape 3: Remontez les nouvelles plaquettes et assurez-vous qu'elles sont bien fix�es.</size>"
    };
        }
        else if (OverlayText.text == "platine")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Platine</color></size>\n<size=20>�tape 1: Retirez la platine et inspectez-la.</size>",
        "<size=20>�tape 2: V�rifiez les fixations et l'�tat des composants.</size>",
        "<size=20>�tape 3: Remplacez les pi�ces endommag�es et remontez la platine.</size>"
    };
        }
        else if (OverlayText.text == "ressort")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Ressort</color></size>\n<size=20>�tape 1: Inspectez les ressorts pour tout signe d'usure ou de rupture.</size>",
        "<size=20>�tape 2: Remplacez les ressorts si n�cessaire.</size>",
        "<size=20>�tape 3: R�installez les nouveaux ressorts et v�rifiez leur tension.</size>"
    };
        }
        else if (OverlayText.text == "tigesoupape")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Tige de soupape</color></size>\n<size=20>�tape 1: Retirez la tige de soupape et inspectez-la.</size>",
        "<size=20>�tape 2: V�rifiez l'usure ou les dommages.</size>",
        "<size=20>�tape 3: Remplacez la tige si n�cessaire et r�installez.</size>"
    };
        }
        else if (OverlayText.text == "tigeculbuteur")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Tige de culbuteur</color></size>\n<size=20>�tape 1: V�rifiez la tige de culbuteur pour tout dommage ou usure.</size>",
        "<size=20>�tape 2: Remplacez la tige si n�cessaire.</size>",
        "<size=20>�tape 3: R�installez la nouvelle tige et assurez-vous de son bon fonctionnement.</size>"
    };
        }
        else if (OverlayText.text == "vilebrequin")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Vilebrequin</color></size>\n<size=20>�tape 1: V�rifiez l'�tat du vilebrequin pour tout jeu ou fissure.</size>",
        "<size=20>�tape 2: D�montez et nettoyez le vilebrequin.</size>",
        "<size=20>�tape 3: Remplacez les pi�ces endommag�es et r�installez le vilebrequin.</size>"
    };
        }
        else if (OverlayText.text == "vis")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Vis</color></size>\n<size=20>�tape 1: Inspectez les vis pour tout dommage ou usure.</size>",
        "<size=20>�tape 2: Remplacez les vis endommag�es.</size>",
        "<size=20>�tape 3: R�installez les nouvelles vis et serrez-les correctement.</size>"
    };
        }
        else if (OverlayText.text == "vishuileamoteur")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Vis huile moteur</color></size>\n<size=20>�tape 1: V�rifiez l'�tat du filtre � huile et du bouchon de vidange.</size>",
        "<size=20>�tape 2: Vidangez l'huile usag�e et remplacez le filtre.</size>",
        "<size=20>�tape 3: Remplissez avec de l'huile neuve et assurez-vous du bon fonctionnement.</size>"
    };
        }
        else
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'�l�ment : Arbre � canne</color></size>\n<size=20>�tape 1: V�rifiez l'�tat de l'objet.</size>",
        "<size=20>�tape 2: Commencez la r�paration en suivant le guide.</size>",
        "<size=20>�tape 3: Faites comme ceci.</size>"
    };
        }

        UpdatePanelText();
    }
    void PlayAudio()
    {
     
      
            if (audioSource != null)
            {
                Debug.LogWarning("okay c'est bon ");

                audioSource.Play();
                count++;
            }
            else
            {
                Debug.LogWarning("Aucun AudioSource assign� !");
            }
        

       
    }

    public void ToggleUI()
    {
        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(IsSelected);

        }
        if (canvasComponent != null)
        {
            canvasComponent.gameObject.SetActive(IsSelected);
        }
        if (infoPanel != null)
        {
            infoPanel.SetActive(IsSelected);
            UpdatePanelText();
        }

        // Afficher ou cacher les boutons en fonction de l'�tat de s�lection
        if (nextPageButton != null)
        {
            nextPageButton.gameObject.SetActive(IsSelected);
        }

        if (previousPageButton != null)
        {
            previousPageButton.gameObject.SetActive(IsSelected);
        }

        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(IsSelected);
        }
    }

    private void UpdatePanelText()
    {
        if (panelText != null)
        {
            panelText.text = pages[currentPage];
            panelText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/YourCoolFont"); // Remplacez par le chemin de votre police
            panelText.fontStyle = FontStyles.Bold;
        }
    }

    private void NextPage()
    {
        currentPage = (currentPage + 1) % pages.Length;  // Passage � la page suivante
        UpdatePanelText();
    }

    private void PreviousPage()
    {
        // Si on est � la premi�re page (currentPage == 0), revenir � la derni�re page
        if (currentPage == 0)
        {
            currentPage = pages.Length - 1;
        }
        else
        {
            currentPage--;  // Sinon, passer � la page pr�c�dente
        }
        UpdatePanelText();
    }

    private void ClosePanel()
    {
        // Logique pour fermer le panneau (peut-�tre d�s�lectionner l'objet)
        Selected = false;
    }

    public void ToggleCanvas()
    {
        if (canvasComponent != null)
        {
            canvasComponent.gameObject.SetActive(IsSelected);
        }
    }
}