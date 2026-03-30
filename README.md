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
   Ajout du fichier appsetting.json dans le dossier /ESP.API
```bash
    {
      "ConnectionStrings": {
        "DefaultConnection": "server=127.0.0.1;port=3306;database=raceportal;user=apiuser;password=ApiPass10!"
      },
      "Jwt": {
        "Key": "D452qs456453qsdKBHFWXDHds241FExtra",
        "Issuer": "https://raceportal.edwrdledgar.me",
        "Audience": "https://raceportal.edwrdledgar.me",
        "ExpiresMinutes": "60"
      },

      "Stripe": {
        "SecretKey": "sk_test_51TBeigE7cDC4ZOdEYvkoXESwAwjMijcghoTrwOsm8CLR79ncyhaBHtAmCOE3zW84yzVGE9qd8c6ZKLpHJQmk1cqu00XUXIBq18",
        "WebhookSecret": "whsec_5Z3M5DmXZXujalGGsoUncohxwAvVEuO6"
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
   Il peut y avoir des problèmes de dépendances si cela arrive, ajouter les dépendances suivantes : 
   ```bash
     cd /var/www/ESP/ESP-API/ESP.WebAPI
     dotnet add package System.IdentityModel.Tokens.Jwt --version 7.1.2
     dotnet add package Microsoft.IdentityModel.Tokens --version 7.1.2
     dotnet add package Microsoft.IdentityModel.Protocols --version 7.1.2
     dotnet add package Microsoft.IdentityModel.Logging --version 7.1.2

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
   
 







