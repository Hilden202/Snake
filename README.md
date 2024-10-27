🐍 Snake - Console Game

Välkommen till mitt Snake-spel! Detta är en klassisk Snake-implementation för konsolen, utvecklad i C#. Spelet är enkelt att spela men ändå utmanande – perfekt för nostalgiska spelare och de som vill prova en tidlös klassiker. Här är en översikt över spelet och instruktioner för hur du kommer igång!

🎮 Funktioner

Klassisk Snake-spelmekanik – Styra din orm för att äta "skatter" och växa.
Ökande svårighetsgrad – Ormen ökar i hastighet ju mer den äter.
Topp 10-highscore-lista – Spara dina högsta poäng och jämför dem med dina tidigare försök.
Pausfunktion – Pausa spelet när du behöver en paus och fortsätt där du slutade.
Spelplan med gränser – Klassiska väggar runt spelplanen som gör spelet mer utmanande.

🕹️ Spelinstruktioner

Starta spelet: Kör programmet från konsolen.
Riktningskontroller:
Upp: Piltangent "Upp"
Ner: Piltangent "Ner"
Vänster: Piltangent "Vänster"
Höger: Piltangent "Höger"
Pausa/Fortsätt: Tryck på mellanslag för att pausa och fortsätta spelet.
Avsluta spelet: Tryck på "Q" för att avsluta spelet.

🏆 Highscore-lista

När spelet är slut, jämförs din poäng med den befintliga topplistan. Om din poäng är tillräckligt hög får du möjligheten att ange ditt namn och spara poängen i highscores.txt.

🚀 Installation

Klona projektet från GitHub:

git clone https://github.com/Hilden202/Snake.git

Navigera till projektmappen:

cd snake-console-game

Kompilera och kör spelet (för .NET Core-användare):
dotnet run

🛠️ Teknikstack

Programspråk: C#
Plattform: Konsolapplikation
Highscore-lagring: highscores.txt lagrar highscore-listan för framtida försök.
