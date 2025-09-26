# GroceryApp sprint3 Studentversie  
    
## UC07 Delen boodschappenlijst  
Is compleet  
  
## UC08 Zoeken producten  
Aanvullen:
- In de GroceryListItemsView zitten twee Collection Views, namelijk één voor de inhoud van de boodschappenlijst en één voor producten die je toe kunt voegen aan de boodschappenlijst  
- Voeg boven de tweede CollectionView een zoekveld (SearchBar) in om op producten te kunnen zoeken.  
- Zorg dat de SearchCommand wordt gebonden aan een functie in het onderliggende ViewModel (GroceryListItemsViewModel) en dat de zoekterm die in het zoekveld is ingetypt gebruikt wordt als parameter (SearchCommandParameter).  
- Werk in het viewModel (GroceryListItemsViewModel) de zoekfunctie uit en zorg dat de beschikbare producten worden gefilterd op de zoekterm!  

## UC9 Registratie 
Beschrijving:
Nieuwe gebruikers kunnen zich registreren om toegang te krijgen tot de applicatie en hun eigen boodschappenlijsten te beheren.

Stappen:
1. Gebruiker opent de app en gaat naar het registratiegedeelte.
2. Gebruiker voert Naam, E-mailadres en Wachtwoord in.

Systeem controleert:
- Alle velden zijn ingevuld.
- Wachtwoord bevat minimaal 8 tekens, inclusief minstens één hoofdletter en één cijfer.


Bij fouten toont het systeem een duidelijke melding:
- “Vul alle registratievelden in.”
- “Wachtwoord moet minimaal 8 tekens bevatten, inclusief een hoofdletter en een cijfer.”


Bij succesvolle registratie:
- Het account wordt aangemaakt en opgeslagen in de database.
- Er verschijnt een: “Welkom [Naam]!”
- De gebruiker wordt e direct ingelogd en doorgestuurd naar de hoofdpagina.

Feedback:
- Fouten: rood weergegeven.






  

