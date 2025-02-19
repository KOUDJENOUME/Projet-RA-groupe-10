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
    private Button previousPageButton; // Nouveau bouton précédent

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
            previousPageButton.onClick.AddListener(PreviousPage); // Action de retour à la page précédente
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ClosePanel);  // Action de fermeture du panneau
        }

        // Vérifie le texte dans OverlayText et définit les pages en conséquence
        if (OverlayText.text == "Arbre à canne")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Arbre à canne</color></size>\n<size=20>Étape 1 : Vérifiez l'état de l'arbre à canne.</size>",
        "<size=20>Étape 2: Nettoyez les dents et vérifiez l'alignement.</size>",
        "<size=20>Étape 3: Remplacez les pièces endommagées et remontez l'arbre.</size>"
    };
        }
        else if (OverlayText.text == "bielle")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Bielle</color></size>\n<size=20>Étape 1: Vérifiez l'état de la bielle pour des fissures ou de l'usure.</size>",
        "<size=20>Étape 2: Démontez et nettoyez la bielle.</size>",
        "<size=20>Étape 3: Remplacez la bielle si elle est endommagée, puis remontez.</size>"
    };
        }
        else if (OverlayText.text == "blocmoteur")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Bloc moteur</color></size>\n<size=20>Étape 1: Retirez le bloc moteur et inspectez-le pour des fissures.</size>",
        "<size=20>Étape 2: Démontez les composants internes, tels que les pistons.</size>",
        "<size=20>Étape 3: Nettoyez et remplacez les pièces usées, puis remontez.</size>"
    };
        }
        else if (OverlayText.text == "bougie")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Bougie</color></size>\n<size=20>Étape 1: Vérifiez la bougie pour des signes d'usure ou de débris.</size>",
        "<size=20>Étape 2: Nettoyez la bougie ou remplacez-la si nécessaire.</size>",
        "<size=20>Étape 3: Réinstallez la bougie et vérifiez son fonctionnement.</size>"
    };
        }
        else if (OverlayText.text == "capuchon")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Capuchon</color></size>\n<size=20>Étape 1: Inspectez le capuchon pour des fissures ou des signes d'usure.</size>",
        "<size=20>Étape 2: Remplacez le capuchon si nécessaire.</size>",
        "<size=20>Étape 3: Remontez le capuchon et assurez-vous de son bon ajustement.</size>"
    };
        }
        else if (OverlayText.text == "chainedistribution")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Chaîne de distribution</color></size>\n<size=20>Étape 1: Vérifiez la tension de la chaîne de distribution.</size>",
        "<size=20>Étape 2: Remplacez la chaîne si elle est usée ou endommagée.</size>",
        "<size=20>Étape 3: Réinstallez la nouvelle chaîne et réajustez la tension.</size>"
    };
        }
        else if (OverlayText.text == "collecteurechappement")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Collecteur d'échappement</color></size>\n<size=20>Étape 1: Inspectez le collecteur d'échappement pour des fissures ou des fuites.</size>",
        "<size=20>Étape 2: Démontez le collecteur si nécessaire.</size>",
        "<size=20>Étape 3: Remplacez ou soudez les fissures, puis remontez.</size>"
    };
        }
        else if (OverlayText.text == "couronnedentee")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Couronne dentée</color></size>\n<size=20>Étape 1: Vérifiez l'usure de la couronne dentée.</size>",
        "<size=20>Étape 2: Démontez la couronne si elle est endommagée.</size>",
        "<size=20>Étape 3: Remplacez-la par une nouvelle couronne et remontez.</size>"
    };
        }
        else if (OverlayText.text == "couvreculasse")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Couvre-culasse</color></size>\n<size=20>Étape 1: Retirez le couvre-culasse pour inspection.</size>",
        "<size=20>Étape 2: Vérifiez les joints et les fixations.</size>",
        "<size=20>Étape 3: Remplacez les joints si nécessaire et remontez le couvre-culasse.</size>"
    };
        }
        else if (OverlayText.text == "culasse")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Culasse</color></size>\n<size=20>Étape 1: Retirez la culasse pour inspection.</size>",
        "<size=20>Étape 2: Vérifiez les soupapes et les guides de soupapes.</size>",
        "<size=20>Étape 3: Remplacez les pièces endommagées et remontez la culasse.</size>"
    };
        }
        else if (OverlayText.text == "cylinder")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Cylindre</color></size>\n<size=20>Étape 1: Inspectez les cylindres pour des rayures ou des fissures.</size>",
        "<size=20>Étape 2: Polissez les cylindres ou remplacez-les si nécessaires.</size>",
        "<size=20>Étape 3: Remontez les cylindres avec les joints neufs.</size>"
    };
        }
        else if (OverlayText.text == "filtre")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Filtre</color></size>\n<size=20>Étape 1: Retirez le filtre et vérifiez son état.</size>",
        "<size=20>Étape 2: Nettoyez ou remplacez le filtre si nécessaire.</size>",
        "<size=20>Étape 3: Réinstallez le filtre et assurez-vous de son bon fonctionnement.</size>"
    };
        }
        else if (OverlayText.text == "goujons")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Goujons</color></size>\n<size=20>Étape 1: Vérifiez les goujons pour des signes de dommage.</size>",
        "<size=20>Étape 2: Remplacez les goujons si nécessaire.</size>",
        "<size=20>Étape 3: Réinstallez les goujons neufs et assurez-vous de leur bonne fixation.</size>"
    };
        }
        else if (OverlayText.text == "plaquedesupport")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Plaquette de support</color></size>\n<size=20>Étape 1: Inspectez les plaquettes de support pour tout signe d'usure.</size>",
        "<size=20>Étape 2: Remplacez les plaquettes si nécessaire.</size>",
        "<size=20>Étape 3: Remontez les nouvelles plaquettes et assurez-vous qu'elles sont bien fixées.</size>"
    };
        }
        else if (OverlayText.text == "platine")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Platine</color></size>\n<size=20>Étape 1: Retirez la platine et inspectez-la.</size>",
        "<size=20>Étape 2: Vérifiez les fixations et l'état des composants.</size>",
        "<size=20>Étape 3: Remplacez les pièces endommagées et remontez la platine.</size>"
    };
        }
        else if (OverlayText.text == "ressort")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Ressort</color></size>\n<size=20>Étape 1: Inspectez les ressorts pour tout signe d'usure ou de rupture.</size>",
        "<size=20>Étape 2: Remplacez les ressorts si nécessaire.</size>",
        "<size=20>Étape 3: Réinstallez les nouveaux ressorts et vérifiez leur tension.</size>"
    };
        }
        else if (OverlayText.text == "tigesoupape")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Tige de soupape</color></size>\n<size=20>Étape 1: Retirez la tige de soupape et inspectez-la.</size>",
        "<size=20>Étape 2: Vérifiez l'usure ou les dommages.</size>",
        "<size=20>Étape 3: Remplacez la tige si nécessaire et réinstallez.</size>"
    };
        }
        else if (OverlayText.text == "tigeculbuteur")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Tige de culbuteur</color></size>\n<size=20>Étape 1: Vérifiez la tige de culbuteur pour tout dommage ou usure.</size>",
        "<size=20>Étape 2: Remplacez la tige si nécessaire.</size>",
        "<size=20>Étape 3: Réinstallez la nouvelle tige et assurez-vous de son bon fonctionnement.</size>"
    };
        }
        else if (OverlayText.text == "vilebrequin")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Vilebrequin</color></size>\n<size=20>Étape 1: Vérifiez l'état du vilebrequin pour tout jeu ou fissure.</size>",
        "<size=20>Étape 2: Démontez et nettoyez le vilebrequin.</size>",
        "<size=20>Étape 3: Remplacez les pièces endommagées et réinstallez le vilebrequin.</size>"
    };
        }
        else if (OverlayText.text == "vis")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Vis</color></size>\n<size=20>Étape 1: Inspectez les vis pour tout dommage ou usure.</size>",
        "<size=20>Étape 2: Remplacez les vis endommagées.</size>",
        "<size=20>Étape 3: Réinstallez les nouvelles vis et serrez-les correctement.</size>"
    };
        }
        else if (OverlayText.text == "vishuileamoteur")
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Vis huile moteur</color></size>\n<size=20>Étape 1: Vérifiez l'état du filtre à huile et du bouchon de vidange.</size>",
        "<size=20>Étape 2: Vidangez l'huile usagée et remplacez le filtre.</size>",
        "<size=20>Étape 3: Remplissez avec de l'huile neuve et assurez-vous du bon fonctionnement.</size>"
    };
        }
        else
        {
            pages = new string[] {
        "<size=40><color=red>Nom de l'élément : Arbre à canne</color></size>\n<size=20>Étape 1: Vérifiez l'état de l'objet.</size>",
        "<size=20>Étape 2: Commencez la réparation en suivant le guide.</size>",
        "<size=20>Étape 3: Faites comme ceci.</size>"
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
                Debug.LogWarning("Aucun AudioSource assigné !");
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

        // Afficher ou cacher les boutons en fonction de l'état de sélection
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
        currentPage = (currentPage + 1) % pages.Length;  // Passage à la page suivante
        UpdatePanelText();
    }

    private void PreviousPage()
    {
        // Si on est à la première page (currentPage == 0), revenir à la dernière page
        if (currentPage == 0)
        {
            currentPage = pages.Length - 1;
        }
        else
        {
            currentPage--;  // Sinon, passer à la page précédente
        }
        UpdatePanelText();
    }

    private void ClosePanel()
    {
        // Logique pour fermer le panneau (peut-être désélectionner l'objet)
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