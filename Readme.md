# Pour instancier la bd, exécuter dans

1. Installer ef core https://learn.microsoft.com/en-us/ef/core/get-started/overview/install 
1. Ajouter Pomelo à ton projet qui possède le lien vers la base de données.
1. Faire un build de la solution.
1. Ouvrir une fenêtre console dans visual studio en cliquant droit sur le projet ESP.Infrastructure + terminal
1. Faire la commmande ci-dessous pour importer la base de données dans le code selon l'approche data first

```powershell
dotnet ef dbcontext scaffold "server=localhost;port=3306;database=LE_NOM_DE_TA_BD;uid=root;password=root" Pomelo.EntityFrameworkCore.MySql -o ../ESP.Domain/Entities -c AppDbContext
```

La ligne de commande va te produire un nouveau fichier AppDbContext que tu pourras utiliser pour remplacer celui qui est existant. Toutes tes entités seront ainsi crées à partir de la base de données.


