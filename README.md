# Uruchomienie projektu.

## Wymagania
Potrzebne są:
+ Visual Studio
+ (opt) Docker + WSL
+ Python
+ Kod projektu
+ (opt) SQL Server Management Studio

## Przygotowanie projektu
Projekt korzysta z SQLServer.<br>
Domyślnie jest to sqlserwer postawiony za pomocą Dockera:<br>
    ```
        docker pull mcr.microsoft.com/mssql/server:2025-latest
    ```
    <br>
    ```
        docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Haslo123!" -p 1433:1433 -v sqlvolume:/var/opt/mssql --name sqlserver -d mcr.microsoft.com/server:2025-latest
    

Serwer aplikacji łączy się z bazą danych kontem Użytkownika bazy danych "Server".<br>
Tego Użytkownika można utworzyć za pomocą skryptu ```SkryptDoUżytkowników.sql```, uruchomionego w SQL Server Management Studio.<br>
<br>

Jeśli połączenie z serwerem bazodanowym działa, bazę danych można utworzyć za pomocą komendy ```update-database``` w Package Manager Console w Visual Studio.<br>
Gdy baza zostanie utworzona, wypełnić ją losowymi danymi można skryptem ```faker-script.py```.<br>
<br>
Przygotowanie aplikacji jest zakończone i teraz można ją uruchomić.

## Korzystanie z aplikacji
Skrypt ```faker-script.py``` tworzy nowych użytkowników, w tym użytkownika *admin*.<br>
Przy logowaniu na konto *admin* należy podać "admin" w polach na Email i Hasło.<br>
Skrypt ```faker-script.py``` ustawia hasło dla pozostałych Użytkowników na ich Nazwisko. Wszystkich Użytkowników można znaleźć pod adresem ```/Users```, ale tę stronę widzi tylko Admin.<br>
*Admin* ma możliwość dodawania, usuwania, edytowania wszystkich Encji oraz generowania Raportów na stronie "Nieboszycy" pod adresem ```/Deceased```.<br>