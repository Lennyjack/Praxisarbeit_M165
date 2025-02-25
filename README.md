# Skiservice-Website mit .NET 8 & MongoDB

## Projektbeschreibung
Dies ist eine Webanwendung für einen Skiservice, die eine Benutzerregistrierung, Anmeldung und Verwaltung von Service-Aufträgen ermöglicht. Die Anwendung basiert auf **.NET 8**, **MongoDB** und verwendet ein einfaches **HTML/CSS (Bootstrap) Frontend mit JavaScript**.

## Technologien
- **Backend**: .NET 8 (Web API)
- **Datenbank**: MongoDB
- **Frontend**: HTML, CSS (Bootstrap), JavaScript
- **Authentication**: JWT (JSON Web Token)

---

## Setup & Installation

### **1. Backend starten**
1. **Repository klonen**
```bash
git clone https://github.com/dein-repo/skiservice.git
cd skiservice
```
2. **Benötigte Pakete installieren**
```bash
dotnet restore
```
3. **Datenbankverbindung konfigurieren**  
In der `appsettings.json` die MongoDB-Verbindungszeichenfolge anpassen:
```json
"ConnectionStrings": {
  "MongoDb": "mongodb://localhost:27017/SkiserviceDB"
}
```
4. **Projekt starten**
```bash
dotnet run
```
Backend sollte nun unter `http://localhost:5000` laufen.

---

### **2. Frontend starten**
1. **Frontend-Dateien öffnen**
   - Navigiere zum `frontend`-Ordner und öffne `index.html` in einem Browser.
   - Stelle sicher, dass dein Backend aktiv ist.
2. **Login testen**
   - Registriere dich über das Formular.
   - Nach erfolgreicher Anmeldung kannst du Aufträge erstellen.

---

## API-Endpunkte

### **1. Benutzerverwaltung**
| Methode | Route | Beschreibung |
|---------|----------------|------------------------------|
| **POST** | `/api/users/register` | Neuen Benutzer registrieren |
| **POST** | `/api/users/login` | Benutzer einloggen |
| **GET** | `/api/users/me` | Eigene Daten abrufen |
| **PUT** | `/api/users/me` | Eigene Daten aktualisieren |
| **GET** | `/api/users` | Alle Benutzer abrufen (Admin) |

### **2. Service-Aufträge**
| Methode | Route | Beschreibung |
|---------|----------------|------------------------------|
| **POST** | `/api/orders` | Neuen Service-Auftrag erstellen |
| **GET** | `/api/orders` | Eigene Aufträge abrufen |
| **GET** | `/api/orders/{id}` | Einzelnen Auftrag abrufen |
| **PUT** | `/api/orders/{id}` | Eigenen Auftrag bearbeiten |
| **DELETE** | `/api/orders/{id}` | Eigenen Auftrag löschen |

### **3. Admin-Funktionen**
| Methode | Route | Beschreibung |
|---------|----------------|------------------------------|
| **GET** | `/api/admin/orders` | Alle Aufträge abrufen |
| **PUT** | `/api/admin/orders/{id}` | Auftrag eines Users bearbeiten |
| **GET** | `/api/admin/users` | Alle Benutzer abrufen |
| **GET** | `/api/admin/users/{id}` | Einzelnen Benutzer abrufen |

---

## Verbesserungen & To-Do
- [ ] Responsive Design verbessern
- [ ] Mehr Filteroptionen für Service-Aufträge
- [ ] Automatische Email-Bestätigung nach Registrierung

---

## Lizenz
Dieses Projekt steht unter der **MIT-Lizenz**.


---

## Autoren
- **Ege Ulu**
- **Lenny Brun**

