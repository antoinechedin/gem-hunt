using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    public delegate void UpdateText();
    public static event UpdateText OnUpdateText;

    private Dictionary<string, string> languages;
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        languages = new Dictionary<string, string>();
        languages.Add("french", french);
        languages.Add("english", english);
        languages.Add("portuguese", portuguese);
    }

    public void UpdateAllText()
    {
        OnUpdateText();
    }

    public void LoadLocalizedText(string lang)
    {
        isReady = false;
        localizedText = new Dictionary<string, string>();

        string dataAsJson = languages[lang];

        Localization loadedData = JsonUtility.FromJson<Localization>(dataAsJson);

        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

    private string french = @"{
    ""items"": [
         {
            ""key"":""languages"",
            ""value"": ""Langues""
        },
        {
            ""key"":""newGame"",
            ""value"": ""Nouvelle Partie""
        },
        {
            ""key"": ""start"",
            ""value"": ""Commencer""
        },
        {
            ""key"":""player"",
            ""value"":""Joueur""
        },
        {
            ""key"": ""computer"",
            ""value"": ""Ordinateur""
        },
        {
            ""key"":""counterText"",
            ""value"": ""{0}/5 max""
        },
        {
            ""key"":""stop"",
            ""value"": ""Arrêter""
        },
        {
            ""key"":""continue"",
            ""value"": ""Continuer""
        },
        {
            ""key"":""turnText"",
            ""value"": ""C'est à {0}""
        },
        {
            ""key"":""winText"",
            ""value"": ""{0} a gagné !""
        },
        {
            ""key"":""results"",
            ""value"": ""Résultats""
        },
        {
            ""key"":""restart"",
            ""value"": ""Recommencer""
        },
        {
            ""key"":""home"",
            ""value"": ""Accueil""
        },
        {
            ""key"":""pause"",
            ""value"": ""Pause""
        },
        {
            ""key"":""resume"",
            ""value"": ""Reprendre""
        },
        {
            ""key"":""rules"",
            ""value"": ""Règles""
        },
        {
            ""key"":""dicePool"",
            ""value"": ""La réserve""
        },
        {
            ""key"":""goodGame"",
            ""value"": ""Bonne chance""
        },
        {
            ""key"":""rules_0_0"",
            ""value"": ""Gem Hunt est un jeu de dés où il faut capturer des gemmes corrompues.""
        },
        {
            ""key"":""rules_0_1"",
            ""value"": ""Au début de votre tour, 3 dés sont pris aléatoirement de la réserve. Chacun représente une gemme corrompue.""
        },
        {
            ""key"":""rules_0_2"",
            ""value"": ""Les dés verts sont les plus simples, les oranges sont moyens, et les rouges sont les plus durs.""
        },
        {
            ""key"":""rules_1_0"",
            ""value"": ""Les dés ont 3 symboles :""
        },
        {
            ""key"":""rules_1_1"",
            ""value"": ""Bulle : Tu as capturé la gemme, et tu marques 1 point. Il y en a 3 sur les dés verts, 2 sur les oranges et 1 sur les rouges.""
        },
        {
            ""key"":""rules_1_2"",
			""value"": ""Griffe : Tu as pris un coup ! Tu perds 1 point de vie. Il y en a 3 sur les dés verts, 2 sur les oranges et 1 sur les verts.""
        },
        {
            ""key"":""rules_1_3"",
            ""value"": ""Traces : La gemme s'est échappée. Si tu choisis de continuer ta chasse, tu le relanceras au lieu d'en prendre un nouveau. Il y en a 2 quelle que soit la couleur du dé.""
        },
        {
            ""key"":""rules_2_0"",
            ""value"": ""Si tu perds tous tes points de vie, ton tour se termine et tu perds les points que tu as gagné ce tour. Sinon tu peux choisir d'arrêter ou de continuer la chasse.""
        },
        {
            ""key"":""rules_2_1"",
            ""value"": ""Si tu choisis d'arrêter, tu sauvegardes tes points, reprends tes points de vie et le joueur suivant commence son tour.""
        },
        {
            ""key"":""rules_2_2"",
            ""value"": ""Si tu choisis de continuer, les bulles et les griffes sont mis à l'écart et de nouveaux dés sont tirées de la réserve pour toujours avoir 3 dés. à lancer.""
        },
        {
            ""key"":""rules_3_0"",
            ""value"": ""Garde en tête que relancer un dé rouge comporte plus de risques qu'avec un dé vert. Si tu n'as plus qu'un point de vie, tu devrais peut-être arrêter ta chasse.""
        },
        {
            ""key"":""rules_3_1"",
            ""value"": ""Fait aussi attention à la réserve. Il est risqué de continuer s'il ne reste que des dés rouges ou oranges.""
        },
        {
            ""key"":""rules_3_2"",
            ""value"": ""Le premier joueur à 13 points gagne la partie.""
        },
        {
            ""key"":""Steven"",
            ""value"": ""Steven""
        },
        {
            ""key"":""Garnet"",
            ""value"": ""Grenat""
        },
        {
            ""key"":""Amethyst"",
            ""value"": ""Améthyste""
        },
        {
            ""key"":""Pearl"",
            ""value"": ""Perle""
        },
        {
            ""key"":""Ruby"",
            ""value"": ""Rubis""
        },
        {
            ""key"":""Sapphire"",
            ""value"": ""Saphire""
        },
        {
            ""key"":""Peridot"",
            ""value"": ""Péridot""
        },
        {
            ""key"":""Lapis"",
            ""value"": ""Lapis""
        },
        {
            ""key"":""Greg"",
            ""value"": ""Greg""
        },
        {
            ""key"":""Connie"",
            ""value"": ""Connie""
        },
        {
            ""key"":""Lion"",
            ""value"": ""Lion""
        },
        {
            ""key"":""Jasper"",
            ""value"": ""Jaspe""
        },
        {
            ""key"":""Opal"",
            ""value"": ""Opale""
        },
        {
            ""key"":""Sugulite"",
            ""value"": ""Lavulite""
        },
        {
            ""key"":""Sardonyx"",
            ""value"": ""Sardonyx""
        },
        {
            ""key"":""Alexandrite"",
            ""value"": ""Alexandrite""
        },
        {
            ""key"":""Holo Pearl"",
            ""value"": ""Holo Perle""
        }
    ]}";

    private string english = @"{
    ""items"": [
         {
            ""key"":""languages"",
            ""value"": ""Languages""
        },
        {
            ""key"":""newGame"",
            ""value"": ""New Game""
        },
        {
            ""key"": ""start"",
            ""value"": ""Start""
        },
        {
            ""key"":""player"",
            ""value"":""Player""
        },
        {
            ""key"": ""computer"",
            ""value"": ""Computer""
        },
        {
            ""key"":""counterText"",
            ""value"": ""{0}/5 max""
        },
        {
            ""key"":""stop"",
            ""value"": ""Stop""
        },
        {
            ""key"":""continue"",
            ""value"": ""Continue""
        },
        {
            ""key"":""turnText"",
            ""value"": ""{0}'s turn""
        },
        {
            ""key"":""winText"",
            ""value"": ""{0} wins!""
        },
        {
            ""key"":""results"",
            ""value"": ""Results""
        },
        {
            ""key"":""restart"",
            ""value"": ""Restart""
        },
        {
            ""key"":""home"",
            ""value"": ""Home""
        },
        {
            ""key"":""pause"",
            ""value"": ""Pause""
        },
        {
            ""key"":""resume"",
            ""value"": ""Resume""
        },
        {
            ""key"":""rules"",
            ""value"": ""Rules""
        },
        {
            ""key"":""dicePool"",
            ""value"": ""Dice pool""
        },
        {
            ""key"":""goodGame"",
            ""value"": ""Good luck""
        },
        {
            ""key"":""rules_0_0"",
            ""value"": ""Gem Hunt is a dice game where you track corrupted gems to capture them.""
        },
        {
            ""key"":""rules_0_1"",
            ""value"": ""On your turn, 3 dice are taken from the pool and rolled. Each represents a corrupted gem.""
        },
        {
            ""key"":""rules_0_2"",
            ""value"": ""Green dices are the easiest, orange are medium though and red are the thoughest.""
        },
        {
            ""key"":""rules_1_0"",
            ""value"": ""The dices have 3 symbols:""
        },
        {
            ""key"":""rules_1_1"",
            ""value"": ""Bubble: You caputered the gem, scoring 1 point. There's 3 of them on green dice, 2 on orange and only 1 on red.""
        },
        {
            ""key"":""rules_1_2"",
            ""value"": ""Claws: The gem fought back! You lose 1 life point. There's 3 of them on red dice, 2 on orange and 1 on green.""
        },
        {
            ""key"":""rules_1_3"",
            ""value"": ""Footprints: The gem escaped. If you choose to continue your hunt, you will re-roll this dice instead of picking a new one. There's 2 of them, no matter the dice color.""
        },
        {
            ""key"":""rules_2_0"",
            ""value"": ""If you lose all your life points, your turn is over and you lose all the points you got this turn. Otherwise, you can choose to stop or to continue the hunt.""
        },
        {
            ""key"":""rules_2_1"",
            ""value"": ""If you choose to stop, you save your points, you restore your health and the next player's turn begins.""
        },
        {
            ""key"":""rules_2_2"",
            ""value"": ""If you choose to continue, bubble and claws dice are set aside and new random dice are taken from the pool so you always have 3 dice to roll.""
        },
        {
            ""key"":""rules_3_0"",
            ""value"": ""Keep in mind that re-rolling red dice are more dangerous than green ones. If you only have 1 life point left, maybe you should stop your hunt.""
        },
        {
            ""key"":""rules_3_1"",
            ""value"": ""Same thing if there's only red or orange dice left in the pool. So keep an eye on it.""
        },
        {
            ""key"":""rules_3_2"",
            ""value"": ""The first player to have 13 points or more wins the game.""
        },
        {
            ""key"":""Steven"",
            ""value"": ""Steven""
        },
        {
            ""key"":""Garnet"",
            ""value"": ""Garnet""
        },
        {
            ""key"":""Amethyst"",
            ""value"": ""Amethyst""
        },
        {
            ""key"":""Pearl"",
            ""value"": ""Pearl""
        },
        {
            ""key"":""Ruby"",
            ""value"": ""Ruby""
        },
        {
            ""key"":""Sapphire"",
            ""value"": ""Sapphire""
        },
        {
            ""key"":""Peridot"",
            ""value"": ""Peridot""
        },
        {
            ""key"":""Lapis"",
            ""value"": ""Lapis""
        },
        {
            ""key"":""Greg"",
            ""value"": ""Greg""
        },
        {
            ""key"":""Connie"",
            ""value"": ""Connie""
        },
        {
            ""key"":""Lion"",
            ""value"": ""Lion""
        },
        {
            ""key"":""Jasper"",
            ""value"": ""Jasper""
        },
        {
            ""key"":""Opal"",
            ""value"": ""Opal""
        },
        {
            ""key"":""Sugulite"",
            ""value"": ""Sugulite""
        },
        {
            ""key"":""Sardonyx"",
            ""value"": ""Sardonyx""
        },
        {
            ""key"":""Alexandrite"",
            ""value"": ""Alexandrite""
        },
        {
            ""key"":""Holo Pearl"",
            ""value"": ""Holo Pearl""
        }
    ]}";

    private string portuguese = @"{
    ""items"": [
         {
            ""key"":""languages"",
            ""value"": ""Idiomas""
        },
        {
            ""key"":""newGame"",
            ""value"": ""Nuevo juego""
        },
        {
            ""key"": ""start"",
            ""value"": ""Inicio""
        },
        {
            ""key"":""player"",
            ""value"":""Jugador""
        },
        {
            ""key"": ""computer"",
            ""value"": ""Ordenador""
        },
        {
            ""key"":""counterText"",
            ""value"": ""{0}/5 máx""
        },
        {
            ""key"":""stop"",
            ""value"": ""Parar""
        },
        {
            ""key"":""continue"",
            ""value"": ""Continuar""
        },
        {
            ""key"":""turnText"",
            ""value"": ""El turno de {0}""
        },
        {
            ""key"":""winText"",
            ""value"": ""¡{0} gana!""
        },
        {
            ""key"":""results"",
            ""value"": ""Resultados""
        },
        {
            ""key"":""restart"",
            ""value"": ""Reiniciar""
        },
        {
            ""key"":""home"",
            ""value"": ""Recepción""
        },
        {
            ""key"":""pause"",
            ""value"": ""Pausa""
        },
        {
            ""key"":""resume"",
            ""value"": ""Reanudar""
        },
        {
            ""key"":""rules"",
            ""value"": ""Reglas""
        },
        {
            ""key"":""dicePool"",
            ""value"": ""La reserva""
        },
        {
            ""key"":""goodGame"",
            ""value"": ""Buena suerte""
        },
        {
            ""key"":""rules_0_0"",
            ""value"": ""Gem Hunt es un juego de dados en el que puedes rastrear las gemas corruptas para capturarlas.""
        },
        {
            ""key"":""rules_0_1"",
            ""value"": ""En tu turno, se cogen 3 dados de la reserva y se tira. Cada uno representa una gema corrupta.""
        },
        {
            ""key"":""rules_0_2"",
            ""value"": ""Los dados verdes son los más fáciles, los naranjas son medianos y los rojos son los más comunes.""
        },
        {
            ""key"":""rules_1_0"",
            ""value"": ""Los dados tienen 3 símbolos:""
        },
        {
            ""key"":""rules_1_1"",
            ""value"": ""Burbuja: Capturó la gema, anotando un punto. Hay 3 de ellos en los dados verdes, 2 en los naranjas y sólo 1 en los rojos.""
        },
        {
            ""key"":""rules_1_2"",
            ""value"": ""Garras: ¡La gema se defendió! Pierdes un punto de vida. Hay 3 de ellos en los dados rojos, 2 en naranja y 1 en verde.""
        },
        {
            ""key"":""rules_1_3"",
            ""value"": ""Huellas: La gema escapó. Si eliges continuar tu cacería, volverás a tirar los dados en lugar de elegir uno nuevo. Hay dos de ellos, sin importar el color de los dados.""
        },
        {
            ""key"":""rules_2_0"",
            ""value"": ""Si pierdes todos tus puntos de vida, tu turno se acaba y pierdes todos los puntos que tienes en ese turno. De lo contrario, puede elegir entre detenerse o continuar la cacería.""
        },
        {
            ""key"":""rules_2_1"",
            ""value"": ""Si decides parar, guardas tus puntos, recuperas tu salud y comienza el turno del siguiente jugador.""
        },
        {
            ""key"":""rules_2_2"",
            ""value"": ""Si decides continuar, los dados de burbujas y garras se separan y se sacan nuevos dados al azar de la quiniela para que siempre tengas 3 dados para tirar.""
        },
        {
            ""key"":""rules_3_0"",
            ""value"": ""Ten en cuenta que volver a tirar los dados rojos es más peligroso que los verdes. Si sólo te queda un punto de vida, tal vez deberías dejar de cazar.""
        },
        {
            ""key"":""rules_3_1"",
            ""value"": ""Lo mismo si sólo quedan dados rojos o naranjas en la piscina. Así que vigílalo.""
        },
        {
            ""key"":""rules_3_2"",
            ""value"": ""El primer jugador que tenga 13 puntos o más gana el juego.""
        },
        {
            ""key"":""Steven"",
            ""value"": ""Steven""
        },
        {
            ""key"":""Garnet"",
            ""value"": ""Garnet""
        },
        {
            ""key"":""Amethyst"",
            ""value"": ""Amatista""
        },
        {
            ""key"":""Pearl"",
            ""value"": ""Perla""
        },
        {
            ""key"":""Ruby"",
            ""value"": ""Rubí""
        },
        {
            ""key"":""Sapphire"",
            ""value"": ""Zafiro""
        },
        {
            ""key"":""Peridot"",
            ""value"": ""Peridot""
        },
        {
            ""key"":""Lapis"",
            ""value"": ""Lapislázuli""
        },
        {
            ""key"":""Greg"",
            ""value"": ""Greg""
        },
        {
            ""key"":""Connie"",
            ""value"": ""Connie""
        },
        {
            ""key"":""Lion"",
            ""value"": ""León""
        },
        {
            ""key"":""Jasper"",
            ""value"": ""Jaspe""
        },
        {
            ""key"":""Opal"",
            ""value"": ""Ópalo""
        },
        {
            ""key"":""Sugulite"",
            ""value"": ""Sugalite""
        },
        {
            ""key"":""Sardonyx"",
            ""value"": ""Sardonyx""
        },
        {
            ""key"":""Alexandrite"",
            ""value"": ""Alejandrita""
        },
        {
            ""key"":""Holo Pearl"",
            ""value"": ""Holoperla""
        }
    ]}";

    private string spanish = @"{
    ""items"": [
         {
            ""key"":""languages"",
            ""value"": ""Idiomas""
        },
        {
            ""key"":""newGame"",
            ""value"": ""Nuevo juego""
        },
        {
            ""key"": ""start"",
            ""value"": ""Inicio""
        },
        {
            ""key"":""player"",
            ""value"":""Jugador""
        },
        {
            ""key"": ""computer"",
            ""value"": ""Ordenador""
        },
        {
            ""key"":""counterText"",
            ""value"": ""{0}/5 máx""
        },
        {
            ""key"":""stop"",
            ""value"": ""Parar""
        },
        {
            ""key"":""continue"",
            ""value"": ""Continuar""
        },
        {
            ""key"":""turnText"",
            ""value"": ""El turno de {0}""
        },
        {
            ""key"":""winText"",
            ""value"": ""¡{0} gana!""
        },
        {
            ""key"":""results"",
            ""value"": ""Resultados""
        },
        {
            ""key"":""restart"",
            ""value"": ""Reiniciar""
        },
        {
            ""key"":""home"",
            ""value"": ""Recepción""
        },
        {
            ""key"":""pause"",
            ""value"": ""Pausa""
        },
        {
            ""key"":""resume"",
            ""value"": ""Reanudar""
        },
        {
            ""key"":""rules"",
            ""value"": ""Reglas""
        },
        {
            ""key"":""dicePool"",
            ""value"": ""La reserva""
        },
        {
            ""key"":""goodGame"",
            ""value"": ""Buena suerte""
        },
        {
            ""key"":""rules_0_0"",
            ""value"": ""Gem Hunt es un juego de dados en el que puedes rastrear las gemas corruptas para capturarlas.""
        },
        {
            ""key"":""rules_0_1"",
            ""value"": ""En tu turno, se cogen 3 dados de la reserva y se tira. Cada uno representa una gema corrupta.""
        },
        {
            ""key"":""rules_0_2"",
            ""value"": ""Los dados verdes son los más fáciles, los naranjas son medianos y los rojos son los más comunes.""
        },
        {
            ""key"":""rules_1_0"",
            ""value"": ""Los dados tienen 3 símbolos:""
        },
        {
            ""key"":""rules_1_1"",
            ""value"": ""Burbuja: Capturó la gema, anotando un punto. Hay 3 de ellos en los dados verdes, 2 en los naranjas y sólo 1 en los rojos.""
        },
        {
            ""key"":""rules_1_2"",
            ""value"": ""Garras: ¡La gema se defendió! Pierdes un punto de vida. Hay 3 de ellos en los dados rojos, 2 en naranja y 1 en verde.""
        },
        {
            ""key"":""rules_1_3"",
            ""value"": ""Huellas: La gema escapó. Si eliges continuar tu cacería, volverás a tirar los dados en lugar de elegir uno nuevo. Hay dos de ellos, sin importar el color de los dados.""
        },
        {
            ""key"":""rules_2_0"",
            ""value"": ""Si pierdes todos tus puntos de vida, tu turno se acaba y pierdes todos los puntos que tienes en ese turno. De lo contrario, puede elegir entre detenerse o continuar la cacería.""
        },
        {
            ""key"":""rules_2_1"",
            ""value"": ""Si decides parar, guardas tus puntos, recuperas tu salud y comienza el turno del siguiente jugador.""
        },
        {
            ""key"":""rules_2_2"",
            ""value"": ""Si decides continuar, los dados de burbujas y garras se separan y se sacan nuevos dados al azar de la quiniela para que siempre tengas 3 dados para tirar.""
        },
        {
            ""key"":""rules_3_0"",
            ""value"": ""Ten en cuenta que volver a tirar los dados rojos es más peligroso que los verdes. Si sólo te queda un punto de vida, tal vez deberías dejar de cazar.""
        },
        {
            ""key"":""rules_3_1"",
            ""value"": ""Lo mismo si sólo quedan dados rojos o naranjas en la piscina. Así que vigílalo.""
        },
        {
            ""key"":""rules_3_2"",
            ""value"": ""El primer jugador que tenga 13 puntos o más gana el juego.""
        },
        {
            ""key"":""Steven"",
            ""value"": ""Steven""
        },
        {
            ""key"":""Garnet"",
            ""value"": ""Garnet""
        },
        {
            ""key"":""Amethyst"",
            ""value"": ""Amatista""
        },
        {
            ""key"":""Pearl"",
            ""value"": ""Perla""
        },
        {
            ""key"":""Ruby"",
            ""value"": ""Rubí""
        },
        {
            ""key"":""Sapphire"",
            ""value"": ""Zafiro""
        },
        {
            ""key"":""Peridot"",
            ""value"": ""Peridot""
        },
        {
            ""key"":""Lapis"",
            ""value"": ""Lapislázuli""
        },
        {
            ""key"":""Greg"",
            ""value"": ""Greg""
        },
        {
            ""key"":""Connie"",
            ""value"": ""Connie""
        },
        {
            ""key"":""Lion"",
            ""value"": ""León""
        },
        {
            ""key"":""Jasper"",
            ""value"": ""Jaspe""
        },
        {
            ""key"":""Opal"",
            ""value"": ""Ópalo""
        },
        {
            ""key"":""Sugulite"",
            ""value"": ""Sugalite""
        },
        {
            ""key"":""Sardonyx"",
            ""value"": ""Sardonyx""
        },
        {
            ""key"":""Alexandrite"",
            ""value"": ""Alejandrita""
        },
        {
            ""key"":""Holo Pearl"",
            ""value"": ""Holoperla""
        }
    ]}";
}