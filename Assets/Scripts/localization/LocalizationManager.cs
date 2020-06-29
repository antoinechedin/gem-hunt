using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    public TMP_FontAsset latin;
    public TMP_FontAsset latinOutline;
    public TMP_FontAsset latinShadow;
    public TMP_FontAsset cyrillic;
    public TMP_FontAsset cyrillicOutline;
    public TMP_FontAsset cyrillicShadow;

    public Alphabet Alphabet { get; private set; }

    public delegate void UpdateText();
    public static event UpdateText OnUpdateText;

    private Dictionary<string, string> languages;
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

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
        languages.Add("spanish", spanish);
        languages.Add("portuguese", portuguese);
        languages.Add("russian", russian);
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
        
        if(lang.Equals("russian"))
        {
            Alphabet = Alphabet.Cyrillic;
        }
        else{
            Alphabet = Alphabet.Latin;
        }

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
            ""value"": ""max""
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
            ""key"":""Sugilite"",
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
        },
        {
            ""key"":""Bismuth"",
            ""value"": ""Bismuth""
        },
        {
            ""key"":""Stevonnie"",
            ""value"": ""Stevonie""
        },
        {
            ""key"":""Smoky"",
            ""value"": ""Quartz Fumé ""
        },
        {
            ""key"":""Rainbow"",
            ""value"": ""Arc-en-ciel""
        },
        {
            ""key"":""Sunstone"",
            ""value"": ""Pierre de Soleil""
        },
        {
            ""key"":""Obsidian"",
            ""value"": ""Obsidienne""
        },
        {
            ""key"":""White"",
            ""value"": ""Blanc""
        },
        {
            ""key"":""Yellow"",
            ""value"": ""Jaune""
        },
        {
            ""key"":""Blue"",
            ""value"": ""Bleu""
        },
        {
            ""key"":""Spinel"",
            ""value"": ""Spinelle""
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
            ""value"": ""max""
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
            ""value"": ""{0} won!""
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
            ""key"":""Sugilite"",
            ""value"": ""Sugilite""
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
        },
        {
            ""key"":""Bismuth"",
            ""value"": ""Bismuth""
        },
        {
            ""key"":""Stevonnie"",
            ""value"": ""Stevonie""
        },
        {
            ""key"":""Smoky"",
            ""value"": ""Smoky ""
        },
        {
            ""key"":""Rainbow"",
            ""value"": ""Rainbow""
        },
        {
            ""key"":""Sunstone"",
            ""value"": ""Sunstone""
        },
        {
            ""key"":""Obsidian"",
            ""value"": ""Obsidian""
        },
        {
            ""key"":""White"",
            ""value"": ""White""
        },
        {
            ""key"":""Yellow"",
            ""value"": ""Yellow""
        },
        {
            ""key"":""Blue"",
            ""value"": ""Blue""
        },
        {
            ""key"":""Spinel"",
            ""value"": ""Spinel""
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
            ""value"": ""Nuevo Jogada""
        },
        {
            ""key"": ""start"",
            ""value"": ""Começar""
        },
        {
            ""key"":""player"",
            ""value"":""Jogador""
        },
        {
            ""key"": ""computer"",
            ""value"": ""IA""
        },
        {
            ""key"":""counterText"",
            ""value"": ""máx""
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
            ""value"": ""Vez de {0}""
        },
        {
            ""key"":""winText"",
            ""value"": ""Vitória de {0}""
        },
        {
            ""key"":""results"",
            ""value"": ""Resultados""
        },
        {
            ""key"":""restart"",
            ""value"": ""Recomeçar""
        },
        {
            ""key"":""home"",
            ""value"": ""Menu""
        },
        {
            ""key"":""pause"",
            ""value"": ""Pausa""
        },
        {
            ""key"":""resume"",
            ""value"": ""Resumo""
        },
        {
            ""key"":""rules"",
            ""value"": ""Regras""
        },
        {
            ""key"":""dicePool"",
            ""value"": ""A reserva""
        },
        {
            ""key"":""goodGame"",
            ""value"": ""Bom jogo""
        },
        {
            ""key"":""rules_0_0"",
            ""value"": ""Gem Hunt é um jogo de dados onde você tem que capturar gemmas corrompidas.""
        },
        {
            ""key"":""rules_0_1"",
            ""value"": ""No início do seu turno, 3 dados são tirados aleatoriamente da reserva. Cada um representa uma gema corrompida.""
        },
        {
            ""key"":""rules_0_2"",
            ""value"": ""Os dados verdes são os mais fáceis, os laranjas são os médios e os vermelhos os mais difíceis.""
        },
        {
            ""key"":""rules_1_0"",
            ""value"": ""Os dados têm 3 símbolos:""
        },
        {
            ""key"":""rules_1_1"",
            ""value"": ""Bolha: Você capturou a gema e marcou 1 ponto. Há 3 nos dados verdes, 2 nas laranjas e 1 nos vermelhos.""
        },
        {
            ""key"":""rules_1_2"",
            ""value"": ""Garras: Foi atingido! Perdeste 1 ponto de vida. Há 3 nos dados vermelhos, 2 nas laranjas e 1 nos verdes. ""
        },
        {
            ""key"":""rules_1_3"",
            ""value"": ""Pegadas: A gema escapou. Se você escolher continuar sua caça, você irá reiniciá-la invés de tomar uma nova. Há dois deles, qualquer que seja a cor do dado.""
        },
        {
            ""key"":""rules_2_0"",
            ""value"": ""Se você perder todos os seus pontos de saúde, a sua vez termina e você perde os pontos que ganhou neste turno. Caso contrário, pode optar por parar ou continuar a caça.""
        },
        {
            ""key"":""rules_2_1"",
            ""value"": ""Se escolheres parar, guardas os teus pontos, recuperas os teus pontos de saúde e o próximo jogador começa a sua vez.""
        },
        {
            ""key"":""rules_2_2"",
            ""value"": ""Se você optar por continuar, as bolhas e garras são removidas e novos dados são tirados da reserva para sempre ter 3 dados para jogar.""
        },
        {
            ""key"":""rules_3_0"",
            ""value"": ""Tenha em conta que lançar de novo um dado vermelho é mais arriscado do que lançar um dado verde. Se só te resta um ponto de vida, talvez devesses parar de caçar.""
        },
        {
            ""key"":""rules_3_1"",
            ""value"": ""Preste também atenção à reserva. É arriscado continuar se apenas sobrar dados vermelhos ou laranjas.""
        },
        {
            ""key"":""rules_3_2"",
            ""value"": ""O primeiro jogador de 13 pontos ganha o jogo.""
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
            ""value"": ""Ametista""
        },
        {
            ""key"":""Pearl"",
            ""value"": ""Pérola""
        },
        {
            ""key"":""Ruby"",
            ""value"": ""Rubi""
        },
        {
            ""key"":""Sapphire"",
            ""value"": ""Safira""
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
            ""value"": ""Leão""
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
            ""key"":""Sugilite"",
            ""value"": ""Sugilite""
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
            ""value"": ""Holo-Pérola""
        },
        {
            ""key"":""Bismuth"",
            ""value"": ""Bismuto""
        },
        {
            ""key"":""Stevonnie"",
            ""value"": ""Stevonnie""
        },
        {
            ""key"":""Smoky"",
            ""value"": ""Quartzo Fumê""
        },
        {
            ""key"":""Rainbow"",
            ""value"": ""Arco-Íris""
        },
        {
            ""key"":""Sunstone"",
            ""value"": ""Pedra do Sol""
        },
        {
            ""key"":""Obsidian"",
            ""value"": ""Obsidiana""
        },
        {
            ""key"":""White"",
            ""value"": ""Branco""
        },
        {
            ""key"":""Yellow"",
            ""value"": ""Amarelo""
        },
        {
            ""key"":""Blue"",
            ""value"": ""Azul""
        },
        {
            ""key"":""Spinel"",
            ""value"": ""Spinela""
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
            ""value"": ""máx""
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
            ""key"":""Sugilite"",
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
        },
        {
            ""key"":""Bismuth"",
            ""value"": ""Bismuto""
        },
        {
            ""key"":""Stevonnie"",
            ""value"": ""Stevonie""
        },
        {
            ""key"":""Smoky"",
            ""value"": ""Smoky ""
        },
        {
            ""key"":""Rainbow"",
            ""value"": ""Arcoíris""
        },
        {
            ""key"":""Sunstone"",
            ""value"": ""Sunstone""
        },
        {
            ""key"":""Obsidian"",
            ""value"": ""Obsidiana""
        },
        {
            ""key"":""White"",
            ""value"": ""Blanco""
        },
        {
            ""key"":""Yellow"",
            ""value"": ""Amarillo""
        },
        {
            ""key"":""Blue"",
            ""value"": ""Azul""
        },
        {
            ""key"":""Spinel"",
            ""value"": ""Espinela""
        }
    ]}";

    private string russian = @"{
    ""items"": [
         {
            ""key"":""languages"",
            ""value"": ""Язык""
        },
        {
            ""key"":""newGame"",
            ""value"": ""Новая игра""
        },
        {
            ""key"": ""start"",
            ""value"": ""Начать""
        },
        {
            ""key"":""player"",
            ""value"":""Игрок""
        },
        {
            ""key"": ""computer"",
            ""value"": ""Компьютер""
        },
        {
            ""key"":""counterText"",
            ""value"": ""mакс""
        },
        {
            ""key"":""stop"",
            ""value"": ""Стоп""
        },
        {
            ""key"":""continue"",
            ""value"": ""Продолжить""
        },
        {
            ""key"":""turnText"",
            ""value"": ""Ход {0}""
        },
        {
            ""key"":""winText"",
            ""value"": ""{0} выйграл!""
        },
        {
            ""key"":""results"",
            ""value"": ""Результаты""
        },
        {
            ""key"":""restart"",
            ""value"": ""Повтор""
        },
        {
            ""key"":""home"",
            ""value"": ""На главную""
        },
        {
            ""key"":""pause"",
            ""value"": ""Пауза""
        },
        {
            ""key"":""resume"",
            ""value"": ""Продолжить""
        },
        {
            ""key"":""rules"",
            ""value"": ""Правила""
        },
        {
            ""key"":""dicePool"",
            ""value"": ""Игра в кости""
        },
        {
            ""key"":""goodGame"",
            ""value"": ""Удачи!""
        },
        {
            ""key"":""rules_0_0"",
            ""value"": ""Gem Hunt - это игра в кости, в которой вы Испорченые самоцветы, чтобы поймать их.""
        },
        {
            ""key"":""rules_0_1"",
            ""value"": ""В свою очередь кидаются 3 кубика. Каждый из них представляет собой испорченный драгоценный камень.""
        },
        {
            ""key"":""rules_0_2"",
            ""value"": ""Зеленые кубики-самые легкие, оранжевые-средние, а красные-сложный.""
        },
        {
            ""key"":""rules_1_0"",
            ""value"": ""У кубиков 3 символа:""
        },
        {
            ""key"":""rules_1_1"",
            ""value"": ""Пузырь: вы захватили самоцвет, набрав 1 очко. На зеленом кубике их 3, на оранжевом-2 и только на Красном-1.""
        },
        {
            ""key"":""rules_1_2"",
            ""value"": ""Когти: Атака испорченого самоцвета! Вы теряете 1 очко жизни. Челюсти есть на 3 красных кубиках, 2 на оранжевых и 1 на зеленых.""
        },
        {
            ""key"":""rules_1_3"",
            ""value"": ""Следы: Испорченый самоцвет сбежал. Если вы решите продолжить свою охоту, вы снова бросите эту кость вместо того, чтобы выбрать новую. Всего их 2, независимо от цвета кости.""
        },
        {
            ""key"":""rules_2_0"",
            ""value"": ""Если вы потеряете все сердечки, ваш ход закончится и вы потеряете все очки, которые получили в этом ходе. В любом случае вы можете остановить, или продолжить охоту.""
        },
        {
            ""key"":""rules_2_1"",
            ""value"": ""Если вы решите остановиться, вы сохраняете свои очки, восстанавливаете свое здоровье и ждете своего следующего хода.""
        },
        {
            ""key"":""rules_2_2"",
            ""value"": ""Если вы решите продолжить, кости заменяются новыми случайными костями. Так что вы всегда бросаете 3 кости.""
        },
        {
            ""key"":""rules_3_0"",
            ""value"": ""Имейте в виду, что повторное бросание красных кубиков более опасно, чем зеленых. Если у вас осталось только 1 сердечко,то возможно вам следует прекратить свою охоту.""
        },
        {
            ""key"":""rules_3_1"",
            ""value"": ""То же самое, если после броска остались только красные или оранжевые кости. Будь осторожен.""
        },
        {
            ""key"":""rules_3_2"",
            ""value"": ""Если игрок собирает 13 очков и больше,чем другие игроки,то он побеждает""
        },
        {
            ""key"":""Steven"",
            ""value"": ""Стивена""
        },
        {
            ""key"":""Garnet"",
            ""value"": ""Гранат""
        },
        {
            ""key"":""Amethyst"",
            ""value"": ""Аметист""
        },
        {
            ""key"":""Pearl"",
            ""value"": ""Жемчуг""
        },
        {
            ""key"":""Ruby"",
            ""value"": ""Рубин""
        },
        {
            ""key"":""Sapphire"",
            ""value"": ""Сапфир""
        },
        {
            ""key"":""Peridot"",
            ""value"": ""Перидот""
        },
        {
            ""key"":""Lapis"",
            ""value"": ""Ляпис""
        },
        {
            ""key"":""Greg"",
            ""value"": ""Грега""
        },
        {
            ""key"":""Connie"",
            ""value"": ""Конни""
        },
        {
            ""key"":""Lion"",
            ""value"": ""Льва""
        },
        {
            ""key"":""Jasper"",
            ""value"": ""Джаспер""
        },
        {
            ""key"":""Opal"",
            ""value"": ""Опала""
        },
        {
            ""key"":""Sugilite"",
            ""value"": ""Сагилит""
        },
        {
            ""key"":""Sardonyx"",
            ""value"": ""Сардоникс""
        },
        {
            ""key"":""Alexandrite"",
            ""value"": ""Александрит""
        },
        {
            ""key"":""Holo Pearl"",
            ""value"": ""Голограммы Жемчуг""
        },
        {
            ""key"":""Bismuth"",
            ""value"": ""Висмут""
        },
        {
            ""key"":""Stevonnie"",
            ""value"": ""Стивонни""
        },
        {
            ""key"":""Smoky"",
            ""value"": ""Дымчатого ""
        },
        {
            ""key"":""Rainbow"",
            ""value"": ""Радужного""
        },
        {
            ""key"":""Sunstone"",
            ""value"": ""Камня Солнца""
        },
        {
            ""key"":""Obsidian"",
            ""value"": ""Обсидиана""
        },
        {
            ""key"":""White"",
            ""value"": ""Белого""
        },
        {
            ""key"":""Yellow"",
            ""value"": ""Желтого""
        },
        {
            ""key"":""Blue"",
            ""value"": ""Синего""
        },
        {
            ""key"":""Spinel"",
            ""value"": ""Спинел""
        }
    ]}";
}
