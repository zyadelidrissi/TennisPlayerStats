Prérequis
Avant de lancer l'application, assurez-vous d'avoir installé les outils suivants :

.NET SDK pour la construction et l'exécution du projet.
Visual Studio.
Instructions pour Démarrer l'Application
Clonez le repo sur votre machine :

Ouvrez le projet dans Visual Studio :

Lancez Visual Studio.
Sélectionnez "Fichier" > "Ouvrir" > "Projet/Solution".
Naviguez jusqu'au dossier du projet cloné et ouvrez le fichier .sln.
Lancement de l'application :

Appuyez sur F5 dans Visual Studio ou sélectionnez "Déboguer" > "Démarrer le débogage" pour lancer l'application.
Alternativement, ouvrez un terminal et exécutez les commandes suivantes :

dotnet build
dotnet run

Test des Endpoints :
Utilisez les endpoints suivants pour tester l'API :

Obtenir les Joueurs Triés :
Récupérer une liste triée de joueurs en fonction de leur classement.

URL : /api/players/sortedByRank
Méthode : GET
Obtenir un Joueur par ID :
Récupérer des informations sur un joueur spécifique en utilisant son ID.

URL : /api/players/{id}
Méthode : GET
Obtenir des Statistiques :
Récupérez des statistiques, y compris le pays avec le ratio de victoires le plus élevé, la masse corporelle moyenne des joueurs et la taille médiane.

URL : /api/players/statistics
Méthode : GET
N'hésitez pas à explorer d'autres fonctionnalités de l'API et à ajuster les paramètres selon vos besoins.