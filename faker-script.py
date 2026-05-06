from faker import Faker
import pyodbc
import random
from random import randint
from datetime import timedelta 

fake = Faker('pl_PL')

## Liczba nowych rekordów do dodania
N = 100

# Połączenie z bazą danych
conn = pyodbc.connect(
    "DRIVER={ODBC Driver 18 for SQL Server};"
    "SERVER=(localdb)\MSSQLLocalDB;"
    "DATABASE=GraveyardDB;"
    "Trusted_Connection=yes;"
)
cursor = conn.cursor()
if conn:
    print("Połączono!")

# # Wypełnienie tabeli ContactData losowymi danymi kontaktowymi: #telefonu, email, miasto, ulica, kod pocztowy
# for i in range(N):
#     phone = fake.phone_number()
#     email = fake.email()
#     city = fake.city()
#     street = fake.street_address()
#     post_code = fake.postcode()

#     cursor.execute(
#         """
#         INSERT INTO ContactDatas (PhoneNumber, EMail, CityName, StreetName, ZipCode)
#         VALUES (?, ?, ?, ?, ?)
#         """,
#         phone, email, city, street, post_code
#     )

# conn.commit()
# print(f"Wstawiono {i+1} danych kontaktowych!")



# # Wypełnienie tabeli Priests losowymi danymi: imie, nazwisko, nowe ContactData dla danego księdza, id losowej parafii.
# for i in range(N):
#     name = fake.first_name()
#     surname = fake.last_name()
#     ## Po podstawowych danych tworzone jest nowe ContactData dla danego Księdza
#     phone = fake.phone_number()
#     email = fake.email()
#     city = fake.city()
#     street = fake.street_address()
#     post_code = fake.postcode()
#     cursor.execute(
#         """
#         INSERT INTO ContactDatas (PhoneNumber, EMail, CityName, StreetName, ZipCode)
#         OUTPUT INSERTED.Id
#         VALUES (?, ?, ?, ?, ?)
#         """,
#         phone, email, city, street, post_code
#     )
#     contact_data_id = cursor.fetchone()[0]
    
#     ## Wybrana zostanie losowa parafia
#     cursor.execute("SELECT Id FROM Parishes")
#     parish_ids = [row[0] for row in cursor.fetchall()]
#     parish_id = random.choice(parish_ids)

#     cursor.execute(
#         """
#         INSERT INTO Priests (FirstName, Surname, ContactDataId, ParishId)
#         VALUES (?, ?, ?, ?)
#         """,
#         name, surname, contact_data_id, parish_id
#     )

# conn.commit()
# print(f"Wstawiono {i+1} księży!")



# # Wypełnienie tabeli PlotOwners losowymi danymi: imie, nazwisko, nowe ContactData
# for i in range(N):
#     name = fake.first_name()
#     surname = fake.last_name()
#     ## Po podstawowych danych tworzone jest nowe ContactData dla właściciela
#     phone = fake.phone_number()
#     email = fake.email()
#     city = fake.city()
#     street = fake.street_address()
#     post_code = fake.postcode()
#     cursor.execute(
#         """
#         INSERT INTO ContactDatas (PhoneNumber, EMail, CityName, StreetName, ZipCode)
#         OUTPUT INSERTED.Id
#         VALUES (?, ?, ?, ?, ?)
#         """,
#         phone, email, city, street, post_code
#     )
#     contact_data_id = cursor.fetchone()[0]

#     cursor.execute(
#         """
#         INSERT INTO PlotOwners (FirstName, Surname, ContactDataId)
#         VALUES (?, ?, ?)
#         """,
#         name, surname, contact_data_id
#     )

# conn.commit()
# print(f"Wstawiono {i+1} właścicieli działek!")



# # Wypełnienie tabeli Plots losowymi danymi: właściciel działki, sekcja cmentarza
# for i in range(N):
#     ## Wybrany zostanie losowy właściciel działki
#     cursor.execute("SELECT Id FROM PlotOwners")
#     contact_data_ids = [row[0] for row in cursor.fetchall()]
#     contact_data_id = random.choice(contact_data_ids)

#     ## Wybrana zostanie losowa sekcja cmentarza
#     cursor.execute("SELECT Id FROM GraveyardSection")
#     graveyard_section_ids = [row[0] for row in cursor.fetchall()]
#     graveyard_section_id = random.choice(graveyard_section_ids)

#     cursor.execute(
#         """
#         INSERT INTO Plots (PlotOwnerId, GraveyardSectionId)
#         VALUES (?, ?)
#         """,
#         contact_data_id, graveyard_section_id
#     )

# conn.commit()
# print(f"Wstawiono {i+1} działek!")



# Wypełnienie tabeli Deceaseds losowymi danymi: imię, nazwisko, data urodzenia, data śmierci; potem trumna i pogrzeb danego nieboszczyka;
# Również tworzy nowy Casket i wypełnia losowymi danymi: materiał, cena
# Również tworzy nowy Funeral i wypełnia losowymi danymi: data odbycia pogrzebu, nieboszczyk, ksiądz, dom pogrzebowy, działka odbycia pogrzebu
# Również tworzy nowy Grave i wypełnia losowymi danymi: działa, nieboszczyk, głębokość grobu
    for i in range(200):
        ## Tworzenie nieboszczyka
        name = fake.first_name()
        surname = fake.last_name()
        birth_date = fake.date_of_birth()
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
        date = death_date + timedelta(days=3)

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
            date, deceased_id, priest_id, funeral_home_id, plot_id
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
            VALUES (?, ?, ?)
            """,
            plot_id, deceased_id, depth_id
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


    conn.commit()
print(f"Wstawiono {i+1} nieboszczyków, pogrzebów, grobów!")