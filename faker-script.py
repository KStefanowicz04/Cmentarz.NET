from faker import Faker
import pyodbc
import random
from random import randint

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

# # Wypełnienie tabeli Casket
# # Najpierw trzeba mieć liczbę Materiałów w tabeli słownikowej Materials
# cursor.execute("SELECT Id FROM Materials")
# material_ids = [row[0] for row in cursor.fetchall()]
# print(f"Znaleziono {len(material_ids)} materiałów.")

# # Wypełnienie tabeli losowymi materiałami i losowymi cenami
# for i in range(N):
#     material_id = random.choice(material_ids)
#     price = randint(100, 9999)

#     cursor.execute(
#         """
#         INSERT INTO Casket (MaterialId, Price, DeceasedId)
#         VALUES (?, ?, NULL)
#         """,
#         material_id, price
#     )

# conn.commit()
# print(f"Wstawiono {i} trumien!")

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



# Wypełnienie tabeli Funerals losowymi danymi: data odbycia pogrzebu, nieboszczyk, ksiądz, dom pogrzebowy, działka odbycia pogrzebu
for i in range(5):
    ## Wybrany zostanie losowa data
    date = fake.date();

    ## Wybrany zostanie losowy nieboszczyk niemający jeszcze pogrzebu
    cursor.execute("SELECT Id FROM Deceaseds")
    deceaseds = [row[0] for row in cursor.fetchall()]
    deceased = random.choice(deceaseds)

    ## Wybrana zostanie losowa działka
    cursor.execute("SELECT Id FROM Plots")
    plot_ids = [row[0] for row in cursor.fetchall()]
    plot_id = random.choice(plot_ids)

    cursor.execute(
        """
        INSERT INTO Funerals (FuneralDate, DeceasedId, PriestId, FuneralHomeId, PlotId)
        VALUES (?, ?, ?, ?, ?)
        """,
        date, a, b, c, plot_id
    )

conn.commit()
print(f"Wstawiono {i+1} działek!")