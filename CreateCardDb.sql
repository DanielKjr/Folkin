--
-- File generated with SQLiteStudio v3.3.3 on Thu Nov 17 12:01:07 2022
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Card
CREATE TABLE Card (ID INTEGER PRIMARY KEY, DeckID REFERENCES Deck (ID), Title STRING, Type STRING, Tag INTEGER, TagText STRING, Description STRING, Icon STRING, Sprite STRING);

-- Table: Deck
CREATE TABLE Deck (ID INTEGER PRIMARY KEY, UserID INTEGER REFERENCES User (ID), Name STRING);

-- Table: User
CREATE TABLE User (ID INTEGER PRIMARY KEY, Type STRING);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
