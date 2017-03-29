CREATE USER search_operator WITH
  LOGIN
  SUPERUSER
  NOINHERIT
  CREATEDB
  CREATEROLE
  NOREPLICATION;

CREATE DATABASE search_operator
    WITH 
    OWNER = search_operator
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1252'
    LC_CTYPE = 'English_United States.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;