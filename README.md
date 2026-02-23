 # RacePortal

 > Application codée en C# pour la back-end avec une API .NET, en React pour le front-end. 
 
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

 # Demarage du serveur en dev :
  Front-end : 
   ```bash
     npm run dev
   ```
   Back-end :
   ```bash
     dotnet run
   ```

 # Demarage du serveur en prod :
  Front-end : 
   ```bash
     npm run build
   ```
   Back-end :
   ```bash
     sudo systemctl start (nom de l'api)
     sudo cp -r dist/* /var/www/html/

     sudo systemctl reload nginx
   ```
   
 







