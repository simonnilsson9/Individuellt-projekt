# Individuellt projekt Simon Nilsson SUT23
Detta är en C# kod som har använts med .NET 6 som console App. Mitt uppdrag var att skapa en internetbank/bankomat som har flera olika funktioner. Användaren får mata in inloggningssuppgifter och sedan göra val utifrån en meny. Där kan man kolla konton, överföra mellan konton och ta ut pengar. 


# Förbättringar och reflektioner
Jag använder mig utav Arrays i min lösning. Jag sparar användarnamn, lösenord, konton, summan på konton i olika separata arrays. För att sedan jämföra så att arrayen matchar användaren så använder jag mig utav metoden Array.IndeOf som med hjälp av användarens inmatning kan se ifall den stämmer överens med befintliga arrayen. Detta är ganska komplicerat och ifall man hade velat ha en bättre struktur kring hela systemet så hade man kunnat använda sig utav Klasser. Då hade man mycket enklare kunnat spara all information för en användare i en klass. På så sätt så hade det framförallt blivit väldigt mycket tydligare för vilka värden som hör till rätt användare. 
Min struktur på programmet är att jag har använt mig av metoder på varje funktion i programmet. Detta för att det ska bli lättare för användaren att förstå hur det hela är uppbyggt och för att jag lätt ska kunna använda samma bit kod på flera olika ställen i programmet. Detta gör att det blir mindre kod och lättare att förstå. 
