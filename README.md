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
        "DefaultConnection": "DBconnectionString"
      },
      "Jwt": {
        "Key": "jwtKey",
        "Issuer": "Server url",
        "Audience": "Server url",
        "ExpiresMinutes": "60"
      },

      "Stripe": {
        "SecretKey": "secretKey",
        "WebhookSecret": "webhook"
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
     cd /var/www/ESP/ESP-API
     dotnet publish -c Release -o /var/www/ESP/publish

     sudo nano /etc/systemd/system/esp-api.service
     [Unit]
     Description=RacePortal API
     
     [Service]
     WorkingDirectory=/var/www/ESP/publish
     ExecStart=/usr/bin/dotnet /var/www/ESP/publish/ESP.API.dll
     Restart=always
     User=root
     
     [Install]
     WantedBy=multi-user.target

     sudo systemctl daemon-reload
     sudo systemctl enable esp-api
     
     sudo systemctl start esp-api
     sudo cp -r dist/* /var/www/html/

     sudo systemctl reload nginx
   ```
   
 







