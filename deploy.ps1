# -----------------------------
# Script de déploiement sur serveur
# -----------------------------

# -----------------------------
# CONFIGURATION
# -----------------------------
$serverUser = "edgar"
$serverIP = "IP_DE_TON_SERVEUR"
$serverPath = "/var/www/ESP-API"  # dossier sur le serveur
$localProject = $localProject = "C:\Users\edgar\ONEDRI~1\CEGEP~1\HIVER2~1\ESP\DEV\ESP-API"
$remoteUrl = "https://github.com/EdgarCegepRDL/ESP-H26-Edgar.git"

# -----------------------------
# SE PLACER DANS LE PROJET LOCAL
# -----------------------------
Set-Location $localProject

# -----------------------------
# CREATION D'UNE BRANCHE (OPTIONNEL)
# -----------------------------
$createBranch = Read-Host "Voulez-vous créer une nouvelle branche ? (y/n)"
if ($createBranch -eq "y") {
    $branchName = Read-Host "Entrez le nom de la nouvelle branche"
    git checkout -b $branchName
    Write-Host "Nouvelle branche '$branchName' créée ✅"
} else {
    $branchName = git branch --show-current
    Write-Host "Vous êtes sur la branche existante : $branchName"
}

# -----------------------------
# MESSAGE DU COMMIT
# -----------------------------
$commitMessage = Read-Host "Entrez le message du commit"
if ([string]::IsNullOrEmpty($commitMessage)) {
    $commitMessage = "Mise à jour du projet"
}

# -----------------------------
# AJOUT ET COMMIT
# -----------------------------
git add .
git commit -m "$commitMessage"

# -----------------------------
# CONFIGURATION DU REMOTE
# -----------------------------
git remote remove origin -ErrorAction SilentlyContinue
git remote add origin $remoteUrl

# -----------------------------
# PUSH SUR GITHUB
# -----------------------------
git push -u origin $branchName
Write-Host "✅ Projet poussé sur GitHub sur la branche '$branchName'"

# -----------------------------
# DEPLOIEMENT SUR SERVEUR
# -----------------------------
Write-Host "Déploiement sur serveur..."
ssh $serverUser@$serverIP "cd $serverPath || git clone $remoteUrl $serverPath; cd $serverPath; git checkout $branchName; git pull origin $branchName"

Write-Host "✅ Déploiement terminé sur le serveur"
