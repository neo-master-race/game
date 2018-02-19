# Convention de codage

## Systèmes de casse utilisés et autres règles d'écriture

### lower camel case
Tous les mots sont liés sans espaces ni `_`, avec une lettre capitale au début de chaque mot, sauf au premier. 
Exemple :

 - `objectName`
 - `object`

### upper camel case
Idem que pour le lower camel case, mais avec la première lettre en majuscule

 - `ObjectName`
 - `Object`

### Autres règles
 - Aucun `_` pour permettre une lisibilité des blocs de nom, sauf en cas de full upper case (voir constantes).

## Règles de nommage

### Variables
Système : lower camel case

```c#
int nomDeMaVariable ;
```

### Booléens
Un booléen commence toujours par un `is` puis l'état ou la valeur binaire testée.
```c#
bool isChecked;
bool isValidated;
```
Quelques variations avec les prefixes `has` ou `on` sont les bienvenues si elles sont pertinentes.
```c#
bool hasItem;
bool onValidation;
```

### Fonctions
Système : lower camel case
```c#
int renderTextures() ;
```

### Classes et structures
Nom : upper camel case
Attributs et méthodes : voir variables et fonctions
```c#
public class NomDeMaClasse ;
```

### Enumérations
Nom : upper camel case
Valeurs : full upper case (avec `_` autorisés)
```c#
public enum Mode { VERSUS, TOURNAMENT, AGAINST_THE_CLOCK};
```

### Constantes
Système : full upper case (avec `_` autorisés)

## Autour du code

### Fichiers
A la création, décrire rapidement en en-tête la fonction du fichier et de son ensemble de fonctions/objets. Indiquer également la date de création (pour ne pas avoir à fouiller dans GitLab) ainsi que le premier auteur. Les mises à jour de la descriptions sont appréciées.

### Documentation
Tout objet (variable, fonction, classe...) doit venir avec une ligne de commentaire pour expliquer sa signification/fonctionnement. Un nom d'auteur est souhaité pour les fonctions.