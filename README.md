Pr�requis
Avant de lancer l'application, assurez-vous d'avoir install� les outils suivants :

.NET SDK pour la construction et l'ex�cution du projet.
Visual Studio.
Instructions pour D�marrer l'Application
Clonez le repo sur votre machine :

Ouvrez le projet dans Visual Studio :

Lancez Visual Studio.
S�lectionnez "Fichier" > "Ouvrir" > "Projet/Solution".
Naviguez jusqu'au dossier du projet clon� et ouvrez le fichier .sln.
Lancement de l'application :

Appuyez sur F5 dans Visual Studio ou s�lectionnez "D�boguer" > "D�marrer le d�bogage" pour lancer l'application.
Alternativement, ouvrez un terminal et ex�cutez les commandes suivantes :

dotnet build
dotnet run

Test des Endpoints :
Utilisez les endpoints suivants pour tester l'API :

Obtenir les Joueurs Tri�s :
R�cup�rer une liste tri�e de joueurs en fonction de leur classement.

URL : /api/players/sortedByRank
M�thode : GET
Obtenir un Joueur par ID :
R�cup�rer des informations sur un joueur sp�cifique en utilisant son ID.

URL : /api/players/{id}
M�thode : GET
Obtenir des Statistiques :
R�cup�rez des statistiques, y compris le pays avec le ratio de victoires le plus �lev�, la masse corporelle moyenne des joueurs et la taille m�diane.

URL : /api/players/statistics
M�thode : GET
N'h�sitez pas � explorer d'autres fonctionnalit�s de l'API et � ajuster les param�tres selon vos besoins.