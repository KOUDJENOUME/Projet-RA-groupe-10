using UnityEngine;

public class RepairAnimationManager : MonoBehaviour
{
    // Liste des animators des différents composants
    public Animator arbreACanneAnimator;
    public Animator bielleAnimator;
    public Animator blocMoteurAnimator;
    public Animator bougieAnimator;
    public Animator capuchonTeteAnimator;
    public Animator chaineDistributionAnimator;
    public Animator collecteurEchappementAnimator;
    public Animator couronneDenteeAnimator;
    public Animator couvreCulasseAnimator;
    public Animator culasseAnimator;
    public Animator cylinderAnimator;
    public Animator filtreAnimator;
    public Animator goujonsAnimator;
    public Animator lightAnimator;
    public Animator plaquesSupportAnimator;
    public Animator platineAnimator;
    public Animator ressortsAnimator;
    public Animator tigeSoupapeAnimator;
    public Animator tigeCulbuteurAnimator;
    public Animator vilebrequinAnimator;
    public Animator visAnimator;
    public Animator visFiltreHuileAnimator;


    // Méthode pour déclencher l'animation en fonction du composant
    public void PlayAnimationForComponent(string componentName)
    {
        // Vérifie quel composant a été touché et lance son animation
        switch (componentName)
        {

            case "Arbre à cames":
                arbreACanneAnimator.SetTrigger("ArbreACames");
                break;
            case "bielle.001":
            case "bielle.002":
            case "bielle.003":
            case "bielle.004":
            case "bielle.005":
            case "bielle.006":
            case "bielle.007":
                bielleAnimator.SetTrigger(componentName);
                break;
            case "Bloc moteur":
                blocMoteurAnimator.SetTrigger("BlocMoteur");
                break;
            case "Bougie":
            case "Bougies":
                bougieAnimator.SetTrigger(componentName);
                break;
            case "Capuchon de tete":
            case "Capuchon de tete.001":
                capuchonTeteAnimator.SetTrigger(componentName);
                break;
            case "chaine de distribution":
                chaineDistributionAnimator.SetTrigger("ChaineDeDistribution");
                break;
            case "Collecteur d'échappement":
            case "Collecteur d'échappement.001":
                collecteurEchappementAnimator.SetTrigger(componentName);
                break;
            case "Couronne dentée":
                couronneDenteeAnimator.SetTrigger("CouronneDentee");
                break;
            case "Couvre culasse":
                couvreCulasseAnimator.SetTrigger("CouvreCulasse");
                break;
            case "culasse":
            case "culasse.001":
                culasseAnimator.SetTrigger(componentName);
                break;
            case "Cylinder.01":
            case "Cylinder.021":
            case "Cylinder.025":
            case "Cylinder.027":
            case "Cylinder.030":
            case "Cylinder.032":
            case "Cylinder.034":
            case "Cylinder.036":
            case "Cylinder.040":
            case "Cylinder.041":
                cylinderAnimator.SetTrigger(componentName);
                break;
            case "FILTRE":
                filtreAnimator.SetTrigger("Filtre");
                break;
            case "Goujons":
                goujonsAnimator.SetTrigger("Goujons");
                break;
            case "Light":
                lightAnimator.SetTrigger("Light");
                break;
            case "Plane.052":
            case "Plaques de support":
                plaquesSupportAnimator.SetTrigger(componentName);
                break;
            case "Platine":
            case "Platine.001":
                platineAnimator.SetTrigger(componentName);
                break;
            case "ressorts":
            case "ressorts.001":
                ressortsAnimator.SetTrigger(componentName);
                break;
            case "tige de soupape":
            case "tige de soupape.001":
            case "tige de soupape.002":
            case "tige de soupape.003":
                tigeSoupapeAnimator.SetTrigger(componentName);
                break;
            case "Tige de culbuteur":
            case "Tige de culbuteur.001":
                tigeCulbuteurAnimator.SetTrigger(componentName);
                break;
            case "Vilebrequin":
                vilebrequinAnimator.SetTrigger("Vilebrequin");
                break;
            case "VIS":
                visAnimator.SetTrigger("Vis");
                break;
            case "vis du filtre à huile":
                visFiltreHuileAnimator.SetTrigger("VisFiltreHuile");
                break;

            default:
                Debug.LogWarning("Composant non reconnu : " + componentName);
                break;
        }
    }

    void Start()
    {

        arbreACanneAnimator.enabled = false;
        bielleAnimator.enabled = false;
        blocMoteurAnimator.enabled = false;
        bougieAnimator.enabled = false;
        capuchonTeteAnimator.enabled = false;
        chaineDistributionAnimator.enabled = false;
        collecteurEchappementAnimator.enabled = false;
        couronneDenteeAnimator.enabled = false;
        couvreCulasseAnimator.enabled = false;
        culasseAnimator.enabled = false;
        cylinderAnimator.enabled = false;
        filtreAnimator.enabled = false;
        goujonsAnimator.enabled = false;
        lightAnimator.enabled = false;
        plaquesSupportAnimator.enabled = false;
        platineAnimator.enabled = false;
        ressortsAnimator.enabled = false;
        tigeSoupapeAnimator.enabled = false;
        tigeCulbuteurAnimator.enabled = false;
        vilebrequinAnimator.enabled = false;
        visAnimator.enabled = false;
        visFiltreHuileAnimator.enabled = false;
    }

    // Méthode pour gérer les interactions tactiles (sur téléphone)
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Réactive l'Animator du composant touché
                    Animator touchedAnimator = hit.collider.gameObject.GetComponent<Animator>();
                    if (touchedAnimator != null)
                    {
                        touchedAnimator.enabled = true;
                        // Appelle la méthode qui joue l'animation du composant touché
                        PlayAnimationForComponent(hit.collider.gameObject.name);
                    }
                }
            }
        }
    }
}
