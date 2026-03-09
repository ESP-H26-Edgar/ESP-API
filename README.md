 # RacePortal

 > Application codée en C# pour la back-end avec une API en .NET
 
 # Prérequis
 
  ## Installation de Node.js
  ```bash
    curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
    sudo apt install nodejs -y
  ```
  ## Installation de .NET
  ```bash
    wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    sudo apt update

    sudo apt install -y dotnet-sdk-8.0 aspnetcore-runtime-8.0
  ```
  ## Installation de MariaDB
   ```bash
     sudo apt install mariadb-server mariadb-client -y
  ```
  ## Installation de Nginx
  ```bash
     sudo apt install nginx -y
     sudo systemctl start nginx
     sudo systemctl enable nginx
  ```

## Ajout important 
   1. Ajout du fichier .env à la racine du projet
      ```bash
      DB_HOST=(Votre ip)
      DB_PORT=(Votre port)
      DB_NAME=(votre nom de base de données)
      DB_USER=(votre user)
      DB_PASSWORD=(votre mot de passe)
      ```
   3. Ajout du fichier appsetting.json dans le dossier /ESP.API
       ```bash
       {
      "ConnectionStrings": {
          "DefaultConnection": "Server=%DB_HOST%;Port=%DB_PORT%;Database=%DB_NAME%;User=%DB_USER%;Password=%DB_PASSWORD%"
      },
      "Jwt": {
          "Key": "(Votre TokenJWT ici)",
          "ExpiresMinutes": "60"
      },
      "Logging": {
          "LogLevel": {
              "Default": "Information",
              "Microsoft.AspNetCore": "Warning"
          }
      },
      "AllowedHosts": "*"
       }
       ```

 4. Ajoute le package jwt
   ```bash
     cd /var/www/ESP/ESP-API/ESP.WebAPI
     dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   ```
 # Demarage du serveur en dev :
   Back-end :
   ```bash
     dotnet run
   ```

 # Demarage du serveur en prod :
   Back-end :
   ```bash
     sudo systemctl start (nom de l'api)
     sudo cp -r dist/* /var/www/html/

     sudo systemctl reload nginx
   ```
   
 







