--
-- File generated with SQLiteStudio v3.3.3 on Tue Nov 22 13:35:54 2022
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Card
CREATE TABLE Card (ID INTEGER PRIMARY KEY, DeckID INTEGER, Title STRING, Type STRING, Tag INTEGER, TagText STRING, Description STRING, Icon STRING, Sprite STRING, FOREIGN KEY(DeckID) REFERENCES Deck(ID));

-- Table: Deck
CREATE TABLE Deck ( ID INTEGER PRIMARY KEY, UserID INTEGER,Name STRING, FOREIGN KEY(UserID) REFERENCES User(ID));
INSERT INTO Deck (ID, UserID, Name) VALUES (1, 1, 'Standard');

-- Table: User
CREATE TABLE User (ID INTEGER PRIMARY KEY, Type STRING);
INSERT INTO User (ID, Type) VALUES (1, 'Spillleder');

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
