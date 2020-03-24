-- DROP DATABASE "KfzKonfiguratorDB";
-- CREATE DATABASE "KfzKonfiguratorDB";
-- \c KfzKonfiguratorDB
DROP TABLE IF EXISTS options;
CREATE TABLE options(
    option_id SERIAL PRIMARY KEY,
    group_name TEXT NOT NULL,
    name TEXT NOT NULL,
    price MONEY NOT NULL,
    description TEXT);
INSERT INTO options(group_name, name, price) VALUES ('Model', 'BMW X6 M', 97400);
INSERT INTO options(group_name, name, price) VALUES ('Motor', '1.0 L Benzin', 1000);
INSERT INTO options(group_name, name, price) VALUES ('Motor', '2.0 L Benzin', 2000);
INSERT INTO options(group_name, name, price) VALUES ('Motor', '2.0 L Diesel', 3000);
INSERT INTO options(group_name, name, price) VALUES ('Lackierung', 'Alpinweiß Uni', 0);
INSERT INTO options(group_name, name, price) VALUES ('Lackierung', 'Carbonschwarz Metallic', 0);
INSERT INTO options(group_name, name, price) VALUES ('Lackierung', 'Ametrin Metallic', 1300);
INSERT INTO options(group_name, name, price) VALUES ('Lackierung', 'Rein Gold', 200000);
INSERT INTO options(group_name, name, price) VALUES ('Felgen', '21"/22" M LMR Sternspeiche 809 M Bicolor / MB', 0);
INSERT INTO options(group_name, name, price) VALUES ('Felgen', '21"/22" LMR Sternspeiche 818 M Bicolor / MB', 0);
INSERT INTO options(group_name, name, price) VALUES ('Scheibenwischer', 'Ja', 100);
INSERT INTO options(group_name, name, price) VALUES ('Scheibenwischer', 'Nein', 0);

DROP TABLE IF EXISTS orders;
CREATE TABLE orders(
    order_id SERIAL PRIMARY KEY,
    option_id_list TEXT NOT NULL,
    total_price MONEY NOT NULL);