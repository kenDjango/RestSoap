# Velib Project

## Extension: Development

Graphical User Interface for the client.
Replace all the accesses to WS (beetween Velib WS and IWS, between IWS and WS Clients) with asynchronous ones. Some indications can be find just below.
Add a cache in IWS, to reduce communications between Velib WS and IWS.

## Comment ça marche ?

### Le cache:

Ajout de deux structures de données dans le WS. Une liste pour stocker la liste des contracts de JCDecaux, un dictonnaire avec pour clé une ville et 
pour valeur la liste des stations de cette ville. Les structures sont déclarées static afin d'être partagé par toute les instances du WS, en effet a chaque appel d'une des méthodes du WS un nouvel objet est crée.

### Appels Asynchrones:

Ajout d'appels asynchrones au WS de JCDecaux de même lors de l'appel du VWS dans le client graphique.

### Client graphique:

Compsé de deux combobox une première pour selectionner la ville (appel de la méthode getContracts à la création du client) une dexième combobox pour selectionner
la ville et un bouton pour obtenir le nombre de vélos disponibles. Ecouteur d'événement sur la première combobox qui permet de mettre à jour la deuxième
(la clear et la remplit avec le resultat de l'appel au VWS).