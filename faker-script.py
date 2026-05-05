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
#     full_name = fake.name().split(' ')
#     name = full_name[0]
#     surname = full_name[1]
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