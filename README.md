
---

# JWT Auth with React and .NET

### Opis
Aplikacija predstavlja full-stack sistem za upravljanje podacima o studentima, koristeći <a href="">React za frontend</a> i .NET Core za backend. U aplikaciji su implementirani autentifikacija i autorizacija uz pomoć JWT (JSON Web Token) tehnologije, čime se omogućava sigurno registrovanje, logovanje, i pristup zaštićenim rutama. Aplikacija takođe podržava CRUD operacije nad podacima o studentima.

---

## Funkcionalnosti

### Autentifikacija i autorizacija
Autentifikacija i autorizacija su ključne komponente za bezbednost aplikacije.

- **Autentifikacija**: Proces potvrđivanja identiteta korisnika, obezbeđujući da je korisnik onaj za koga se predstavlja. U ovoj aplikaciji autentifikacija je omogućena putem JWT tokena, koji se generiše prilikom uspešnog logovanja.
- **Autorizacija**: Kontrola pristupa resursima na osnovu korisničkih prava. Samo autentifikovani korisnici sa važećim JWT tokenom imaju pristup zaštićenim rutama, kao što su pregled profila i ažuriranje podataka.

### Registracija i logovanje
- **Registracija**: Kreiranje novog korisničkog naloga pomoću jedinstvenog korisničkog imena i lozinke.
- **Logovanje**: Prijava u sistem uz generisanje JWT tokena, koji omogućava pristup zaštićenim endpoint-ovima.

### CRUD operacije nad studentima
CRUD operacije omogućavaju kreiranje, čitanje, ažuriranje i brisanje podataka o studentima:

1. **Create (Dodavanje studenta)**: Omogućava administratoru ili autorizovanom korisniku da doda novog studenta u sistem.
2. **Read (Pregled studenata)**: Prikazuje listu svih studenata, uključujući mogućnost pregleda detalja o pojedinačnom studentu.
3. **Update (Ažuriranje podataka o studentu)**: Omogućava ažuriranje specifičnih informacija o studentu, kao što su ime i druge relevantne informacije.
4. **Delete (Brisanje studenta)**: Omogućava brisanje podataka o studentu iz sistema.

---

## API Rute

### Autentifikacija i autorizacija

- **POST** `/api/Students/register` - Registracija novog korisnika. 
- **POST** `/api/Students/login` - Logovanje korisnika i generisanje JWT tokena.

### CRUD operacije nad studentima

- **GET** `/api/Students` - Vraća listu svih studenata (zaštićena ruta).
- **GET** `/api/Students/profile` - Vraća detalje o studentu sa specifičnim ID-jem (zaštićena ruta).
- **POST** `/api/Students` - Dodaje novog studenta (zaštićena ruta).
- **PUT** `/api/Students/{id}` - Ažurira podatke o studentu sa specifičnim ID-jem (zaštićen.
- **PATCH** `/api/Students/{id}/name` - Ažurira samo ime studenta sa specifičnim ID-jem .
- **DELETE** `/api/Students/{id}` - Briše studenta sa specifičnim ID-jem .

---

## Tehnologije

- **Backend**: .NET Core, ASP.NET Core, Entity Framework Core
- **Frontend**: React
- **Autentifikacija**: JSON Web Token (JWT)

---
