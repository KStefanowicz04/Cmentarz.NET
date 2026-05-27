from faker import Faker
import pyodbc
import random
from random import randint
from datetime import date, timedelta
import hashlib

fake = Faker('pl_PL')

# Połączenie z bazą danych
conn = pyodbc.connect(
    "DRIVER={ODBC Driver 18 for SQL Server};"
    "SERVER=localhost,1433;"
    "DATABASE=GraveyardDB;"
    "UID=sa;"
    "PWD=Haslo123!;"
    "Encrypt=no;"
)
cursor = conn.cursor()
if conn:
    print("Połączono!")


## Funkcja do hashowania hasła
def hash_password(password: str) -> bytes:
    return hashlib.sha256(password.encode("utf-8")).digest()



## Liczba rekordów wstawianych do tabel bazy danych zależy od poniższych zmiennych
# Mnożnik rekordów; 1 to wartość podstawowa, np. 1.5 to 50% więcej rekordów, 0.5 to 50% mniej rekordów
multiplier = 1
N_priests = int(20 * multiplier)
N_plots = int(200 * multiplier)
N_plotowners = int(N_plots/4)
N_deceased = int(N_plots/2)
N_users = 50 * multiplier  ## Ta zmienna nie uwzględnia użytkowników utworzonych przy tworzeniu PlotOwner!






## Encje słownikowe
# Wypełnienie tabeli słownikowej BurialDepth
depth_strings = [
    'Standard (1.7m)',
    'Piętrowo (2.5m)',
    'Dla urn (0.7m)',
    'Na powierzchni',
    'Płytko (1m)',
    'Głęboko (4m)'
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT Depth FROM BurialDepth")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for depth_string in depth_strings:
    if depth_string not in existing:
        cursor.execute(
            """
            INSERT INTO BurialDepth (Depth)
            VALUES (?)
            """,
            depth_string
        )

conn.commit()
print(f"Wypełniono słownik BurialDepth!")

# Wypełnienie tabeli słownikowej CausesOfDeath
cause_strings = [
    'Atak serca',
    'Zadźganie',
    'Utopienie',
    'Uduszenie',
    'Powieszenie',
    'Zastrzelenie',
    'Spalenie',
    'Zgniecenie',
    'Porażenie prądem',
    'Wybuch',
    'Uderzenie',
    'Wypadek samochodowy',
    'Kraken',
    'Upadek'
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT Cause FROM CausesOfDeath")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for cause_string in cause_strings:
    if cause_string not in existing:
        cursor.execute(
            """
            INSERT INTO CausesOfDeath (Cause)
            VALUES (?)
            """,
            cause_string
        )

conn.commit()
print(f"Wypełniono słownik CausesOfDeath!")

# Wypełnienie tabeli słownikowej GravestoneInscryptions
inscryption_strings = [
    'Offline na zawsze',
    'Ostatnio widziany: wieczność temu',
    'Rest In Piss',
    'Nareszcie sobie odpocznę',
    'Powodzenia następnym razem',
    '-Koniec to nigdy nie Koniec to nigdy nie Koniec to nigdy nie-',
    'Zrekrutowany do Wojny Szkieletów',
    'Teraz widzi więcej',
    'Teraz widzi mniej',
    'Memento Mori',
    'To Również Przeminie',
    'Na Balu Kreślarzy',
    'Tańczy Danse Macabre',
    'Koniec z projektami!',
    'Nie zdążył zarejestrować się na Linuxa',
    'Żałuję, że nie spędziłem więcej czasu przed komputerem',
    'Tam, na dole',
    'Był brat ciepły, stał się zimny',
    'Po drugiej stronie słońca',
    'Na sztos',
    'Na stos',
    'Już nie człowieczy',
    'Powrócił do gwiazd',
    'I na co to wszystko?',
    '\'Tis just a flesh wound',
    'Jeszcze tu wrócę',
    'Popioły, zgliszcza, gruz',
    'Na niebie świecił będzie i tak Wielki Wóz',
    'Even in death I serve the Omnissiah',
    'Rolling On',
    'W drogę!',
    'W krainie zapomnienia',
    'Dopadł go biały węgorz',
    'Jak kłoda leżę',
    'Czesze się gąbką',
    'Gdzie ta keja?',
    'Tylko kosy błysk',
    'To ja, to ty',
    'To jest to, a to jest tamto',
    'Jeśli nie my, to kto?',
    'Bez tros',
    'Wróci do funfli',
    'Pomogą wam!',
    'Można, jak najbardziej.',
    'Jeszcze jak!',
    'Jak mi dadzą to zjem',
    'W stronę Keplera-22b',
    'Za 7 górami',
    'Dla nich nie ma ładnych rzeczy',
    'Kupisz sobie nowego',
    'Coście uczynili z tą krainą?',
    'Komu bije dzwon?',
    'To początek końca!',
    'Chciał pokazać nierówności kapitalizmu wczesnego',
    'Kompleksy od stuleci',
    'To przepowiedziane!',
    'Noc zakryje wszystkie brudy',
    'O 50 lat za późno',
    'Powrót do przeszłości',
    'Tylko bicie leżącego',
    'Tego nikt już nie powstrzyma',
    'Na co wiedzieć wszystko?',
    'Bliżej ku celom posiadania',
    'Bim-bom, bam-bim-bom',
    'Ten jest ostatni, który nie pierwszy',
    'Byłem czy miałem? Dwie zagadki',
    'Zczezł',
    'Pikawa mu stawa',
    'Kopyrtnął',
    'W ciemnej mogile',
    'Wycofany',
    'Przyniesie wojnę pod twój dom',
    'Stawi na przeciw pana świat',
    'Nie ma wiary bez niewoli',
    'Nie ma bólu, co nie boli',
    'Niech się dzieje wola nieba',
    'Z nią się zawsze zgadzać trzeba',
    'Ja was bracia znam',
    'Od teraz warstwą ziemi',
    'On tańczy, tańczy',
    'Mówi że nigdy nie umrze',
    'Ku zemście Demiurgowi',
    'Na co komu dziś wczorajszy sen?',
    'Co może zmienić naturę człowieka?',
    'Nie ma czego tu szukać',
    'Na dworze',
    'Na polu',
    'Lecz nie tykaj jego samego',
    'Powróci za 400 lat',
    'Nie może krzyczeć',
    'Gdzieś się podział',
    'Padłeś? Powstań. Kanapka z chlebem.'
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT Inscryption FROM GravestoneInscryptions")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for inscryption_string in inscryption_strings:
    if inscryption_string not in existing:
        cursor.execute(
            """
            INSERT INTO GravestoneInscryptions (Inscryption)
            VALUES (?)
            """,
            inscryption_string
        )

conn.commit()
print(f"Wypełniono słownik GravestoneInscryptions!")

# Wypełnienie tabeli słownikowej GraveyardSection
section_strings = [
    'Sekcja Chrześcijańska',
    'Sekcja Protestancka',
    'Sekcja Żołnierzy Radzieckich',
    'Sekcja Islamska',
    'Sekcja Czwartkicka',
    'Sekcja Buddyska',
    'Sekcja Konfuzjańska',
    'Sekcja Chińska',
    'Sekcja Prawosławna',
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT SectionType FROM GraveyardSection")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for section_string in section_strings:
    if section_string not in existing:
        cursor.execute(
            """
            INSERT INTO GraveyardSection (SectionType)
            VALUES (?)
            """,
            section_string
        )

conn.commit()
print(f"Wypełniono słownik GraveyardSection!")

# Wypełnienie tabeli słownikowej Materials
material_strings = [
    'Marmur',
    'Granit',
    'Szkło',
    'Ceramika',
    'Stal',
    'Złoto',
    'Dąb',
    'Brzoza',
    'Baobab',
    'Buk',
    'Topola',
    'Wierzba',
    'Węgiel',
    'Bizmut',
    'Bazalt',
    'Kwarc',
    'Linoleum',
    'Sosna',
    'Cegła',
    'Pustak',
    'Sklejka',
    'Plastik'
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT Type FROM Materials")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for material_string in material_strings:
    if material_string not in existing:
        cursor.execute(
            """
            INSERT INTO Materials (Type)
            VALUES (?)
            """,
            material_string
        )

conn.commit()
print(f"Wypełniono słownik Materials!")

# Wypełnienie tabeli słownikowej Parishes
parish_strings = [
    'Parafia Św. Alll-Mera',
    'Parafia Imienia Gro-gorotha',
    'Parafia Imienia G. Brzęczyszczykiewicza',
    'Parafia Imienia Adama Miauczyńskiego',
    'Parafia Serca Azathotha',
    'Niezależna Organizacja Spełniająca Rolę Parafii Piątego Kościoła Czwartkizmu w Mieście Białystok',
    'Parafia pod przewodnictwem Marii Konopnickiej',
    'Parafia Wschodzącego Słońca',
    'Parafia Trzech Stokrotek',
    'Parafia Sensatów',
    'Parafia Świętego Słowa Eminema',
    'Parafia Organizacji Freie Deutsche Jugend',
    'Parafia Funfli Kopyrtających Gitowców',
    'Parafia Taty Kazika',
    'Parafia Węża Rzecznego'
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT Name FROM Parishes")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for parish_string in parish_strings:
    if parish_string not in existing:
        cursor.execute(
            """
            INSERT INTO Parishes (Name)
            VALUES (?)
            """,
            parish_string
        )

conn.commit()
print(f"Wypełniono słownik Parishes!")

# Wypełnienie tabeli słownikowej Roles
role_strings = [
    'Admin',
    'Moderator',
    'Pracownik',
    'Użytkownik',
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT RoleName FROM Roles")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for role_string in role_strings:
    if role_string not in existing:
        cursor.execute(
            """
            INSERT INTO Roles (RoleName)
            VALUES (?)
            """,
            role_string
        )

conn.commit()
print(f"Wypełniono słownik Roles!")



## Zwykłe tabele

# Wypełnienie tabeli FuneralHomes
home_strings = [
    'Dom Zachodzącego Słońca',
    'Zakład Pogrzebowy A.S. Bytom',
    'Zakład Pogrzebowy Czołówka Piekła',
    'Zakład "Druga Strona"',
    'Zakład Pogrzebowy Los Pollos Hermanos',
    'Zakład Jeden Krok',
    'Zakład Pogrzebowy Zegarmistrz',
    'Zakład 7-me Niebo'
]
# Wartości już umieszczone w tabeli nie zostaną dodane ponownie
cursor.execute("SELECT FuneralHomeName FROM FuneralHomes")
existing = {row[0] for row in cursor.fetchall()}
# Wypełnienie
for home_string in home_strings:
    if home_string not in existing:
        ## Utworzenie losowego ContactData dla danego domu pogrzebowego
        phone = fake.phone_number()
        email = fake.email()
        city = fake.city()
        street = fake.street_address()
        post_code = fake.postcode()
        cursor.execute(
            """
            INSERT INTO ContactDatas (PhoneNumber, EMail, CityName, StreetName, ZipCode)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?, ?)
            """,
            phone, email, city, street, post_code
        )
        contact_data_id = cursor.fetchone()[0]

        cursor.execute(
            """
            INSERT INTO FuneralHomes (FuneralHomeName, ContactDataId)
            VALUES (?, ?)
            """,
            home_string, contact_data_id
        )

conn.commit()
print(f"Wypełniono tabelę FuneralHomes!")


# Wypełnienie tabeli Priests losowymi danymi: imie, nazwisko, nowe ContactData dla danego księdza, id losowej parafii.
## Liczenie liczby Księży; w bazie będzie znajdować się najwyżej N Księży
cursor.execute("SELECT COUNT(*) FROM Priests")
current_count = cursor.fetchone()[0]

if current_count < N_priests:
    remaining = N_priests - current_count

    for i in range(remaining):
        name = fake.first_name()
        surname = fake.last_name()
        ## Po podstawowych danych tworzone jest nowe ContactData dla danego Księdza
        phone = fake.phone_number()
        email = f"{name.lower()}.{surname.lower()}@example.com"
        city = fake.city()
        street = fake.street_address()
        post_code = fake.postcode()
        cursor.execute(
            """
            INSERT INTO ContactDatas (PhoneNumber, EMail, CityName, StreetName, ZipCode)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?, ?)
            """,
            phone, email, city, street, post_code
        )
        contact_data_id = cursor.fetchone()[0]
        
        ## Wybrana zostanie losowa parafia
        cursor.execute("SELECT Id FROM Parishes")
        parish_ids = [row[0] for row in cursor.fetchall()]
        parish_id = random.choice(parish_ids)

        cursor.execute(
            """
            INSERT INTO Priests (FirstName, Surname, ContactDataId, ParishId)
            VALUES (?, ?, ?, ?)
            """,
            name, surname, contact_data_id, parish_id
        )

    conn.commit()
    print(f"Wstawiono {i+1} księży!")



## Wypełnienie tabeli PlotOwners losowymi danymi: imie, nazwisko, nowe ContactData
## Również tworzy konto User dla danego właściciela
# Liczenie liczby Właścicieli; w bazie będzie znajdować się najwyżej N Właścicieli
cursor.execute("SELECT COUNT(*) FROM PlotOwners")
current_count = cursor.fetchone()[0]

if current_count < N_plotowners:
    remaining = N_plotowners - current_count

    # Zebranie ID roli "Użytkownik" w bazie
    cursor.execute("SELECT Id FROM Roles WHERE RoleName = N'Użytkownik'")
    uzytkownik_role_id = cursor.fetchone()[0]

    for i in range(remaining):
        name = fake.first_name()
        surname = fake.last_name()
        ## Po podstawowych danych tworzone jest nowe ContactData dla właściciela
        phone = fake.phone_number()
        email = f"{name.lower()}.{surname.lower()}@example.com"
        city = fake.city()
        street = fake.street_address()
        post_code = fake.postcode()
        cursor.execute(
            """
            INSERT INTO ContactDatas (PhoneNumber, EMail, CityName, StreetName, ZipCode)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?, ?)
            """,
            phone, email, city, street, post_code
        )
        contact_data_id = cursor.fetchone()[0]

        cursor.execute(
            """
            INSERT INTO PlotOwners (FirstName, Surname, ContactDataId)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?)
            """,
            name, surname, contact_data_id
        )
        plotowner_id = cursor.fetchone()[0]



        ## Utworzenie Usera dla danego Użytkownika
        ## Hashowanie hasła (jest i musi być TEN SAM SPOSÓB co w Kontrolerze do Logowania i Kontrolerze do Rejestracji)
        password = hash_password(surname)
        ## Dodanie użytkownika
        cursor.execute(
            """
            INSERT INTO Users (FirstName, Surname, Email, Password, ContactDataId)
            OUTPUT INSERTED.UserId
            VALUES (?, ?, ?, ?, ?)
            """,
            name, surname, email, password, contact_data_id
        )
        user_id = cursor.fetchone()[0]

        ## Dodanie roli Użytkownik dla danego użytkownika
        cursor.execute(
            """
            INSERT INTO RoleUser (RolesId, UsersUserId)
            VALUES (?, ?)
            """,
            uzytkownik_role_id, user_id
        )


        ## Dodanie UserId do PlotOwner
        cursor.execute(
            """
            UPDATE PlotOwners
            SET UserId = ?
            WHERE Id = ?
            """,
            (user_id, plotowner_id)
        )


    conn.commit()
    print(f"Wstawiono {i+1} właścicieli działek!")



# Wypełnienie tabeli Plots losowymi danymi: cena, sekcja cmentarza
## Liczenie liczby Działek; w bazie będzie znajdować się najwyżej (N/2) zajętych Działek
cursor.execute("SELECT COUNT(*) FROM Plots")
current_count = cursor.fetchone()[0]

if current_count < N_plots:
    remaining = N_plots - current_count

    for i in range(remaining):
        ## Wylosowana zostanie wartość działki
        plot_value = randint(200, 10000);

        ## Wybrana zostanie losowa sekcja cmentarza
        cursor.execute("SELECT Id FROM GraveyardSection")
        graveyard_section_ids = [row[0] for row in cursor.fetchall()]
        graveyard_section_id = random.choice(graveyard_section_ids)

        cursor.execute(
            """
            INSERT INTO Plots (PlotValue, GraveyardSectionId)
            VALUES (?, ?)
            """,
            plot_value, graveyard_section_id
        )

        conn.commit()
        print(f"Wstawiono {i+1} działek!")


## Każdy właściciel otrzyma działkę
# Pobranie wszystkich właścicieli BEZ działek
cursor.execute(
    """
    SELECT po.Id
    FROM PlotOwners po
    LEFT JOIN Plots p ON po.Id = p.PlotOwnerId
    WHERE p.Id is NULL
    """
)
owner_ids = [row[0] for row in cursor.fetchall()]
# Pobranie wszystkich działek
cursor.execute("SELECT Id FROM Plots")
plot_ids = [row[0] for row in cursor.fetchall()]
# Wylosowanie działek dla właścicieli
random.shuffle(plot_ids)
assigned_plots = plot_ids[:len(owner_ids)]

i=0
# Przypisanie działek właścicielom
for owner_id, plot_id in zip(owner_ids, assigned_plots):
    cursor.execute(
        """
        UPDATE Plots
        SET PlotOwnerId = ?
        WHERE Id = ?
        """,
        owner_id, plot_id
    )
    i = i+1

conn.commit()
print(f"Przypisano działki {i} właścicielom.")






# Wypełnienie tabeli Deceaseds losowymi danymi: imię, nazwisko, data urodzenia, data śmierci; potem trumna i pogrzeb danego nieboszczyka;
# Również tworzy nowy Casket i wypełnia losowymi danymi: materiał, cena
# Również tworzy nowy Funeral i wypełnia losowymi danymi: data odbycia pogrzebu, nieboszczyk, ksiądz, dom pogrzebowy, działka odbycia pogrzebu
# Również tworzy nowy Grave i wypełnia losowymi danymi: działka, nieboszczyk, głębokość grobu
# Również tworzy DeathCertificate i wypełnia losowymi danymi: data wydania, zakład pogrzebowy, id nieboszczyka, powód śmierci
## Liczenie liczby Nieboszczyków; w bazie będzie znajdować się najwyżej N Nieboszczyków
cursor.execute("SELECT COUNT(*) FROM Deceaseds")
current_count = cursor.fetchone()[0]

if current_count < N_deceased:
    remaining = N_deceased - current_count

    for i in range(remaining):
        ## Tworzenie nieboszczyka
        name = fake.first_name()
        surname = fake.last_name()

        # Wybieranie daty urodzin w przedziale od 1900 do 1943 (czyli data śmierci od 1921 do 2026)
        start_d = date(1921, 1, 1)
        end_d = date(1943, 12, 31)
        d = end_d - start_d
        rand_days = random.randint(0, d.days)
        birth_date = start_d + timedelta(rand_days) ##fake.date_of_birth()
        death_date = birth_date + timedelta(days=randint(8000, 30000))

        ## Utworzenie Nieboszczyka
        cursor.execute(
            """
            INSERT INTO Deceaseds (FirstName, Surname, BirthDate, DeathDate)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?)
            """,
            name, surname, birth_date, death_date
        )
        ## ID nowo utworzonego nieboszczyka
        deceased_id = cursor.fetchone()[0]


        ## Wypełnienie brakujących danych: trumna (CasketId) i pogrzeb (FuneralId)

        ## Utworzenie losowej trumny
        # Najpierw trzeba mieć liczbę Materiałów w tabeli słownikowej Materials
        cursor.execute("SELECT Id FROM Materials")
        material_ids = [row[0] for row in cursor.fetchall()]
        casket_material_id = random.choice(material_ids)
        casket_price = randint(100, 9999)
        ## Utworzenie trumny
        cursor.execute(
            """
            INSERT INTO Casket (MaterialId, Price, DeceasedId)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?)
            """,
            casket_material_id, casket_price, deceased_id
        )
        ## ID nowo utworzonego grobu
        casket_id = cursor.fetchone()[0]


        ## Utworzenie losowego pogrzebu
        # Wybrana zostanie data 3 dni po śmierci nieboszczyka
        funeral_date = death_date + timedelta(days=3)

        # Wybrany zostanie losowy ksiądz
        cursor.execute("SELECT Id FROM Priests")
        priest_ids = [row[0] for row in cursor.fetchall()]
        priest_id = random.choice(priest_ids)

        # Wybrany zostanie losowy Dom Pogrzebowy
        cursor.execute("SELECT Id FROM FuneralHomes")
        funeral_home_ids = [row[0] for row in cursor.fetchall()]
        funeral_home_id = random.choice(funeral_home_ids)

        # Wybrana zostanie losowa działka
        cursor.execute("SELECT Id FROM Plots")
        plot_ids = [row[0] for row in cursor.fetchall()]
        plot_id = random.choice(plot_ids)

        cursor.execute(
            """
            INSERT INTO Funerals (FuneralDate, DeceasedId, PriestId, FuneralHomeId, PlotId)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?, ?)
            """,
            funeral_date, deceased_id, priest_id, funeral_home_id, plot_id
        )
        ## ID nowo utworzonego pogrzebu
        funeral_id = cursor.fetchone()[0]


        ## Utworzenie losowego grobu na danej działce dla danego nieboszczyka
        # Wybrana zostanie losowa głębokość grobu
        cursor.execute("SELECT Id FROM BurialDepth")
        depth_ids = [row[0] for row in cursor.fetchall()]
        depth_id = random.choice(depth_ids)

        cursor.execute(
            """
            INSERT INTO Graves (PlotId, DeceasedId, BurialDepthId)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?)
            """,
            plot_id, deceased_id, depth_id
        )
        ## ID nowo utworzonego grobu
        grave_id = cursor.fetchone()[0]

        ## Utworzenie losowego nagrobka do danego grobu
        # Data instalacji nagrobka (+1-7 dni po pogrzebie)
        installation_date = funeral_date + timedelta(days=randint(1, 7))
        # Losowy materiał nagrobka
        cursor.execute("SELECT Id FROM Materials")
        material_ids = [row[0] for row in cursor.fetchall()]
        gravestone_material_id = random.choice(material_ids)
        # Losowy opis nagrobka
        cursor.execute("SELECT Id FROM GravestoneInscryptions")
        inscryption_ids = [row[0] for row in cursor.fetchall()]
        gravestone_inscryption_id = random.choice(inscryption_ids)
        ## Utworzenie nagrobka
        cursor.execute(
            """
            INSERT INTO Gravestones (InstallationDate, MaterialId, GraveId, GravestoneInscryptionId)
            OUTPUT INSERTED.Id
            VALUES (?, ?, ?, ?)
            """,
            installation_date, gravestone_material_id, grave_id, gravestone_inscryption_id
        )
        ## ID nowo utworzonego nagrobka
        gravestone_id = cursor.fetchone()[0]
        # Aktualizacja grobu o ID nagrobka
        cursor.execute(
            """
            UPDATE Graves
            SET GravestoneId = ?
            WHERE Id = ?
            """,
            gravestone_id, grave_id
        )



        ## Przypisanie FuneralId i GraveId do Deceased
        cursor.execute(
            """
            UPDATE Deceaseds
            SET CasketId = ?, FuneralId = ?
            WHERE Id = ?
            """,
            (casket_id, funeral_id, deceased_id)
        )

        ## Utworzenie losowego Certyfikatu do danego nieboszczyka
        # Data wydania certyfikatu
        issue_date = death_date + timedelta(days=randint(0, 7))
        # Losowy dom pogrzebowy
        cursor.execute("SELECT Id FROM FuneralHomes")
        funeralhome_ids = [row[0] for row in cursor.fetchall()]
        funeralhome_id = random.choice(funeralhome_ids)
        # Losowa przyczyna śmierci
        cursor.execute("SELECT Id FROM CausesOfDeath")
        cod_ids = [row[0] for row in cursor.fetchall()]
        cod_id = random.choice(cod_ids)

        cursor.execute(
            """
            INSERT INTO DeathCertificates (IssueDate, Issuer, DeceasedId, CauseOfDeathId)
            VALUES (?, ?, ?, ?)
            """,
            issue_date, funeralhome_id, deceased_id, cod_id
        )


    conn.commit()
    print(f"Wstawiono {i+1} nieboszczyków, pogrzebów, grobów, certyfikatów!")


# Wypełnienie tabeli Users losowymi zwykłymi użytkownikami. Dodaj również użytkownika admin z rolą Admin.
# Bez zalogowania na to konto dostęp do niektórych stron jest ograniczony.

## Dodanie użytkownika admin, jesli jeszcze nie ma go w bazie.
cursor.execute(
    """
    SELECT * FROM Users
    WHERE Email = 'admin'
    """
)
row = cursor.fetchone()
if (not row):
    print("Admin nie jest w bazie danych. Tworzenie admina.")
    ## Dodanie admina
    password = hash_password('admin')

    cursor.execute("INSERT INTO ContactDatas DEFAULT VALUES")
    cursor.execute("SELECT SCOPE_IDENTITY()")
    contact_data_id = int(cursor.fetchone()[0])

    cursor.execute(
        """
        INSERT INTO Users (FirstName, Surname, Email, Password, ContactDataId)
        OUTPUT INSERTED.UserId
        VALUES (?, ?, ?, ?, ?)
        """,
        'admin', 'admin', 'admin', password, contact_data_id
    )
    admin_id = cursor.fetchone()[0]

    ## Dodanie Roli Admin
    # Zebranie ID roli "Admin" w bazie
    cursor.execute("SELECT Id FROM Roles WHERE RoleName = 'Admin'")
    admin_role_id = cursor.fetchone()[0]
    cursor.execute(
        """
        INSERT INTO RoleUser (RolesId, UsersUserId)
        VALUES (?, ?)
        """,
        admin_role_id, admin_id
    )
    conn.commit()
    print(f"Wstawiono admina!")


## Liczenie liczby użytkowników; w bazie będzie znajdować się najwyżej N użytkowników
cursor.execute("SELECT COUNT(*) FROM Users")
current_count = cursor.fetchone()[0]

if current_count < N_users:
    remaining = N_users - current_count

    # Zebranie ID roli "Użytkownik" w bazie
    cursor.execute("SELECT Id FROM Roles WHERE RoleName = N'Użytkownik'")
    uzytkownik_role_id = cursor.fetchone()[0]

    for i in range(remaining):
        name = fake.first_name()
        surname = fake.last_name()
        email = f"{name.lower()}.{surname.lower()}@example.com"
        ## Hashowanie hasła (jest i musi być TEN SAM SPOSÓB co w Kontrolerze do Logowania i Kontrolerze do Rejestracji)
        password = hash_password(surname)

        # print(f"Imie: {name}, Nazwisko: {surname}, EMail: {email,} Hasło: {password_string}, Hash: {password}\n")

        ## Użytkownicy mają przypisane domyślnie puste ContactData do swojego konta
        cursor.execute("INSERT INTO ContactDatas DEFAULT VALUES")
        cursor.execute("SELECT SCOPE_IDENTITY()")
        contact_data_id = int(cursor.fetchone()[0])

        ## Dodanie użytkownika
        cursor.execute(
            """
            INSERT INTO Users (FirstName, Surname, Email, Password, ContactDataId)
            OUTPUT INSERTED.UserId
            VALUES (?, ?, ?, ?, ?)
            """,
            name, surname, email, password, contact_data_id
        )
        user_id = cursor.fetchone()[0]

        ## Dodanie roli Użytkownik dla danego użytkownika
        cursor.execute(
            """
            INSERT INTO RoleUser (RolesId, UsersUserId)
            VALUES (?, ?)
            """,
            uzytkownik_role_id, user_id
        )

    conn.commit()
    print(f"Wstawiono {i+1} użytkowników!")